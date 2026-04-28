using JHOP.Models;
using JHOP.Models.Dto;
using JHOP.Models.Dto.Education;
using JHOP.Models.Dto.Experience;
using JHOP.Models.Dto.Interest;
using JHOP.Models.Dto.Language;
using JHOP.Models.Dto.Photo;
using JHOP.Models.Dto.Projects;
using JHOP.Models.Dto.Skill;
using JHOP.Models.Dto.Strenght;
using JHOP.Models.Dto.UserPersonalData;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;
using System.Security.Cryptography;

namespace JHOP.Controllers
{
    public static class ProfileEndPoints
    {
        public static IEndpointRouteBuilder MapProfileEndPoints(this IEndpointRouteBuilder app)
        {
            // Profiles
            app.MapGet("/GetUserProfiles", GetManyProfiles);
            app.MapGet("/GetUserProfile", GetProfileById);
            app.MapGet("/GetUserProfilesPhotos", GetProfilePhotos);

            // PersonalData
            app.MapPost("/AddProfilePersonalData", PostPersonalData);
            app.MapPut("/EditProfilePersonalData", EditPersonalData);
            app.MapPut("/EditProfileUserProfile", EditUserPersonalProfileData);
            app.MapPut("/EditProfileLinks", EditPersonalDataLinks);


            // Education
            app.MapPost("/AddProfileEducation", PostEducation);
            app.MapPut("/EditProfileEducation", EditEducation);
            app.MapDelete("/DeleteProfileEducation/{id:int}", DeleteEducation);

            // Experience
            app.MapPost("/AddProfileExperience", PostExperience);
            app.MapPut("/EditProfileExperience", EditExperience);
            app.MapDelete("/DeleteProfileExperience/{id:int}", DeletExperience);

            // Intrests
            app.MapPost("/AddProfileInterest", PostInterest);
            app.MapPut("/EditProfileInterest", EditInterest);
            app.MapDelete("/DeleteProfileInterest/{id:int}", DeleteInterest);

            // Skills
            app.MapPost("/AddProfileSkill", PostSkill);
            app.MapPut("/EditProfileSkill", EditSkill);
            app.MapDelete("/DeleteProfileSkill/{id:int}", DeleteSkill);

            // Strengths

            app.MapPost("/AddProfileStrenght", PostStrenght);
            app.MapPut("/EditProfileStrenght", EditStrenght);
            app.MapDelete("/DeleteProfileStrenght/{id:int}", DeleteStrenght);

            // Language
            app.MapPost("/AddProfileLanguage", PostLanguage);
            app.MapPut("/EditProfileLanguage", EditLanguage);
            app.MapDelete("/DeleteProfileLanguage/{id:int}", DeleteLanguage);

            // Photo
            app.MapPost("/AddProfilePhoto", PostProfilePhoto).DisableAntiforgery();
            app.MapDelete("/DeleteProfilePhoto/{id:int}", DeleteProfilePhoto);
            app.MapPut("/EditProfilePhoto", EditProfilePhoto).DisableAntiforgery();

            // Projects
            app.MapPost("/AddProfileProject", PostProfileProject);
            app.MapPut("/EditProfileProject", EditProfileProject);
            app.MapDelete("/DeleteProfileProject/{id:int}", DeleteProfileProject);


            return app;
        }

        // Profile
        public static async Task<IResult> GetManyProfiles(
         ClaimsPrincipal user,
         [FromServices] AppDbContext db
          )
        {
            string userId = user.Claims.First(x => x.Type == "UserID").Value;
            try
            {
                var profiles = await db.Profiles.Where(x => x.UserId == userId)
                    .Include(p => p.PersonalData)
                    .Include(p => p.Educations)
                    .Include(p => p.JobExperiences)
                    .Include(p => p.Languages)
                    .Include(p => p.Interests)
                    .Include(p => p.Skills)
                    .Include(p => p.Strengths)
                    .Include(p => p.Projects)
                    .ToListAsync();
                return Results.Ok(profiles);

            }
            catch (Exception ex)
            {
                return Results.BadRequest($"Wystąpił błąd podczas pobierania danych profilów. {ex.ToString()}");
            }

        }

