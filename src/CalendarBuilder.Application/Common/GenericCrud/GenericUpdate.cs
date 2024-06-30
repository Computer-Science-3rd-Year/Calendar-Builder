using System.Text.Json;
using CalendarBuilder.Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CalendarBuilder.Application.Common.GenericCrud
{
    public class GenericUpdateCommand<Entity> : IRequest<Entity> where Entity : BaseEntity 
    {
        public required JsonElement UpdateModel { get; set; }
        public Guid Id { get; set; }
    }

    public class GenericUpdateCommandHandler<Entity> : IRequestHandler<GenericUpdateCommand<Entity>,Entity>
        where Entity : BaseEntity, new()
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<GenericUpdateCommandHandler<Entity>> _logger;
        public GenericUpdateCommandHandler(IApplicationDbContext context, ILogger<GenericUpdateCommandHandler<Entity>> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<Entity> Handle(GenericUpdateCommand<Entity> request, CancellationToken cancellationToken)
        {
            var entity = await _context.GetDbSet<Entity>().FirstOrDefaultAsync(x => x.Id == request.Id);
            entity.Update<Entity>(request.UpdateModel);  
            var newEntity = _context.GetDbSet<Entity>().Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return newEntity.Entity;
        }
    }
}