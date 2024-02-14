using DomainLayer.Imterface;
using InfrastructureLayer.AppDbContext;
using InfrastructureLayer.Repos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InfrastructureLayer
{
    public static class ConfigService
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection service, IConfiguration config)
        {
            service.AddDbContext<DataContext>(o =>
            {
                o.UseSqlServer(
                    config.GetConnectionString("DefaultConnection"),
                    x => x.MigrationsAssembly("InfrastructureLayer")
                );
            });

            service.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            service.AddScoped<IUnitOfWork, UnitOfWork>();

            return service;
        }
    }
}
    