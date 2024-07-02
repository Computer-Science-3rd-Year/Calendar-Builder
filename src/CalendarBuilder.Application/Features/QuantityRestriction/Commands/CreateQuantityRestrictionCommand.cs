
using CalendarBuilder.Application.Common.GenericCrud;
using Microsoft.Extensions.Logging;

public class CreateQuantityRestrictionCommand : GenericCreateCommand<QuantityRestriction>{}
public class CreateQuantityRestrictionCommandH : GenericCreateCommandHandler<QuantityRestriction>
{
    public CreateQuantityRestrictionCommandH(IApplicationDbContext context, ILogger<GenericCreateCommandHandler<QuantityRestriction>> logger) : base(context, logger)
    {
    }
}