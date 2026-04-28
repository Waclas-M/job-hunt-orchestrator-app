using JHOP.Models;
using JHOP.Models.Dto;
using JHOP.ReadModels.Cv;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace JHOP.Controllers
{
    public static class CvDataTechnicalaApiEndpoints
    {
        //public static IEndpointRouteBuilder MapCvDataTechnicalaApiEndpoints(this IEndpointRouteBuilder app)
        //{

        //    app.MapGet("/UserCvDetailsData", UserCvDetailsData);

        //    return app;
        //}

        //public static async Task<IResult> UserCvDetailsData(
        //    string userId,
        //    ClaimsPrincipal user,
        //    [FromServices] AppDbContext db,
        //    [FromServices] UserManager<AppUser> userMenager
        //    )
        //{
        //    if (user.Claims.First(x=> x.Type == "ServiceName").Value != "PythonUploader" && user.IsInRole("Service") != true)
        //    {
        //        return Results.Unauthorized();
        //    }
        //    try
        //    {
                
        //        var educations = await db.UserEducations.Where(x => x.UserId == userId).Select(x => new UserEducationModel
        //        {
        //            SchoolName = x.SchoolName,
        //            StudyProfile = x.StudyProfile,
        //            StartDate = x.StartDate,
        //            EndDate = x.EndDate,
        //            IsCurrent = x.IsCurrent
        //        }).ToListAsync();

        //        var jobs = await db.UserJobExperiences
        //                .Where(x => x.UserId == userId)
        //                .Select(x => new UserExperienceModel
        //                {
        //                    CompanyName = x.CompanyName,
        //                    JobTitle = x.JobTitle,
        //                    JobDescription = x.JobDescription,
        //                    StartDate = x.StartDate,
        //                    EndDate = x.EndDate,
        //                    IsCurrent = x.IsCurrent
        //                })
        //                .ToListAsync();
        //        var languages = await db.UserLanguages.Where(x => x.UserId == userId)
        //                .Select(x => new UserLanguagesModel
        //                {
        //                    Language = x.Language,
        //                    Level = x.Level
        //                })
        //                .ToListAsync();

        //        var strengs = await db.UserStrengs.Where(x => x.UserId == userId)
        //                .Select(x => new UserStrengsModel
        //                {
        //                    Streng = x.Streng
        //                })
        //                .ToListAsync();

        //        var skills = await db.UserSkills.Where(x => x.UserId == userId)
        //                .Select(x => new UserSkillsModel
        //                {
        //                    Skill = x.Skill
        //                })
        //                .ToListAsync();

        //        var intrest = await db.UserIntrests.Where(x => x.UserId == userId)
        //                .Select(x => new UserIntrestsModel
        //                {
        //                    Intrest = x.Intrest,
        //                    Description = x.Description
        //                })
        //                .ToListAsync();

        //        CvReadModelFactory cvReadModelFactory = new CvReadModelFactory();

        //        var data = cvReadModelFactory.Build(
        //            await userMenager.FindByIdAsync(userId),
        //            educations,
        //            jobs,
        //            languages,
        //            strengs,
        //            skills,
        //            intrest
        //        );

                




        //        return Results.Ok(new { data  });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Results.BadRequest(new { Message = $"Wystąpił błąd podczas pobierania danych CV: {ex.Message}" });
        //    }

        //}

    }
}
