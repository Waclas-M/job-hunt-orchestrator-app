using JHOP.Enums;
using JHOP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace JHOP.Controllers
{
    public static class LaborMarketOfferApiEndPoints
    {
        public static IEndpointRouteBuilder MapLaborMarketOfferApiEndPoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/GetLaborMarketOffersByCategory/{categorie:int}", GetLaborMarketOffersByCategorie);
            return app;
        }


        public static async Task<IResult> GetLaborMarketOffersByCategorie(
        ClaimsPrincipal user,
        int categorie,
        [FromServices] AppDbContext db)
        {
            try
            {
                var offers = await db.laborMarketOffers.Where(x => x.Categorie == (Enums.LaborOfferCategorie)categorie).ToListAsync();

                return Results.Ok(offers);
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

    }
}
