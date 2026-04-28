
using JHOP.Models;
using JHOP.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace JHOP.Controllers
{
    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public static class IdentityUserEndPoints
    {
        private static IConfiguration Config { get; set; }
        public static IEndpointRouteBuilder MapIdentityUserEndpoints(this IEndpointRouteBuilder app, IConfiguration config)
        {
            Config = config;
            app.MapPost("/signin", LoginUser).AllowAnonymous();
            app.MapPost("/signup", RegisterUser).AllowAnonymous();
            return app;
        }

        [AllowAnonymous]
        public static async Task<IResult> LoginUser(
          [FromServices] UserManager<AppUser> userMenager,
          [FromBody] LoginModel loginModel
           )
        {
            var user = await userMenager.FindByEmailAsync(loginModel.Email);
            if (user != null && await userMenager.CheckPasswordAsync(user, loginModel.Password))
            {
                var roles = await  userMenager.GetRolesAsync(user);
                
                var siginkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Config["AppSettings:JWTSecret"]!));

                var claimsList = new List<Claim>
                {
                    new Claim("UserID", user.Id.ToString())
                };

                // Mapujemy stringi z roli na obiekty Claim i dodajemy do listy
                claimsList.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claimsList);


                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claimsIdentity,
                    Expires = DateTime.UtcNow.AddMinutes(120),
                    SigningCredentials = new SigningCredentials(siginkey, SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                return Results.Ok(new { token });
            }
            else
                return Results.BadRequest(new { message = "Username or password is incorrect." });

            

        }

        [AllowAnonymous]
        public static async Task<IResult> RegisterUser(
          [FromServices] UserManager<AppUser> userMenager,
          [FromBody] RegistrationModel registerModel,
            [FromServices] AppDbContext db
           )
        {
            try
            {
                AppUser user = new AppUser
                {
                    UserName = registerModel.Email,
                    Email = registerModel.Email,
                    Name = registerModel.Name,
                    SurName = registerModel.SurName
                };

                // 1) Create user
                var createResult = await userMenager.CreateAsync(user, registerModel.Password);
                if (!createResult.Succeeded)
                    return Results.BadRequest(createResult.Errors);

                // 2) Add role
                var roleResult = await userMenager.AddToRoleAsync(user, "User");
                if (!roleResult.Succeeded)
                    return Results.BadRequest(roleResult.Errors);

                // 3) Create default profile
                var profile = new Profile
                {
                    UserId = user.Id,
                    ProfileIndex = 1,
                    Name = "Profil1"
                };

                db.Profiles.Add(profile);
                await db.SaveChangesAsync();

                // 4) Add PersonalData to UserProfile 1
                var profileDb = await db.Profiles.Where(x => x.UserId == user.Id).FirstAsync();
                var personalData = new UserProfilePersonalData
                {
                    FirstName = registerModel.Name,
                    LastName = registerModel.SurName,
                    Email = registerModel.Email,
                    ProfileId = profileDb.Id,

                };
                db.UserProfilePersonalData.Add(personalData);

                await db.SaveChangesAsync();
                return Results.Ok(new { userId = user.Id, profileId = profile.Id });
            }catch (Exception e){
                return Results.NotFound("Błąd przy tworzeniu użytkownika");
            }
        }


    }
}
