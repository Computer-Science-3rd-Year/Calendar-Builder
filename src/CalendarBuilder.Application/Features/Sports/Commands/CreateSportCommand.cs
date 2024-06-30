
using CalendarBuilder.Application.Common.GenericCrud;
using Microsoft.Extensions.Logging;

public class CreateSportCommand : GenericCreateCommand<Sport>{}
public class CreateSportCommandH : GenericCreateCommandHandler<Sport>
{
    public CreateSportCommandH(IApplicationDbContext context, ILogger<GenericCreateCommandHandler<Sport>> logger) : base(context, logger)
    {
    }
}