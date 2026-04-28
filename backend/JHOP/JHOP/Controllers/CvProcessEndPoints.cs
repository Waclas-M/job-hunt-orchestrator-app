using JHOP.Dictionary;
using JHOP.Enums;
using JHOP.Models;
using JHOP.Models.Dto;
using JHOP.Models.Dto.Education;
using JHOP.Models.Dto.Experience;
using JHOP.Models.Dto.Interest;
using JHOP.Models.Dto.Language;
using JHOP.Models.Dto.Skill;
using JHOP.Models.Dto.Strenght;
using JHOP.RabbitMQProducer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Logging;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace JHOP.Controllers
{
    public static class CvProcessEndPoints
    {

        public static IEndpointRouteBuilder MapCvProcessEndPoints(this IEndpointRouteBuilder app)
        {

            //app.MapGet("/UserPersonalData", GetUserPersonalData);
            app.MapPost("/GenerateCv", StartGenerateProcess);
            app.MapPost("/EndFailedProcessCV", EndGenerateProcess).DisableAntiforgery();
            app.MapGet("/CheckStatus", CheckStatus);

            return app;
        }

        public static async Task<IResult> StartGenerateProcess(
            [FromBody] GenerateCvModel dto,
            ClaimsPrincipal user,
            [FromServices] AppDbContext db

            )
        {
            

            try
            {
                string userId = user.Claims.First(x => x.Type == "UserID").Value;

                List<UserEducationDto> education = new();
                List<UserExperienceDto> experience = new();
                List<UserStrengsDto> strengs = new();
                List<UserSkillsDto> skills = new();

                var cvOffer = new UserCvOffer
                {
                    UserId = userId,
                    OfferURL = dto.OfferURL,
                    Status = CvOfferStatus.InProgress,
                    CreatedAt = DateTime.UtcNow,
                    
                };

                db.UserCvOffers.Add(cvOffer);
                
                await db.SaveChangesAsync();
                

                if (!dto.UserEducationsProcessAuto)
                {
                    Console.WriteLine("Pobieranie edukacji dla usera: " + userId);
                    Console.WriteLine("Lista Id do pobrania" + dto.UserEducationsIds.First());

                    education = await db.UserEducations
                        .AsNoTracking()
                        .Where(x => x.Profile.UserId == userId 
                        && x.ProfileId == dto.ProfileId 
                        && dto.UserEducationsIds.Contains(x.Id))
                        .Select(x => new UserEducationDto
                        {
                            ProfileId = x.ProfileId,
                            SchoolName = x.SchoolName,
                            StudyProfile = x.StudyProfile,
                            StartDate = x.StartDate,
                            EndDate = x.EndDate,
                            IsCurrent = x.IsCurrent

                        }).ToListAsync();

                    Console.WriteLine("Lista która wróciła z bazy danych : " + education.First().ProfileId);
                }else
                {
                    education = await db.UserEducations.Where(x => x.Profile.UserId == userId
                      && x.ProfileId == dto.ProfileId).Select(x => new UserEducationDto
                      {
                          ProfileId= x.ProfileId,
                          SchoolName = x.SchoolName,
                          StudyProfile = x.StudyProfile,
                          StartDate = x.StartDate,
                          EndDate = x.EndDate,
                          IsCurrent = x.IsCurrent

                      }).ToListAsync();

                }


                
                if (!dto.UserExperiencesProcessAuto )
                {
                    experience = await db.UserJobExperiences.Where(x => x.Profile.UserId == userId && x.ProfileId == dto.ProfileId
                    && dto.UserExperiencesIds.Contains(x.Id)).Select(x => new UserExperienceDto
                    {
                        ProfileId= x.ProfileId,
                        CompanyName = x.CompanyName,
                        JobDescription = x.JobDescription,
                        StartDate = x.StartDate,
                        EndDate = x.EndDate,
                        JobTitle = x.JobTitle,
                        IsCurrent = x.IsCurrent

                    }).ToListAsync();
                }else
                {
                    experience = await db.UserJobExperiences.Where(x => x.Profile.UserId == userId && x.ProfileId == dto.ProfileId
                    ).Select(x => new UserExperienceDto
                    {
                        ProfileId = x.ProfileId,
                        CompanyName = x.CompanyName,
                        JobDescription = x.JobDescription,
                        StartDate = x.StartDate,
                        EndDate = x.EndDate,
                        JobTitle = x.JobTitle,
                        IsCurrent = x.IsCurrent
                    }).ToListAsync();
                }



                
                if (!dto.UserStrengsProcessAuto)
                {
                    strengs = await db.UserStrengths.Where(x => x.Profile.UserId == userId && x.ProfileId == dto.ProfileId
                    && dto.UserStrengsIds.Contains(x.Id)).Select(x => new UserStrengsDto
                    {
                        ProfileId = x.ProfileId,
                        Strength = x.Strength,
                    }).ToListAsync();

                } else
                {
                    strengs = await db.UserStrengths.Where(x => x.Profile.UserId == userId && x.ProfileId == dto.ProfileId
                    ).Select(x => new UserStrengsDto
                    {
                        ProfileId = x.ProfileId,
                        Strength = x.Strength,
                    }).ToListAsync();
                }





                
                if (!dto.UserSkillsProcessAuto )
                {
                    skills = await db.UserSkills.Where(x => x.Profile.UserId == userId && x.ProfileId == dto.ProfileId
                    && dto.UserSkillsIds.Contains(x.Id)).Select(x=> new UserSkillsDto
                    {
                        ProfileId = x.ProfileId,
                        Skill = x.Skill

                    } ).ToListAsync();
                } else
                {
                    skills = await db.UserSkills.Where(x => x.Profile.UserId == userId && x.ProfileId == dto.ProfileId
                    ).Select(x => new UserSkillsDto
                    {
                        ProfileId = x.ProfileId,
                        Skill = x.Skill
                    }).ToListAsync();
                }

                
                var personalData = await db.UserProfilePersonalData.Where(x => x.ProfileId == dto.ProfileId && x.Profile.UserId == userId).FirstOrDefaultAsync();
                var languages = (await db.UserLanguages
                .Where(x => x.Profile.UserId == userId && x.ProfileId == dto.ProfileId)
                .ToListAsync())
                .Select(x => new UserLanguageDtoRabbit
                {
                    ProfileId = x.ProfileId,
                    Language = LanguagesDictionary.Data[x.Language],
                    Level = x.Level
                })
                .ToList();
                            var interests = await db.UserInterests.Where(x => x.Profile.UserId == userId && x.ProfileId == dto.ProfileId).Select(x => new UserIntrestsDto
                {
                    ProfileId = x.ProfileId,
                    Interest = x.Interest,
                    Description = x.Description
                }).ToListAsync();

                var photo = await db.ProfilePhoto.Where(x => x.ProfileId == dto.ProfileId && x.Profile.UserId == userId).Select(x => new
                {
                    Data = x.Data,
                }).FirstOrDefaultAsync();

                var projects = await db.UserProjects.Where(x => x.Profile.UserId == userId && x.ProfileId == dto.ProfileId).Select(x => new
                {
                    ProfileId = x.ProfileId,
                    Name = x.ProjectName,
                    Description = x.Description,
                    Link = x.ProjectURL,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    Technologies = x.Technologies
                }).ToListAsync();


                var message = new
                {
                    UserId = userId,
                    OfferURL = dto.OfferURL,
                    CvForm = dto.CvForm,
                    PersonalData = personalData,
                    UserEducationsProcessAuto = dto.UserEducationsProcessAuto,
                    UserExperiencesProcessAuto = dto.UserExperiencesProcessAuto,
                    UserStrengsProcessAuto = dto.UserStrengsProcessAuto,
                    UserSkillsProcessAuto = dto.UserSkillsProcessAuto,
                    Education = education,
                    Experience = experience,
                    Strengs = strengs,
                    Skills = skills,
                    Languages = languages,
                    Interests = interests,
                    Projects = projects,
                    Photo = photo?.Data


                };

                await GenerateCvRabbitProducer.SendMessage(message);

                // Tutaj możesz dodać logikę do rozpoczęcia procesu generowania CV dla użytkownika o podanym userId
                // Na przykład, możesz wywołać serwis, który zajmuje się generowaniem CV i przekazać mu userId
            
                return Results.Ok(new { Message = $"Proces generowania CV został rozpoczęty dla użytkownika o ID: {userId}" });

            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { Message = $"Wystąpił błąd podczas rozpoczynania procesu generowania CV: {ex.Message}" });
            }
        }

        public static async Task<IResult> EndGenerateProcess(
        [FromForm] string userId,
        [FromForm] int status,
        [FromServices] AppDbContext db
        )
        {

            try
            {
                var offer = await db.UserCvOffers
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.Id)
                .FirstOrDefaultAsync();

                if (status == 2)
                {
                    offer.Status = Enums.CvOfferStatus.Failed;
                    await db.SaveChangesAsync();
                }
                return Results.Ok($"200 : zkończono proces usera {userId} status {status}");

            }
            catch (Exception ex) {
                return Results.BadRequest($"Nie udało się zakończyć procesu usera {userId}");
            }

        }

        public static async Task<IResult> CheckStatus(
        ClaimsPrincipal user,
        [FromServices] AppDbContext db
        )
        {
            string userId = user.Claims.First(x => x.Type == "UserID").Value;
            try
            {
                var offer = await db.UserCvOffers
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.Id)
                .FirstOrDefaultAsync();

                return Results.Ok(new { 
                    OfferId = offer.Id,
                    Status = offer.Status,
                });

            }
            catch (Exception ex) {return Results.Problem(ex.Message); }
        }





































    }
}
