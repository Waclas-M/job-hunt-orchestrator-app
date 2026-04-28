namespace JHOP.Extentions
{
    public static class AppConfigExtentions
    {

        public static WebApplication ConfigureCORS(this WebApplication app, IConfiguration config)
        {
            app.UseCors(options => options.WithOrigins(["http://localhost:4200", "http://localhost:5000"])
            .AllowAnyMethod()
            .AllowAnyHeader());


            return app;
        }

    }
}
