
using CalendarBuilder.Application.Common.GenericCrud;
using Microsoft.Extensions.Logging;

public class CreateCalendarDayCommand : GenericCreateCommand<CalendarDay>{}
public class CreateCalendarDayCommandH : GenericCreateCommandHandler<CalendarDay>
{
    public CreateCalendarDayCommandH(IApplicationDbContext context, ILogger<GenericCreateCommandHandler<CalendarDay>> logger) : base(context, logger)
    {
    }
}