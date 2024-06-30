using CalendarBuilder.Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CalendarBuilder.Application.Common.GenericCrud
{
    public class GenericRemoveCommand<Entity> : IRequest<bool> where Entity : BaseEntity 
    {
        public required Guid Id { get; set; }
    }

    public class GenericRemoveCommandHandler<Entity> : IRequestHandler<GenericRemoveCommand<Entity>,bool>
        where Entity : BaseEntity
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<GenericRemoveCommandHandler<Entity>> _logger;
        public GenericRemoveCommandHandler(IApplicationDbContext context, ILogger<GenericRemoveCommandHandler<Entity>> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<bool> Handle(GenericRemoveCommand<Entity> request, CancellationToken cancellationToken)
        {
            var entity = await _context.GetDbSet<Entity>().FirstOrDefaultAsync(x => x.Id == request.Id);
            if (entity == null){
                throw new Exception("Entity not found with Id: "+ request.Id);
            }
            _context.GetDbSet<Entity>().Remove(entity!);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}