        public static async Task<IResult> GetProfileById(
         int id,
         ClaimsPrincipal user,
         [FromServices] AppDbContext db
            )
        {
            string userId = user.Claims.First(x => x.Type == "UserID").Value;

            try
            {
                var profile = await db.Profiles.Where(p => p.UserId == userId)
                .Where(p => p.Id == id)
                .Include(p => p.PersonalData)
                .Include(p => p.Educations)
                .Include(p => p.JobExperiences)
                .Include(p => p.Languages)
                .Include(p => p.Interests)
                .Include(p => p.Skills)
                .Include(p => p.Strengths)
                .Include(p => p.Projects)
                .ToListAsync();

                if (profile == null)
                {
                    return Results.Unauthorized();
                }

                return Results.Ok(profile);

            }
            catch (Exception ex) { return Results.BadRequest($"Wystąpił błąd podczas pobierania danych profilu.{ex.ToString()}"); }


        }

        public static async Task<IResult> GetProfilePhotos(
        ClaimsPrincipal user,
        [FromServices] AppDbContext db
        )
        {

            string userId = user.Claims.First(x => x.Type == "UserID").Value;
            try
            {
                var photos = await db.ProfilePhoto.Where(x => x.Profile.UserId == userId).ToListAsync();
                return Results.Ok(photos);
            }
            catch (Exception ex) { return Results.BadRequest($"Wystąpił błąd podczas pobierania zdjęć profilowych. {ex.ToString()}"); }

        }

        //Personal Data
        public static async Task<IResult> PostPersonalData(
        ClaimsPrincipal user,
        [FromServices] AppDbContext db,
        [FromBody] ProfilePersonalDataDto dto
        )
        {
            try
            {

                db.UserProfilePersonalData.Add(new UserProfilePersonalData
                {
                    ProfileId = dto.ProfileId,
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Email = dto.Email,
                    PhoneNumber = dto.PhoneNumber ?? string.Empty
                });
                await db.SaveChangesAsync();
                return Results.Ok(dto);
            }
            catch (Exception ex) { return Results.BadRequest($"Wystąpił błąd podczas dodawania danych osobowych do profilu. {ex.ToString()}"); }

        }

        public static async Task<IResult> EditPersonalData(
        ClaimsPrincipal user,
        [FromServices] AppDbContext db,
        [FromBody] ProfilePersonalDataDto dto
        )
        {
            try
            {
                string userId = user.Claims.First(x => x.Type == "UserID").Value;
                var record = await db.UserProfilePersonalData.Where(x => x.ProfileId == dto.ProfileId && x.Profile.UserId == userId).FirstOrDefaultAsync();
                record.FirstName = dto.FirstName;
                record.LastName = dto.LastName;
                record.Email = dto.Email;
                record.PhoneNumber = dto.PhoneNumber;
                await db.SaveChangesAsync();
                return Results.Ok(record);

            }
            catch (Exception ex) { return Results.BadRequest($"Wystąpił błąd podczas edytowania profilu. {ex.ToString()}"); }

        }

        public static async Task<IResult> EditUserPersonalProfileData(
        ClaimsPrincipal user,
        [FromServices] AppDbContext db,
        [FromBody] EditUserPersonalProfileDto dto
        )
        {
            try
            {
                string userId = user.Claims.First(x => x.Type == "UserID").Value;
                var record = await db.UserProfilePersonalData.Where(x => x.ProfileId == dto.ProfileId && x.Profile.UserId == userId).FirstOrDefaultAsync();
                record.PersonalProfile = dto.PersonalProfile;
                await db.SaveChangesAsync();
                return Results.Ok(record);

            }
            catch (Exception ex) { return Results.BadRequest($"Wystąpił błąd podczas edytowania profilu. {ex.ToString()}"); }


        }

        public static async Task<IResult> EditPersonalDataLinks(
        ClaimsPrincipal user,
        [FromServices] AppDbContext db,
        [FromBody] EditUserContactLinksDto dto
        )
        {
            try
            {
                string userId = user.Claims.First(x => x.Type == "UserID").Value;
                var record = await db.UserProfilePersonalData.Where(x => x.ProfileId == dto.ProfileId && x.Profile.UserId == userId).FirstOrDefaultAsync();
                record.GitHubURL = dto.GitHubURL;
                record.LinkedInURL = dto.LinkedInURL;

                await db.SaveChangesAsync();
                return Results.Ok(record);
            }
            catch (Exception ex) { return Results.BadRequest($"Wystąpił błąd podczas edytowania linków profilu. {ex.ToString()}"); }
        }



