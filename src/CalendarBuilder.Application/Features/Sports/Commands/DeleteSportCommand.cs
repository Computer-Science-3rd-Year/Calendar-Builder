
using CalendarBuilder.Application.Common.GenericCrud;
using Microsoft.Extensions.Logging;

public class RemoveSportCommand : GenericRemoveCommand<Sport>{}
public class RemoveSportCommandH : GenericRemoveCommandHandler<Sport>
{
    public RemoveSportCommandH(IApplicationDbContext context, ILogger<GenericRemoveCommandHandler<Sport>> logger) : base(context, logger)
    {
    }
}