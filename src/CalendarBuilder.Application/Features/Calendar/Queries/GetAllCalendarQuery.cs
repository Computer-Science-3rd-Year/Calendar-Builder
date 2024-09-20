using CalendarBuilder.Application.Common.GenericCrud;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CalendarBuilder.Application.Features.Calendars.Queries
{
    public class GetAllCalendarQuery : GenericGetQuery<Calendar> { }
    public class GetAllCalendarQueryHandler : GenericGetQueryHandler<Calendar>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<GenericGetQueryHandler<Calendar>> _logger;

        public GetAllCalendarQueryHandler(
            IApplicationDbContext context,
            ILogger<GenericGetQueryHandler<Calendar>> logger)
         : base(
            context,
            logger, 
            new List<System.Linq.Expressions.Expression<Func<Calendar, object>>>(){
                x => x.CalendarDays
            }
        )
        {
            _context = context;
            _logger = logger;
        }
        public override async Task<List<Calendar>> Handle(GenericGetQuery<Calendar> request, CancellationToken cancellationToken)
        {
            var result = await _context.Calendars
                .Include(x => x.CalendarDays)
                    .ThenInclude(x => x.MorningSessionSport)
                .Include(x => x.CalendarDays)
                    .ThenInclude(x => x.AfterNoonSessionSport)                
                .ToListAsync(cancellationToken);   
            return result;            
        }
    }
}