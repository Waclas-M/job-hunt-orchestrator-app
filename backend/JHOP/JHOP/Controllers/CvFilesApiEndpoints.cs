using JHOP.Models;
using JHOP.Models.Dto;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Cryptography;

namespace JHOP.Controllers
{
    public static class CvFilesApiEndpoints
    {
        public static IEndpointRouteBuilder MapCvFilesEndPoints(this IEndpointRouteBuilder app)
        {

            //app.MapGet("/UserPersonalData", GetUserPersonalData);
            app.MapPost("/UploadCV", UploadCV).DisableAntiforgery();
            app.MapGet("/CV/DownloadCV{id:int}", DownloadCV).RequireAuthorization();
            app.MapGet("/CV/Offers", GetOffers).RequireAuthorization();
            app.MapDelete("/CV/Offers/Delete/{id:int}", DeleteOffer).RequireAuthorization();

            return app;
        }

        public static async Task<IResult> UploadCV(
            IFormFile file,
            [FromForm] string userId,
            [FromForm] string companyName,
            [FromServices] AppDbContext db)
        {
            if (string.IsNullOrWhiteSpace(userId))
                return Results.Unauthorized();

            if (file is null || file.Length == 0)
                return Results.BadRequest("Brak pliku.");

            if (Path.GetExtension(file.FileName).ToLowerInvariant() != ".pdf")
                return Results.BadRequest("Tylko PDF.");

            if (file.ContentType != "application/pdf")
                return Results.BadRequest("Content-Type musi być application/pdf.");

            byte[] data;

            var offer = await db.UserCvOffers
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.Id)
            .FirstOrDefaultAsync();

            if (offer == null)
                return Results.NotFound("Brak oferty.");

            await using (var ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);
                data = ms.ToArray();
            }

            var sha256 = Convert.ToHexString(SHA256.HashData(data)).ToLowerInvariant();

            var cvFile = new CvFile
            {
                UserId = userId,
                FileName = file.FileName,
                Data = data,
                SizeBytes = file.Length,
                OfferId = offer.Id,
                Sha256 = sha256,
                
                CreatedAt = DateTimeOffset.UtcNow
            };

            db.CvFiles.Add(cvFile);

            await db.SaveChangesAsync();  

            var cvFile1 = await db.CvFiles.Where(x => x.UserId == userId).OrderByDescending(x => x.Id).FirstOrDefaultAsync();
            offer.CvFileId = cvFile1.Id;
            offer.CompanyName = companyName;
            offer.Status = Enums.CvOfferStatus.Completed;

            await db.SaveChangesAsync();

            

            return Results.Ok(new { cvFile.Id, cvFile.FileName, cvFile.SizeBytes, cvFile.Sha256 });
        }

        public static async Task<IResult> DownloadCV(
         int id,
         ClaimsPrincipal user,
         [FromServices] AppDbContext db)
        {
            var userId = user.FindFirstValue("UserID");
            if (string.IsNullOrWhiteSpace(userId))
                return Results.Unauthorized();

            var cvFile = await db.CvFiles.FindAsync(id);

            if (cvFile == null)
                return Results.NotFound("Nie znaleziono pliku CV.");

            if (cvFile.UserId != userId) 
                return Results.Unauthorized();

            return Results.File(cvFile.Data, "application/pdf", cvFile.FileName);
        }

        public static async Task<IResult> GetOffers(
        ClaimsPrincipal user,
        [FromServices] AppDbContext db
        )
        {
            var userId = user.FindFirstValue("UserID");
            if (string.IsNullOrWhiteSpace(userId))
                return Results.Unauthorized();
            try
            {
                
                var offers = await db.UserCvOffers.Where(x => x.UserId == userId && x.Status == Enums.CvOfferStatus.Completed).Select(x => new
                {
                    Id = x.Id,
                    OfferURL = x.OfferURL,
                    CompanyName = x.CompanyName,
                    CreatedAt = x.CreatedAt,
                    CvFileId = x.CvFileId
                }).ToListAsync();
                return Results.Ok(offers);

            }catch (Exception ex)
            {
                return Results.NotFound(ex.Message);
            }
        }

        public static async Task<IResult> DeleteOffer(
        int id,
        ClaimsPrincipal user,
        [FromServices] AppDbContext db
        )
        {
            var userId = user.FindFirstValue("UserID");
            if (string.IsNullOrWhiteSpace(userId))
                return Results.Unauthorized();
            try
            {

                var offer = await db.UserCvOffers
                .FirstOrDefaultAsync(x => x.UserId == userId && x.Id == id);

                
                var cvFile = await db.CvFiles
                .FirstOrDefaultAsync(x => x.UserId == userId && x.Id == offer.CvFileId);

                if (offer != null && cvFile != null)
                { 
                    db.CvFiles.Remove(cvFile);
                    db.UserCvOffers.Remove(offer);
                    await db.SaveChangesAsync();
                }


                return Results.Ok($"Poprawnie usunięto ofertę i powiązane rekordy dla user {userId} id oferty{id}");

            }
            catch (Exception ex)
            {
                return Results.NotFound(ex.Message);
            }
        }

    }
}
