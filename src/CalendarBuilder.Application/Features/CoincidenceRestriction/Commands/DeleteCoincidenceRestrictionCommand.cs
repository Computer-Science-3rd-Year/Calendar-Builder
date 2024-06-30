
using CalendarBuilder.Application.Common.GenericCrud;
using Microsoft.Extensions.Logging;

public class RemoveCoincidenceRestrictionCommand : GenericRemoveCommand<CoincidenceRestriction>{}
public class RemoveCoincidenceRestrictionCommandH : GenericRemoveCommandHandler<CoincidenceRestriction>
{
    public RemoveCoincidenceRestrictionCommandH(IApplicationDbContext context, ILogger<GenericRemoveCommandHandler<CoincidenceRestriction>> logger) : base(context, logger)
    {
    }
}