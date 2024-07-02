using GeneticApproach;
using GeneticApproach.Domain;
using GeneticApproach.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CalendarBuilder.Infrastructure.Services
{
    public class RandomGenerableFactory : IRandomGenerableFactory<CalendarDay>
    {
        public Guid CalendarId { get; set; }
        private readonly IApplicationDbContext _context;

        public RandomGenerableFactory(IApplicationDbContext context)
        {
            _context = context;
        }

        public void Draw(CalendarDay target)
        {
            // pass
        }

        public CalendarDay GenerateRandom()
        {
            return Task.Run<CalendarDay>(async () => {
                var calendar = await _context.Calendars
                    .Include(x => x.CalendarDays)
                    .Where(x => x.Id == CalendarId)
                    .FirstAsync();
                var sports = await _context.Sports
                    .ToListAsync();
                sports.Add(null); 
                var rand = new Random();
                int index = rand.Next(0,calendar.CalendarDays.Count); 
                var calendarDay = calendar.CalendarDays[index];
                calendarDay.MorningSessionSport = sports[rand.Next(0,sports.Count)];
                calendarDay.AfterNoonSessionSport = sports[rand.Next(0,sports.Count)];
                
                calendarDay.MorningSessionSportId = sports[rand.Next(0,sports.Count)]?.Id ?? Guid.Empty;
                calendarDay.AfterNoonSessionSportId = sports[rand.Next(0,sports.Count)]?.Id ?? Guid.Empty;

                return calendarDay;
            }).Result;
        }
    }

    public class CalendarFactory : IChromosomeFactory<CalendarDay, Calendar>
    {
        private readonly RandomGenerableFactory _randomFactory;

        public CalendarFactory(RandomGenerableFactory randomFactory)
        {
            _randomFactory = randomFactory;
        }
        public Calendar ToBase(Chromosome<CalendarDay> chromosome)
        {
            Calendar can = new Calendar(); 
            var days = chromosome.Genes.Select(x => x as CalendarDay).ToList();
            can.CalendarDays = days; 
            return can; 
        }

        public Chromosome<CalendarDay> ToChromosome(Calendar @base)
        {
            return new Chromosome<CalendarDay>(@base.CalendarDays.ToList(),_randomFactory);            
        }
    }
    public class GeneticApproachWrapper : GeneticService<CalendarDay, Calendar>, IGeneticApproachWrapper
    {
        private readonly IApplicationDbContext _context;
        private readonly RandomGenerableFactory _randomFactory;
        private Guid _calendarId;
        public Guid CalendarId
        {
            get { 
                if(_calendarId == Guid.Empty)
                    throw new Exception("Calendar Id not set.");
                return _calendarId;
            }
            set {
                _randomFactory.CalendarId = value;  
                _calendarId = value;
            }
        }

        public GeneticApproachWrapper(IApplicationDbContext context, RandomGenerableFactory randomFactory)
        {
            _context = context;
            _randomFactory = randomFactory;
        }
        public async Task<List<BaseConstraint<Calendar>>> GetConstraints(CancellationToken cancellationToken)
        {
            List<BaseConstraint<Calendar>> cons = new List<BaseConstraint<Calendar>>();

            var coincidenceRestrictions = await _context.CoincidenceRestrictions
                .Where(x => x.CalendarId == CalendarId && x.IsActive)
                .ToListAsync(cancellationToken); 

            foreach (var con in coincidenceRestrictions)
            {
                cons.Add(new BaseConstraint<Calendar>(con.Id.ToString(), 
                    cal => {
                        double result = 0;
                        var list = cal.SportIds().ToList();

                        for (int i = 0; i < list.Count; i++)
                        {
                            for (int j = i + 1; j < list.Count; j++)
                            {
                                if (list[i] == con.FirstSportId && list[j] == con.SecondSportId && Math.Abs(i - j) <= con.SessionsGap)
                                {
                                    result += 5;
                                }
                            }
                        }

                        return result; 
                    } 
                )); 
            }

            var quantityRestrictions = await _context.QuantityRestrictions
                .Where(x => x.CalendarId == CalendarId && x.IsActive)
                .ToListAsync(cancellationToken); 

            
            foreach (var con in quantityRestrictions)
            {
                cons.Add(new BaseConstraint<Calendar>(con.Id.ToString(), 
                    cal => {
                        double result = 0;
                        var count = 0 ; 
                        var list = cal.SportIds().ToList();
                        foreach (var item in list)
                        {
                            if(item == con.SportId)
                                count++;
                        }
                        result = count == con.Quantity ? 0 : 20; 
                        return result;                        
                    } 
                )); 
            }

            return cons;
        }

        async Task<IEnumerable<GeneticResults>> IGeneticApproachWrapper.Evolution(Guid calendarId){
            this.CalendarId = calendarId; 
            var cons = await GetConstraints(default);  
            var fac = new CalendarFactory(_randomFactory); 
            
             var calendar = await _context.Calendars
                .Include(x => x.CalendarDays)
                .Where(x => x.Id == CalendarId)
                .FirstAsync();
           
            var options = new GeneticOptions(){
                ChromosomeLength = calendar.CalendarDays.Count
            };
            
            var list = base.Evolution(
                cons,
                options,
                fac,
                _randomFactory
            );
            return list; 
        } 
    }
}