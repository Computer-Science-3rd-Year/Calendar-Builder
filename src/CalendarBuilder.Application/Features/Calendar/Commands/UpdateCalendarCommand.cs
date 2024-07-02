
using CalendarBuilder.Application.Common.GenericCrud;
using Microsoft.Extensions.Logging;

public class UpdateCalendarCommand : GenericUpdateCommand<Calendar>{}
public class UpdateCalendarCommandH : GenericUpdateCommandHandler<Calendar>
{
    public UpdateCalendarCommandH(IApplicationDbContext context, ILogger<GenericUpdateCommandHandler<Calendar>> logger) : base(context, logger)
    {
    }
}