
using CalendarBuilder.Application.Common.GenericCrud;
using Microsoft.Extensions.Logging;

public class CreateCoincidenceRestrictionCommand : GenericCreateCommand<CoincidenceRestriction>{}
public class CreateCoincidenceRestrictionCommandH : GenericCreateCommandHandler<CoincidenceRestriction>
{
    public CreateCoincidenceRestrictionCommandH(IApplicationDbContext context, ILogger<GenericCreateCommandHandler<CoincidenceRestriction>> logger) : base(context, logger)
    {
    }
}