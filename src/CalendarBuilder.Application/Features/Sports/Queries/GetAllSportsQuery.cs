using CalendarBuilder.Application.Common.GenericCrud;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CalendarBuilder.Application.Features.Sports.Queries
{
    public class GetAllSportsQuery : IRequest<List<Sport>> { }

    public class GetAllSportsQueryHandler : IRequestHandler<GetAllSportsQuery, List<Sport>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<GetAllSportsQueryHandler> _logger;

        public GetAllSportsQueryHandler(IApplicationDbContext context, ILogger<GetAllSportsQueryHandler> logger)
        {
            _context = context;
            _logger = logger;
        }
        async Task<List<Sport>> IRequestHandler<GetAllSportsQuery, List<Sport>>.Handle(GetAllSportsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Here");
            return await _context.Sports.ToListAsync();
        }
    }
    public class Test : GenericGetQuery<Sport>{}
    public class TestH : GenericGetQueryHandler<Sport>
    {
        public TestH(IApplicationDbContext context, ILogger<GenericGetQueryHandler<Sport>> logger) : base(context, logger)
        {
            
        }
    }
}