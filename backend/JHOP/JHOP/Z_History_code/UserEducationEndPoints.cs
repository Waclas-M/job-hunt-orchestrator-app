using JHOP.Models;
using JHOP.Models.Dto.Education;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace JHOP.Controllers
{
    public static class UserEducationEndPoints
    {

        public static IEndpointRouteBuilder MapUserEducatinEndPoints(this IEndpointRouteBuilder app)
        {

            app.MapGet("/GetUserEducationList", GetUserEducationList);
            app.MapPost("/AddUserEducation", AddUserEducation);
            app.MapPut("/EditUserEducation", EditUserEducation);
            return app;
        }

        public static async Task<IResult> GetUserEducationList(
            ClaimsPrincipal user,
          [FromServices] AppDbContext db
           )
        {
            string userId = user.Claims.First(x => x.Type == "UserID").Value;
            var list = await db.UserEducations
                .Where(e => e.Profile.UserId == userId)
                .ToListAsync();

            return Results.Ok(list);
        }

        public static async Task<IResult> AddUserEducation(
            [FromBody] UserEducationDto dto,
            ClaimsPrincipal user,
            [FromServices] AppDbContext db
           )
        {
            string userId = user.Claims.First(x => x.Type == "UserID").Value;
            var education = new UserEducation
            {
                ProfileId = dto.ProfileId,
                SchoolName = dto.SchoolName,
                StudyProfile = dto.StudyProfile,
                StartDate = dto.StartDate,
                EndDate = dto.IsCurrent ? null : dto.EndDate,
                IsCurrent = dto.IsCurrent
            };
            db.UserEducations.Add(education);
            await db.SaveChangesAsync();
            return Results.Ok(education);
        }

        public static async Task<IResult> EditUserEducation(
            [FromBody] EditUserEducationDto dto,
            ClaimsPrincipal user,
            [FromServices] AppDbContext db
           )
        {
            string userId = user.Claims.First(x => x.Type == "UserID").Value;
            var education = await db.UserEducations
                .Where(x => x.Profile.UserId == userId)
                .FirstOrDefaultAsync(x => x.Id == dto.Id);
            if (education == null)
            {
                return Results.NotFound();
            }
            education.SchoolName = dto.SchoolName;
            education.StudyProfile = dto.StudyProfile;
            education.StartDate = dto.StartDate;
            education.EndDate = dto.IsCurrent ? null : dto.EndDate;
            education.IsCurrent = dto.IsCurrent;
            await db.SaveChangesAsync();
            return Results.Ok(education);


        }
    }
}
