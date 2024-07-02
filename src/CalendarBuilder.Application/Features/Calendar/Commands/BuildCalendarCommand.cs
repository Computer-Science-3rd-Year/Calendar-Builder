using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CalendarBuilder.Application.Common.GenericCrud
{
    public class BuildCalendarCommand : IRequest<bool> 
    {
        public Guid Id { get; set; }
    }

    public class BuildCalendarCommandHandler : IRequestHandler<BuildCalendarCommand,bool>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<BuildCalendarCommandHandler> _logger;
        private readonly IGeneticApproachWrapper _geneticWrapper;

        public BuildCalendarCommandHandler(IApplicationDbContext context, ILogger<BuildCalendarCommandHandler> logger,IGeneticApproachWrapper geneticWrapper )
        {
            _context = context;
            _logger = logger;
            _geneticWrapper = geneticWrapper;
        }
        public virtual async Task<bool> Handle(BuildCalendarCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Build calendar handler called with id: "+ request.Id);
            var calendar = await _context.Calendars
                .Include(x => x.CalendarDays)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (calendar == null)
                throw new Exception("Calendar not found"); 
            calendar.Status = CalendarStatus.Building; 
            await _context.SaveChangesAsync(cancellationToken); 
            var solution = (await _geneticWrapper.Evolution(calendar.Id)).First();
            for (int i = 0; i < calendar.CalendarDays.Count; i++)
            {
                var next = solution.Solution[i].Value as CalendarDay; 
                var day = calendar.CalendarDays[i]; 
                day.MorningSessionSport = next?.MorningSessionSport; 
                day.MorningSessionSportId = next?.MorningSessionSportId; 
                day.AfterNoonSessionSport = next?.AfterNoonSessionSport; 
                day.AfterNoonSessionSportId = next?.AfterNoonSessionSportId; 
                _context.CalendarDays.Update(day); 
            }
            calendar.Status = CalendarStatus.Completed; 
            await _context.SaveChangesAsync(cancellationToken); 
            return true; 
        }
    }
}