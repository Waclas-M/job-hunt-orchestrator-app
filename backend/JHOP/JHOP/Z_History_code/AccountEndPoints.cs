using JHOP.Models;
using JHOP.Models.Dto;
using JHOP.Models.Dto.UserPersonalData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JHOP.Controllers
{
    public static class AccountEndPoints
    {
        public static IEndpointRouteBuilder MapAccountEndpoints(this IEndpointRouteBuilder app)
        {

            app.MapGet("/UserPersonalData", GetUserPersonalData);
            app.MapPut("/EditUserPersonalData", EditUserPersonalData);

            app.MapGet("/UserPersonalProfile", GetUserProfile);
            app.MapPut("/EditUserPersonalProfile", EditUserPersonalProfile);

            app.MapGet("/UserContactLinks", GetUserContactLinks);
            app.MapPut("/EditUserContactLinks", EditUserContactLinks);

            return app;
        }

        [Authorize]
        public static async Task<IResult> GetUserPersonalData(ClaimsPrincipal user,
            UserManager<AppUser> userMenager)
        {
            string userId = user.Claims.First(x => x.Type == "UserID").Value;
            var userDetails = await userMenager.FindByIdAsync(userId);
            return Results.Ok(new { FirstName = userDetails.Name, LastName = userDetails.SurName, Email = userDetails.Email, PhoneNumber = userDetails?.PhoneNumber });
        }

        [Authorize]

        public static async Task<IResult> EditUserPersonalData(ClaimsPrincipal user,
            UserManager<AppUser> userMenager, [FromBody] EditUserPersonalDataModel dto)
        {
            string userId = user.Claims.First(x => x.Type == "UserID").Value;
            var userDetails = await userMenager.FindByIdAsync(userId);
            userDetails.Name = dto.FirstName;
            userDetails.SurName = dto.LastName;
            userDetails.Email = dto.Email;
            userDetails.PhoneNumber = dto.PhoneNumber;
            var result = await userMenager.UpdateAsync(userDetails);
            if (result.Succeeded)
            {
                return Results.Ok(new { FirstName = userDetails.Name, LastName = userDetails.SurName, Email = userDetails.Email, PhoneNumber = userDetails?.PhoneNumber });
            }
            else
            {
                return Results.BadRequest(result.Errors);
            }
        }


        [Authorize]
        public static async Task<IResult> GetUserProfile(ClaimsPrincipal user,
            UserManager<AppUser> userMenager)
        {
            string userId = user.Claims.First(x => x.Type == "UserID").Value;
            var userDetails = await userMenager.FindByIdAsync(userId);
            return Results.Ok(new { PersonalProfile = userDetails.PersonalProfile });



        }

        [Authorize]
        public static async Task<IResult> EditUserPersonalProfile(ClaimsPrincipal user,
            UserManager<AppUser> userMenager, [FromBody] EditUserPersonalProfileDto dto)
        {
            string userId = user.Claims.First(x => x.Type == "UserID").Value;
            var userDetails = await userMenager.FindByIdAsync(userId);
            userDetails.PersonalProfile = dto.PersonalProfile;
            var result = await userMenager.UpdateAsync(userDetails);
            if (result.Succeeded)
            {
                return Results.Ok(new { PersonalProfile = userDetails.PersonalProfile });
            }
            else
            {
                return Results.BadRequest(result.Errors);
            }
        }

        [Authorize]
        public static async Task<IResult> GetUserContactLinks(ClaimsPrincipal user,
            UserManager<AppUser> userMenager)
        {
            string userId = user.Claims.First(x => x.Type == "UserID").Value;
            var userDetails = await userMenager.FindByIdAsync(userId);
            return Results.Ok(new { LinkedInURL = userDetails.LinkedInURL, GitHubURL = userDetails.GitHubURL });
        }

        [Authorize]
        public static async Task<IResult> EditUserContactLinks(ClaimsPrincipal user,
            UserManager<AppUser> userMenager, [FromBody] EditUserContactLinksDto dto)
        {
            string userId = user.Claims.First(x => x.Type == "UserID").Value;
            var userDetails = await userMenager.FindByIdAsync(userId);
            userDetails.LinkedInURL = dto.LinkedInURL;
            userDetails.GitHubURL = dto.GitHubURL;
            var result = await userMenager.UpdateAsync(userDetails);
            if (result.Succeeded)
            {
                return Results.Ok(new { LinkedInURL = userDetails.LinkedInURL, GitHubURL = userDetails.GitHubURL });
            }
            else
            {
                return Results.BadRequest(result.Errors);
            }
        }
    }
}
