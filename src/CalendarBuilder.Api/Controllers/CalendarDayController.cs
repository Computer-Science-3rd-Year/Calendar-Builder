using CalendarBuilder.Application.Common.GenericCrud;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CalendarBuilder.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalendarDaysController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CalendarDaysController> _logger;

        public CalendarDaysController(IMediator mediator, ILogger<CalendarDaysController> logger)
        {
            _mediator = mediator;
            _logger = logger;
            _logger.LogInformation("CalendarDays controller mapped");
        }

        [HttpGet]
        public async Task<List<CalendarDay>> GetAllCalendarDays()
        {
            _logger.LogInformation("GetAllCalendarDays endpoint called.");
            return await _mediator.Send(new GenericGetQuery<CalendarDay>());
        }

        [HttpPost]
        public async Task<CalendarDay> Create(GenericCreateCommand<CalendarDay> input)
        {
            _logger.LogInformation("Create endpoint called.");
            return await _mediator.Send(input);
        }
        [HttpPut]
        public async Task<CalendarDay> Update(GenericUpdateCommand<CalendarDay> input)
        {
            _logger.LogInformation("Update endpoint called.");
            return await _mediator.Send(input);
        }
        
        [HttpDelete]
        public async Task<bool> Delete(GenericRemoveCommand<CalendarDay> input)
        {
            _logger.LogInformation("Remove endpoint called.");
            return await _mediator.Send(input);
        }
    }
}