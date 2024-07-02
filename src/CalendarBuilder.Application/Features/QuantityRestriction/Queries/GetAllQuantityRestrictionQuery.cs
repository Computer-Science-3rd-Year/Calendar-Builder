using System.Linq.Expressions;
using CalendarBuilder.Application.Common.GenericCrud;
using Microsoft.Extensions.Logging;

namespace CalendarBuilder.Application.Features.QuantityRestrictions.Queries
{
    public class GetAllQuantityRestrictionQuery : GenericGetQuery<QuantityRestriction> { }
    public class GetAllQuantityRestrictionQueryHandler : GenericGetQueryHandler<QuantityRestriction>
    {
        public GetAllQuantityRestrictionQueryHandler(
            IApplicationDbContext context,
            ILogger<GenericGetQueryHandler<QuantityRestriction>> logger)
         : base(
            context,
            logger,
            includes: 
            new List<Expression<Func<QuantityRestriction, object>>>(){
                x => x.Sport,
            }
        )
        {}
    }
}