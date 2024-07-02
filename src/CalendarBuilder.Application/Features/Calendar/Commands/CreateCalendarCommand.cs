
using CalendarBuilder.Application.Common.GenericCrud;
using Microsoft.Extensions.Logging;

public class CreateCalendarCommand : GenericCreateCommand<Calendar>{}
public class CreateCalendarCommandH : GenericCreateCommandHandler<Calendar>
{
    public CreateCalendarCommandH(IApplicationDbContext context, ILogger<GenericCreateCommandHandler<Calendar>> logger) : base(context, logger)
    {
    }
}