
using CalendarBuilder.Application.Common.GenericCrud;
using Microsoft.Extensions.Logging;

public class RemoveCalendarDayCommand : GenericRemoveCommand<CalendarDay>{}
public class RemoveCalendarDayCommandH : GenericRemoveCommandHandler<CalendarDay>
{
    public RemoveCalendarDayCommandH(IApplicationDbContext context, ILogger<GenericRemoveCommandHandler<CalendarDay>> logger) : base(context, logger)
    {
    }
}