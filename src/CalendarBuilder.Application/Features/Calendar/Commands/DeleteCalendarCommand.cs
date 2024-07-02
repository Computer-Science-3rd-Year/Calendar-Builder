
using CalendarBuilder.Application.Common.GenericCrud;
using Microsoft.Extensions.Logging;

public class RemoveCalendarCommand : GenericRemoveCommand<Calendar>{}
public class RemoveCalendarCommandH : GenericRemoveCommandHandler<Calendar>
{
    public RemoveCalendarCommandH(IApplicationDbContext context, ILogger<GenericRemoveCommandHandler<Calendar>> logger) : base(context, logger)
    {
    }
}