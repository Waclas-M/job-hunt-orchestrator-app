using JHOP.Controllers;
using JHOP.Extentions;
using JHOP.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddSwaggerExplorer()
    .InjectDbContext(builder.Configuration)
    .AddIdenetityHandlersAndStores()
    .ConfigureIdentityOptions()
    .AddIdentityAuth(builder.Configuration);



var app = builder.Build();

app.ConfigureCORS(builder.Configuration);



// --- SEKCJA MIGRACJI ---
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>(); // Podstaw swoj� nazw�

        // Opcjonalnie: Czekanie na baz� (prosty retry)
        int retryCount = 0;
        while (retryCount < 10)
        {
            try
            {
                Console.WriteLine("Pr�ba na�o�enia migracji...");
                context.Database.Migrate();
                Console.WriteLine("Migracje na�o�one pomy�lnie.");
                break;
            }
            catch (Exception ex)
            {
                retryCount++;
                Console.WriteLine($"B��d: {ex.Message}");
                Thread.Sleep(5000);
            }
        }
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Wyst�pi� b��d podczas migrowania bazy danych.");
    }
}
// --- KONIEC SEKCJI MIGRACJI ---









// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}







/// Do uruchomienia po�niej jak b�d� certyfikaty.
//app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapGroup("/api")
   .MapIdentityApi<AppUser>()
   .WithMetadata(new AllowAnonymousAttribute());

app.MapGroup("/api")
    .MapIdentityUserEndpoints(builder.Configuration)
    .MapCvProcessEndPoints()
    .MapCvFilesEndPoints()
    .MapTechinicalTokenEndpoints(builder.Configuration)
    .MapProfileEndPoints()
    .MapLaborMarketOfferApiEndPoints();

app.Run();


