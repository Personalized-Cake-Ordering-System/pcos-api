using CusCake.Application;
using CusCake.Infrastructures.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CusCake.Infrastructures
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructuresService(this IServiceCollection services, AppSettings appSettings)
        {

            services.AddAutoMapper(typeof(MapperConfigurationsProfile));
            services.AddDbContext<AppDbContext>(option =>
            option.UseMySql(appSettings.ConnectionStrings.MySqlString, new MySqlServerVersion(new Version(8, 0, 30)))
            // option.UseMySql(appSettings.ConnectionStrings.MySqlStringBackUp, new MySqlServerVersion(new Version(8, 0, 30)))
            );
            return services;
        }
    }
}
