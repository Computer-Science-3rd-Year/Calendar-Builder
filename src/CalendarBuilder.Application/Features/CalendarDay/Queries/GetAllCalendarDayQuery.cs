using CalendarBuilder.Application.Common.GenericCrud;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CalendarBuilder.Application.Features.CalendarDays.Queries
{
    public class GetAllCalendarDayQuery : GenericGetQuery<CalendarDay> { }
    public class GetAllCalendarDayQueryHandler : GenericGetQueryHandler<CalendarDay>
    {
        private readonly IApplicationDbContext _context;

        public GetAllCalendarDayQueryHandler(
            IApplicationDbContext context,
            ILogger<GenericGetQueryHandler<CalendarDay>> logger)
         : base(
            context,
            logger
        )
        {
            _context = context;
        }

        public override async Task<List<CalendarDay>> Handle(GenericGetQuery<CalendarDay> request, CancellationToken cancellationToken)
        {
            var result = await _context.CalendarDays
                .OrderBy(x => x.Date)                
                .ToListAsync(cancellationToken);   
            return result;            
        }
    }
}