using Microsoft.EntityFrameworkCore;

public interface IApplicationDbContext
{
    DbSet<T> GetDbSet<T>() where T : class; 
    DbSet<Sport> Sports { get; set; }
    DbSet<CoincidenceRestriction> CoincidenceRestrictions { get; set; }
    DbSet<CalendarDay> CalendarDays { get; set; }
    DbSet<Calendar> Calendars { get; set; }
    DbSet<QuantityRestriction> QuantityRestrictions { get; set; }
    Task SaveChangesAsync(CancellationToken cancellationToken);
}