using Humanizer;
using JHOP.Models;
using JHOP.Models.Dto.Experience;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace JHOP.Controllers
{
    public static class JobExperienceEndPoints
    {
       
        public static IEndpointRouteBuilder MapJobExperienceEndPoints(this IEndpointRouteBuilder app)
        {
           
            app.MapGet("/GetUserExperienceList", GetUserExperienceList);
            app.MapPost("/AddUserExperience", AddUserExperience);
            app.MapPut("/EditUserExperience", EditUserExperience);
            return app;
        }

        public static async Task<IResult> GetUserExperienceList(
           
            ClaimsPrincipal user,
          [FromServices] AppDbContext db
           )
        {
            string userId = user.Claims.First(x => x.Type == "UserID").Value;
            var list = await db.UserJobExperiences
             .Where(x => x.Profile.UserId == userId)
             .ToListAsync();

            return Results.Ok(new { list });
        }

        public static async Task<IResult> AddUserExperience(
            [FromBody] UserExperienceDto dto,
            ClaimsPrincipal user,
            [FromServices] AppDbContext db
           )
        {
            string userId = user.Claims.First(x => x.Type == "UserID").Value;
            var experience = new UserJobExperience
            {
                ProfileId = dto.ProfileId,
                CompanyName = dto.CompanyName,
                JobTitle = dto.JobTitle,
                JobDescription = dto.JobDescription,
                StartDate = dto.StartDate,
                EndDate = dto.IsCurrent ? null : dto.EndDate,
                IsCurrent = dto.IsCurrent
            };

            db.UserJobExperiences.Add(experience);
            await db.SaveChangesAsync();

            return Results.Ok(experience);
        }

        public static async Task<IResult> EditUserExperience(
           [FromBody] EditUserExperienceDto dto,
           ClaimsPrincipal user,
           [FromServices] AppDbContext db
          )
        {
            string userId = user.Claims.First(x => x.Type == "UserID").Value;
            var experience = await db.UserJobExperiences
                .Where(x => x.Profile.UserId == userId && x.ProfileId == dto.ProfileId )
                .FirstOrDefaultAsync(x => x.Id == dto.Id);

            experience.CompanyName = dto.CompanyName;
            experience.JobTitle = dto.JobTitle;
            experience.JobDescription = dto.JobDescription;
            experience.StartDate = dto.StartDate;
            experience.EndDate = dto.IsCurrent ? null : dto.EndDate;
            experience.IsCurrent = dto.IsCurrent;
            await db.SaveChangesAsync();

            return Results.Ok(experience);
        }
        

    }
}