        // Education
        public static async Task<IResult> PostEducation(
        ClaimsPrincipal user,
        [FromServices] AppDbContext db,
        [FromBody] UserEducationDto dto
        )
        {
            try
            {
                db.UserEducations.Add(new UserEducation
                {
                    ProfileId = dto.ProfileId,
                    SchoolName = dto.SchoolName,
                    StudyProfile = dto.StudyProfile,
                    StartDate = dto.StartDate,
                    EndDate = dto.EndDate,
                    IsCurrent = dto.IsCurrent,
                });
                await db.SaveChangesAsync();
                return Results.Ok(dto);
            }
            catch (Exception ex) { return Results.BadRequest($"Wystąpił błąd podczas dodawania edukacji do profilu. {ex.ToString()}"); }

        }

        public static async Task<IResult> EditEducation(
        ClaimsPrincipal user,
        [FromServices] AppDbContext db,
        [FromBody] EditUserEducationDto dto
        )
        {
            try
            {
                string userId = user.Claims.First(x => x.Type == "UserID").Value;
                var record = await db.UserEducations.Where(x => x.ProfileId == dto.ProfileId && x.Profile.UserId == userId && x.Id == dto.Id).FirstOrDefaultAsync();

                record.SchoolName = dto.SchoolName;
                record.StudyProfile = dto.StudyProfile;
                record.StartDate = dto.StartDate;
                record.EndDate = dto.EndDate;
                record.IsCurrent = dto.IsCurrent;

                await db.SaveChangesAsync();
                return Results.Ok(record);
            }
            catch (Exception ex) { return Results.BadRequest($"Wystąpił błąd podczas edytowaniu edukacji. {ex.ToString()}"); }

        }


        public static async Task<IResult> DeleteEducation(
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

                var educationRekord = await db.UserEducations.FirstOrDefaultAsync(x => x.Id == id);
                var name = educationRekord.SchoolName;
                if (educationRekord != null)
                {
                    db.UserEducations.Remove(educationRekord);
                    await db.SaveChangesAsync();
                }

                return Results.Ok($"Poprawnie usunięto {name}");
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }


        // Experience
        public static async Task<IResult> PostExperience(
        ClaimsPrincipal user,
        [FromServices] AppDbContext db,
        [FromBody] UserExperienceDto dto
        )
        {
            try
            {
                db.UserJobExperiences.Add(new UserJobExperience
                {
                    ProfileId = dto.ProfileId,
                    CompanyName = dto.CompanyName,
                    JobDescription = dto.JobDescription,
                    JobTitle = dto.JobTitle,
                    StartDate = dto.StartDate,
                    EndDate = dto.EndDate,
                    IsCurrent = dto.IsCurrent,
                });
                await db.SaveChangesAsync();
                return Results.Ok(dto);

            }
            catch (Exception ex) { return Results.BadRequest($"Wystąpił błąd podczas dodawania doświadczenia do profilu. {ex.ToString()}"); }

        }

        public static async Task<IResult> EditExperience(
        ClaimsPrincipal user,
        [FromServices] AppDbContext db,
        [FromBody] EditUserExperienceDto dto
        )
        {
            try
            {
                string userId = user.Claims.First(x => x.Type == "UserID").Value;
                var record = await db.UserJobExperiences.Where(x => x.ProfileId == dto.ProfileId && x.Profile.UserId == userId && x.Id == dto.Id).FirstOrDefaultAsync();

                record.CompanyName = dto.CompanyName;
                record.JobDescription = dto.JobDescription;
                record.JobTitle = dto.JobTitle;
                record.StartDate = dto.StartDate;
                record.EndDate = dto.EndDate;
                record.IsCurrent = dto.IsCurrent;

                await db.SaveChangesAsync();
                return Results.Ok(record);
            }
            catch (Exception ex) { return Results.BadRequest($"Wystąpił błąd podczas edytowania doświadczenia. {ex.ToString()}"); }
        }

        public static async Task<IResult> DeletExperience(
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

                var experienceRekord = await db.UserJobExperiences.FirstOrDefaultAsync(x => x.Id == id);
                var name = experienceRekord.CompanyName;
                if (experienceRekord != null)
                {
                    db.UserJobExperiences.Remove(experienceRekord);
                    await db.SaveChangesAsync();
                }

                return Results.Ok($"Poprawnie usunięto  {name}");
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }

