
using CalendarBuilder.Application.Common.GenericCrud;
using Microsoft.Extensions.Logging;

public class UpdateCoincidenceRestrictionCommand : GenericUpdateCommand<CoincidenceRestriction>{}
public class UpdateCoincidenceRestrictionCommandH : GenericUpdateCommandHandler<CoincidenceRestriction>
{
    public UpdateCoincidenceRestrictionCommandH(IApplicationDbContext context, ILogger<GenericUpdateCommandHandler<CoincidenceRestriction>> logger) : base(context, logger)
    {
    }
}