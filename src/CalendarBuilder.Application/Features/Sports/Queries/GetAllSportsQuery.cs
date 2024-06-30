using CalendarBuilder.Application.Common.GenericCrud;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CalendarBuilder.Application.Features.Sports.Queries
{

    public class GetAllSportQuery : GenericGetQuery<Sport>{}
    public class GetAllSportQueryHandler : GenericGetQueryHandler<Sport>
    {
        public GetAllSportQueryHandler(IApplicationDbContext context, ILogger<GenericGetQueryHandler<Sport>> logger) : base(context, logger)
        {
            
        }
    }
}