        // Interest
        public static async Task<IResult> PostInterest(
        ClaimsPrincipal user,
        [FromServices] AppDbContext db,
        [FromBody] UserIntrestsDto dto
        )
        {
            try
            {
                db.UserInterests.Add(new UserInterests
                {
                    ProfileId = dto.ProfileId,
                    Interest = dto.Interest,
                    Description = dto.Description,
                });
                await db.SaveChangesAsync();
                return Results.Ok(dto);

            }
            catch (Exception ex) { return Results.BadRequest($"Wystąpił błąd podczas dodawania zainteresowań do profilu. {ex.ToString()}"); }

        }

        public static async Task<IResult> EditInterest(
        ClaimsPrincipal user,
        [FromServices] AppDbContext db,
        [FromBody] EditUserIntrestsDto dto
        )
        {
            try
            {
                string userId = user.Claims.First(x => x.Type == "UserID").Value;
                var record = await db.UserInterests.Where(x => x.ProfileId == dto.ProfileId && x.Profile.UserId == userId && x.Id == dto.Id).FirstOrDefaultAsync();

                record.Interest = dto.Interest;
                record.Description = dto.Description;

                await db.SaveChangesAsync();
                return Results.Ok(dto);
            }
            catch (Exception ex) { return Results.BadRequest($"Wystąpił błąd podczas edytowaniu zainteresowań. {ex.ToString()}"); }
        }

        public static async Task<IResult> DeleteInterest(
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

                var interesteRekord = await db.UserInterests.FirstOrDefaultAsync(x => x.Id == id);
                var name = interesteRekord.Interest;
                if (interesteRekord != null)
                {
                    db.UserInterests.Remove(interesteRekord);
                    await db.SaveChangesAsync();
                }

                return Results.Ok($"Poprawnie usunięto  {name}");
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }

        // Skill
        public static async Task<IResult> PostSkill(
        ClaimsPrincipal user,
        [FromServices] AppDbContext db,
        [FromBody] UserSkillsDto dto
        )
        {
            try
            {
                db.UserSkills.Add(new UserSkills
                {
                    ProfileId = dto.ProfileId,
                    Skill = dto.Skill,
                });
                await db.SaveChangesAsync();
                return Results.Ok(dto);


            }
            catch (Exception ex) { return Results.BadRequest($"Wystąpił błąd podczas dodawania umiejętności do profilu. {ex.ToString()}"); }
        }

        public static async Task<IResult> EditSkill(
        ClaimsPrincipal user,
        [FromServices] AppDbContext db,
        [FromBody] EditUserSkillsDto dto
        )
        {
            try
            {
                string userId = user.Claims.First(x => x.Type == "UserID").Value;
                var record = await db.UserSkills.Where(x => x.ProfileId == dto.ProfileId && x.Profile.UserId == userId && x.Id == dto.Id).FirstOrDefaultAsync();

                record.Skill = dto.Skill;



                await db.SaveChangesAsync();
                return Results.Ok(dto);
            }
            catch (Exception ex) { return Results.BadRequest($"Wystąpił błąd podczas edytowania umiejętności. {ex.ToString()}"); }
        }

        public static async Task<IResult> DeleteSkill(
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

                var skillRekord = await db.UserSkills.FirstOrDefaultAsync(x => x.Id == id);
                var name = skillRekord.Skill;
                if (skillRekord != null)
                {
                    db.UserSkills.Remove(skillRekord);
                    await db.SaveChangesAsync();
                }

                return Results.Ok($"Poprawnie usunięto  {name}");
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }

        // Strenght

        public static async Task<IResult> PostStrenght(
        ClaimsPrincipal user,
        [FromServices] AppDbContext db,
        [FromBody] UserStrengsDto dto
        )
        {
            try
            {
                db.UserStrengths.Add(new UserStrengths
                {
                    ProfileId = dto.ProfileId,
                    Strength = dto.Strength,
                });
                await db.SaveChangesAsync();
                return Results.Ok(dto);
            }
            catch (Exception ex) { return Results.BadRequest($"Wystąpił błąd podczas dodawania mocnych stron do profilu. {ex.ToString()}"); }


        }

