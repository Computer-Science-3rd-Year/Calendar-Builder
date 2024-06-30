using System.Text.Json;
using CalendarBuilder.Domain.Common;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CalendarBuilder.Application.Common.GenericCrud
{
    public class GenericCreateCommand<Entity> : IRequest<Entity> where Entity : BaseEntity 
    {
        public required JsonElement CreateModel { get; set; }
    }

    public class GenericCreateCommandHandler<Entity> : IRequestHandler<GenericCreateCommand<Entity>,Entity>
        where Entity : BaseEntity, new()
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<GenericCreateCommandHandler<Entity>> _logger;
        public GenericCreateCommandHandler(IApplicationDbContext context, ILogger<GenericCreateCommandHandler<Entity>> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<Entity> Handle(GenericCreateCommand<Entity> request, CancellationToken cancellationToken)
        {
            var entity = new Entity();
            entity.Initialize<Entity>(request.CreateModel);  
            var newEntity = _context.GetDbSet<Entity>().Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return newEntity.Entity;
        }
    }
}