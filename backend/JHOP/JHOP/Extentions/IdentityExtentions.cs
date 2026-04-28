using JHOP.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;

namespace JHOP.Extentions
{
    public static class IdentityExtentions
    {
        public static IServiceCollection AddIdenetityHandlersAndStores(this IServiceCollection services)
        {
            services
                .AddIdentityApiEndpoints<AppUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();
            return services;
        }

        public static IServiceCollection ConfigureIdentityOptions(this IServiceCollection services)
        {
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.User.RequireUniqueEmail = true;
            });

            return services;
        }

        public static IServiceCollection AddIdentityAuth(this IServiceCollection services, IConfiguration config)
        {

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(y =>
                {
                    y.SaveToken = false;
                    y.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(config["AppSettings:JWTSecret"]!)),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                    };
                });
            services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build();

            });



            return services;
        }

    }
}