        public static async Task<IResult> EditStrenght(
        ClaimsPrincipal user,
        [FromServices] AppDbContext db,
        [FromBody] EditStrengsDto dto
        )
        {
            try
            {
                string userId = user.Claims.First(x => x.Type == "UserID").Value;
                var record = await db.UserStrengths.Where(x => x.ProfileId == dto.ProfileId && x.Profile.UserId == userId && x.Id == dto.Id).FirstOrDefaultAsync();

                record.Strength = dto.Strength;


                await db.SaveChangesAsync();
                return Results.Ok(dto);
            }
            catch (Exception ex) { return Results.BadRequest($"Wystąpił błąd podczas edytowania umiejętności. {ex.ToString()}"); }

        }

        public static async Task<IResult> DeleteStrenght(
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

                var strenghtRekord = await db.UserStrengths.FirstOrDefaultAsync(x => x.Id == id);
                var name = strenghtRekord.Strength;
                if (strenghtRekord != null)
                {
                    db.UserStrengths.Remove(strenghtRekord);
                    await db.SaveChangesAsync();
                }

                return Results.Ok($"Poprawnie usunięto  {name}");
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }

        // Language

        public static async Task<IResult> PostLanguage(
        ClaimsPrincipal user,
        [FromServices] AppDbContext db,
        [FromBody] UserLanguagesDto dto
        )
        {

            try
            {
                db.UserLanguages.Add(new UserLanguages
                {
                    ProfileId = dto.ProfileId,
                    Language = dto.Language,
                    Level = dto.Level
                });
                await db.SaveChangesAsync();
                return Results.Ok(dto);
            }
            catch (Exception ex) { return Results.BadRequest($"Wystąpił błąd podczas dodawania języka do profilu. {ex.ToString()}"); }

        }

        public static async Task<IResult> EditLanguage(
        ClaimsPrincipal user,
        [FromServices] AppDbContext db,
        [FromBody] EditUserLanguagesDto dto
        )
        {
            try
            {
                string userId = user.Claims.First(x => x.Type == "UserID").Value;
                var record = await db.UserLanguages.Where(x => x.ProfileId == dto.ProfileId && x.Profile.UserId == userId && x.Id == dto.Id).FirstOrDefaultAsync();

                record.Language = dto.Language;
                record.Level = dto.Level;



                await db.SaveChangesAsync();
                return Results.Ok(dto);
            }
            catch (Exception ex) { return Results.BadRequest($"Wystąpił błąd podczas edytowania kompetencyji językowych. {ex.ToString()}"); }

        }

        public static async Task<IResult> DeleteLanguage(
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

                var languageRekord = await db.UserLanguages.FirstOrDefaultAsync(x => x.Id == id);
                var name = languageRekord.Language;
                if (languageRekord != null)
                {
                    db.UserLanguages.Remove(languageRekord);
                    await db.SaveChangesAsync();
                }

                return Results.Ok($"Poprawnie usunięto  {name}");
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }

        // Photo

        public static async Task<IResult> PostProfilePhoto(
        ClaimsPrincipal user,
        [FromServices] AppDbContext db,
        [FromForm] PhotoDto dto
        )
        {
            try
            {
                var userId = user.FindFirstValue("UserID");
                if (string.IsNullOrWhiteSpace(userId))
                    return Results.Unauthorized();

                if (dto.selectedFile is null || dto.selectedFile.Length == 0)
                    return Results.BadRequest("Brak pliku.");

                var allowedExtensions = new[] { ".png", ".jpg", ".jpeg" };
                var allowedTypes = new[] { "image/png", "image/jpeg" };

                var extension = Path.GetExtension(dto.selectedFile.FileName);

                if (!allowedExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase))
                    return Results.BadRequest("Tylko formaty .png, .jpg, .jpeg.");

                if (!allowedTypes.Contains(dto.selectedFile.ContentType))
                    return Results.BadRequest("Nieprawidłowy typ pliku.");

                byte[] data;

                await using (var ms = new MemoryStream())
                {
                    await dto.selectedFile.CopyToAsync(ms);
                    data = ms.ToArray();
                }
                var sha256 = Convert.ToHexString(SHA256.HashData(data)).ToLowerInvariant();

                var profilePhoto = new ProfilePhoto
                {
                    ProfileId = dto.ProfileId,
                    Data = data,
                    SizeBytes = dto.selectedFile.Length,
                    Sha256 = sha256,
                    CreatedAt = DateTimeOffset.UtcNow
                };

                db.ProfilePhoto.Add(profilePhoto);
                await db.SaveChangesAsync();

                return Results.Ok($"Poprawnie dodano zdjęcie.");
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }

