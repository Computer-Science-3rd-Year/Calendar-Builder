using CalendarBuilder.Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CalendarBuilder.Application.Common.GenericCrud
{
    public class GenericGetQuery<Entity> : IRequest<List<Entity>> { }

    public class GenericGetQueryHandler<Entity> : IRequestHandler<GenericGetQuery<Entity>, List<Entity>>
        where Entity : BaseEntity
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<GenericGetQueryHandler<Entity>> _logger;
        public GenericGetQueryHandler(IApplicationDbContext context, ILogger<GenericGetQueryHandler<Entity>> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async virtual  Task<List<Entity>> Handle(GenericGetQuery<Entity> request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Called generic GET query handler."+ typeof(Entity).Name); 
            return await _context.GetDbSet<Entity>().ToListAsync(cancellationToken);            
        }
    }
}