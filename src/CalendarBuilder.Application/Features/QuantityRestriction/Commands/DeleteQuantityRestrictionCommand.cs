
using CalendarBuilder.Application.Common.GenericCrud;
using Microsoft.Extensions.Logging;

public class RemoveQuantityRestrictionCommand : GenericRemoveCommand<QuantityRestriction>{}
public class RemoveQuantityRestrictionCommandH : GenericRemoveCommandHandler<QuantityRestriction>
{
    public RemoveQuantityRestrictionCommandH(IApplicationDbContext context, ILogger<GenericRemoveCommandHandler<QuantityRestriction>> logger) : base(context, logger)
    {
    }
}