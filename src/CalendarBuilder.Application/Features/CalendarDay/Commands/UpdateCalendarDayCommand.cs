
using CalendarBuilder.Application.Common.GenericCrud;
using Microsoft.Extensions.Logging;

public class UpdateCalendarDayCommand : GenericUpdateCommand<CalendarDay>{}
public class UpdateCalendarDayCommandH : GenericUpdateCommandHandler<CalendarDay>
{
    public UpdateCalendarDayCommandH(IApplicationDbContext context, ILogger<GenericUpdateCommandHandler<CalendarDay>> logger) : base(context, logger)
    {
    }
}