
using CalendarBuilder.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CalendarBuilder.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["Environment"] == "Develop" ?
                    configuration.GetConnectionString("DevelopConnectionStrings") :
                    configuration.GetConnectionString("ProductionConnectionStrings");
            services.AddMediatR(conf => conf.RegisterServicesFromAssembly(typeof(IApplicationDbContext).Assembly));

            services.AddDbContext<CalendarBuilderDbContext>(options =>
                options.UseNpgsql(connectionString)
            );
            services.AddScoped<RandomGenerableFactory>();             
            services.AddScoped<IGeneticApproachWrapper, GeneticApproachWrapper>(); 
            services.AddScoped<IApplicationDbContext, CalendarBuilderDbContext>();
            return services;
        }
    }
}