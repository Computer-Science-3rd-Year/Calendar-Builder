
using CalendarBuilder.Application.Common.GenericCrud;
using Microsoft.Extensions.Logging;

public class UpdateQuantityRestrictionCommand : GenericUpdateCommand<QuantityRestriction>{}
public class UpdateQuantityRestrictionCommandH : GenericUpdateCommandHandler<QuantityRestriction>
{
    public UpdateQuantityRestrictionCommandH(IApplicationDbContext context, ILogger<GenericUpdateCommandHandler<QuantityRestriction>> logger) : base(context, logger)
    {
    }
}