        public static async Task<IResult> DeleteProfilePhoto(
        ClaimsPrincipal user,
        [FromServices] AppDbContext db,
        int id
        )
        {
            try
            {

                var userId = user.FindFirstValue("UserID");
                if (string.IsNullOrWhiteSpace(userId))
                    return Results.Unauthorized();

                var photoRecord = await db.ProfilePhoto.FirstOrDefaultAsync(x => x.Id == id);
                db.ProfilePhoto.Remove(photoRecord);
                await db.SaveChangesAsync();

                return Results.Ok($"Poprawnie usunięto zdjęcie.");


            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);

            }




        }

        public static async Task<IResult> EditProfilePhoto(
        ClaimsPrincipal user,
        [FromServices] AppDbContext db,
        [FromBody] PhotoDto dto
        )
        {
            try
            {
                var userId = user.FindFirstValue("UserID");
                if (string.IsNullOrWhiteSpace(userId))
                    return Results.Unauthorized();
                var photoRecord = await db.ProfilePhoto.FirstOrDefaultAsync(x => x.ProfileId == dto.ProfileId);
                if (photoRecord != null)
                {
                    db.ProfilePhoto.Remove(photoRecord);
                    await db.SaveChangesAsync();
                }
                else { return Results.NotFound("Nie znalezienio pliku do edycji"); }

                var result = await PostProfilePhoto(user, db, dto);

                if (result is Ok<object> ok)
                {
                    return Results.Ok($"Poprawnie zmieniono zdjęcie.");
                }
                else if (result is BadRequest<string> bad)
                {
                    return result;
                }

                return Results.Ok($"Poprawnie zmieniono zdjęcie.");

            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);

            }
        }

        // Projects

        public static async Task<IResult> PostProfileProject(
        ClaimsPrincipal user,
        [FromServices] AppDbContext db,
        [FromBody] UserProjectsDto dto
        )
        {
            try
            {
                var userId = user.FindFirstValue("UserID");
                if (string.IsNullOrWhiteSpace(userId))
                    return Results.Unauthorized();

                db.UserProjects.Add(new UserProjects
                {
                    ProfileId = dto.ProfileId,
                    ProjectName = dto.ProjectName,
                    ProjectURL = dto.ProjectURL,
                    Description = dto.Description,
                    StartDate = dto.StartDate,
                    EndDate = dto.EndDate,
                    Technologies = dto.Technologies
                });
                await db.SaveChangesAsync();
                return Results.Ok(dto);




            }
            catch (Exception ex)
            {
                return Results.BadRequest($"Wystąpił błąd podczas dodawania projektu do profilu. {ex.ToString()}");
            }
        }


        public static async Task<IResult> EditProfileProject(
        ClaimsPrincipal user,
        [FromServices] AppDbContext db,
        [FromBody] UserEditProjectsDto dto
        )
        {
            try
            {
                string userId = user.Claims.First(x => x.Type == "UserID").Value;
                var record = await db.UserProjects.Where(x => x.ProfileId == dto.ProfileId && x.Profile.UserId == userId && x.Id == dto.Id).FirstOrDefaultAsync();
                record.ProjectName = dto.ProjectName;
                record.Description = dto.Description;
                record.ProjectURL = dto.ProjectURL;
                record.StartDate = dto.StartDate;
                record.EndDate = dto.EndDate;
                record.Technologies = dto.Technologies;
                await db.SaveChangesAsync();
                return Results.Ok(dto);
            }
            catch (Exception ex) { return Results.BadRequest($"Wystąpił błąd podczas edytowania projektu. {ex.ToString()}"); }

        }

        public static async Task<IResult> DeleteProfileProject(
        ClaimsPrincipal user,
        [FromServices] AppDbContext db,
        int id
        )
        {
            try
            {
                var userId = user.FindFirstValue("UserID");
                if (string.IsNullOrWhiteSpace(userId))
                    return Results.Unauthorized();

                var projectRecord = await db.UserProjects.FirstOrDefaultAsync(x => x.Id == id);
                db.UserProjects.Remove(projectRecord);
                await db.SaveChangesAsync();
                return Results.Ok($"Poprawnie usunięto projekt.");
            }
            catch (Exception ex)
            {
                return Results.BadRequest($"Wystąpił błąd podczas usuwania projektu. {ex.ToString()}");

            }
        }
    }
}
