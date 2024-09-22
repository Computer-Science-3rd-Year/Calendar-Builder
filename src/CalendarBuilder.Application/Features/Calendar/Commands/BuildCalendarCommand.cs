using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CalendarBuilder.Application.Common.GenericCrud
{
    public class BuildCalendarCommand : IRequest<bool> 
    {
        public Guid Id { get; set; }
    }

    public class BuildCalendarCommandHandler : IRequestHandler<BuildCalendarCommand, bool>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<BuildCalendarCommandHandler> _logger;
        private readonly IGeneticApproachWrapper _geneticWrapper;

        public BuildCalendarCommandHandler(IApplicationDbContext context, ILogger<BuildCalendarCommandHandler> logger, IGeneticApproachWrapper geneticWrapper)
        {
            _context = context;
            _logger = logger;
            _geneticWrapper = geneticWrapper;
        }

        public virtual async Task<bool> Handle(BuildCalendarCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Build calendar handler called with id: {CalendarId}", request.Id);
            var calendar = await _context.Calendars
                .AsNoTracking()
                .Include(x => x.CalendarDays)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (calendar == null)
            {
                _logger.LogWarning("Calendar with id {CalendarId} not found", request.Id);
                throw new Exception("Calendar not found");
            }
            var solution = await _geneticWrapper.Evolution(calendar.Id);

            for (int i = 0; i < calendar.CalendarDays.Count; i++)
            {
                var next = solution.Solution[i].Value as CalendarDay;
                var day = await _context.CalendarDays.FirstOrDefaultAsync(x => x.Id ==  calendar.CalendarDays[i].Id,cancellationToken);
                
                next!.Date = day!.Date; 
                next.CalendarId = day.CalendarId;

                next.MorningSessionSportId = next.MorningSessionSportId == Guid.Empty ? null : next.MorningSessionSportId ;
                next.AfterNoonSessionSportId = next.AfterNoonSessionSportId == Guid.Empty ? null : next.AfterNoonSessionSportId ;
                
                _context.CalendarDays.Remove(day); 
                _context.CalendarDays.Add(next); 
            }
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
/*
    Disclaimer: this could be a lot more improved, but this version of entity framework reports a
    bug for tracking multiple entities to be updated, so in order to find the simplest solution, 
    we are just deleting old calendar days and creating new ones
*/