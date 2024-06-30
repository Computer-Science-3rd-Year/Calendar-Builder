
using CalendarBuilder.Application.Common.GenericCrud;
using Microsoft.Extensions.Logging;

public class UpdateSportCommand : GenericUpdateCommand<Sport>{}
public class UpdateSportCommandH : GenericUpdateCommandHandler<Sport>
{
    public UpdateSportCommandH(IApplicationDbContext context, ILogger<GenericUpdateCommandHandler<Sport>> logger) : base(context, logger)
    {
    }
}