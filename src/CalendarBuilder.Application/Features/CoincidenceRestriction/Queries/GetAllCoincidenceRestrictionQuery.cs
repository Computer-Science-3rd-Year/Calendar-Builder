using System.Linq.Expressions;
using CalendarBuilder.Application.Common.GenericCrud;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;

namespace CalendarBuilder.Application.Features.CoincidenceRestrictions.Queries
{
    public class GetAllCoincidenceRestrictionQuery : GenericGetQuery<CoincidenceRestriction> { }
    public class GetAllCoincidenceRestrictionQueryHandler : GenericGetQueryHandler<CoincidenceRestriction>
    {
        public GetAllCoincidenceRestrictionQueryHandler(
            IApplicationDbContext context,
            ILogger<GenericGetQueryHandler<CoincidenceRestriction>> logger)
         : base(
            context,
            logger,
            includes: 
            new List<Expression<Func<CoincidenceRestriction, object>>>(){
                x => x.FirstSport,
                x => x.SecondSport
            }
        )
        {}
    }
}