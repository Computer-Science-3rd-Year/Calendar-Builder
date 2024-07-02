using CalendarBuilder.Application.Common.GenericCrud;
using Microsoft.Extensions.Logging;

namespace CalendarBuilder.Application.Features.CalendarDays.Queries
{
    public class GetAllCalendarDayQuery : GenericGetQuery<CalendarDay> { }
    public class GetAllCalendarDayQueryHandler : GenericGetQueryHandler<CalendarDay>
    {
        public GetAllCalendarDayQueryHandler(
            IApplicationDbContext context,
            ILogger<GenericGetQueryHandler<CalendarDay>> logger)
         : base(
            context,
            logger
        )
        {}
    }
}