using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace CalendarBuilder.Infrastructure
{
    public class CalendarBuilderDbContextFactory : IDesignTimeDbContextFactory<CalendarBuilderDbContext>
    {
        public CalendarBuilderDbContext CreateDbContext(string[] args)
        {
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "CalendarBuilder.Api");
            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<CalendarBuilderDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseNpgsql(connectionString);

            return new CalendarBuilderDbContext(optionsBuilder.Options);
        }
    }
}
