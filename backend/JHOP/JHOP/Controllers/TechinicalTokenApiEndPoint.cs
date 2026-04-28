using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JHOP.Controllers
{
    public static class TechinicalTokenApiEndPoint
    {
        private static IConfiguration Config { get; set; }
        public static IEndpointRouteBuilder MapTechinicalTokenEndpoints(this IEndpointRouteBuilder app, IConfiguration config)
        {
            Config = config;
            app.MapPost("/service-token", (HttpContext ctx) =>
            {
                // 1) sprawdzenie API key
                var apiKeyHeader = ctx.Request.Headers["X-API-KEY"].ToString();
                var validApiKey = Config["ServiceAuth:ApiKey"];

                if (string.IsNullOrWhiteSpace(validApiKey) || apiKeyHeader != validApiKey)
                    return Results.Unauthorized();

                // 2) budowa claims jak u Ciebie
                var claimsList = new List<Claim>
                {
                    // nie udajemy usera -> brak UserID
                    new Claim("ServiceName", "PythonUploader"),
                    new Claim(ClaimTypes.Role, "Service")
                };

                var siginkey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(Config["AppSettings:JWTSecret"]!)
                );

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claimsList),
                    Expires = DateTime.UtcNow.AddMinutes(120), // tak jak u Ciebie
                    SigningCredentials = new SigningCredentials(siginkey, SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);

                return Results.Ok(new { token });
            }).AllowAnonymous();
            return app;
        }

    }
}
