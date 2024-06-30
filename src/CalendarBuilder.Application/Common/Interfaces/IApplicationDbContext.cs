using Microsoft.EntityFrameworkCore;

public interface IApplicationDbContext
{
    DbSet<T> GetDbSet<T>() where T : class; 
    DbSet<Sport> Sports { get; set; }
    Task SaveChangesAsync(CancellationToken cancellationToken);
}