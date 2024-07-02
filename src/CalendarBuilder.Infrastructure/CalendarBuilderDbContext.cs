using System.Reflection;
using Microsoft.EntityFrameworkCore;
namespace CalendarBuilder.Infrastructure;

public class CalendarBuilderDbContext : DbContext, IApplicationDbContext  
{
    public CalendarBuilderDbContext(DbContextOptions<CalendarBuilderDbContext> options): base(options){}
    public DbSet<Sport> Sports { get; set; }
    public DbSet<CoincidenceRestriction> CoincidenceRestrictions { get; set; }
    public DbSet<QuantityRestriction> QuantityRestrictions { get; set; }

    public DbSet<CalendarDay> CalendarDays { get; set; }

    public DbSet<Calendar> Calendars { get; set; }

    Task IApplicationDbContext.SaveChangesAsync(CancellationToken cancellationToken)
    {
        // TODO: dispatch events and so on (DDD stuff) 
        return base.SaveChangesAsync(cancellationToken);
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public DbSet<T> GetDbSet<T>() where T : class
    {
        var nameType = typeof(T).Name;
        var t = typeof(T);
        
        return nameType switch
        {
            nameof(Sport) => (Sports as DbSet<T>)!,
            nameof(CoincidenceRestriction) => (CoincidenceRestrictions as DbSet<T>)!,
            nameof(CalendarDay) => (CalendarDays as DbSet<T>)!,
            nameof(Calendar) => (Calendars as DbSet<T>)!,
            nameof(QuantityRestriction) => (QuantityRestrictions as DbSet<T>)!,
            _ => throw new ArgumentException("Unsupported type with name: " + nameType),
        };
    }
}
