using System.Linq.Expressions;
using CalendarBuilder.Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;

namespace CalendarBuilder.Application.Common.GenericCrud
{
    public class GenericGetQuery<Entity> : IRequest<List<Entity>> { }

    public class GenericGetQueryHandler<Entity> : IRequestHandler<GenericGetQuery<Entity>, List<Entity>>
        where Entity : BaseEntity
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<GenericGetQueryHandler<Entity>> _logger;
        private readonly IEnumerable<Expression<Func<Entity, object>>> _includes;

        public GenericGetQueryHandler(IApplicationDbContext context, ILogger<GenericGetQueryHandler<Entity>> logger, List<Expression<Func<Entity, object>>>? includes = null)
        {
            _context = context;
            _logger = logger;
            _includes = includes ?? new List<Expression<Func<Entity, object>>>();
        }
        public async virtual  Task<List<Entity>> Handle(GenericGetQuery<Entity> request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Called generic GET query handler."+ typeof(Entity).Name); 
            var dbSet = _context.GetDbSet<Entity>();
            IQueryable<Entity> query = dbSet; 
            foreach (var includeStatement in _includes)
            {
                query = query.Include(includeStatement);    
            }
            return await query.ToListAsync(cancellationToken);            
        }
    }
}