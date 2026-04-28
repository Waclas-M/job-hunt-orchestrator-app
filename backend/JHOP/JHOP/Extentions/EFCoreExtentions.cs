using JHOP.Models;
using Microsoft.EntityFrameworkCore;

namespace JHOP.Extentions
{
    public static class EFCoreExtentions
    {

        public static IServiceCollection InjectDbContext(this IServiceCollection services,IConfiguration config)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(config.GetConnectionString("DevDB")));
            return services;
        }
    }
}
