using CalendarBuilder.Application.Common.GenericCrud;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CalendarBuilder.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalendarsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CalendarsController> _logger;

        public CalendarsController(IMediator mediator, ILogger<CalendarsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
            _logger.LogInformation("Calendars controller mapped");
        }

        [HttpGet]
        public async Task<List<Calendar>> GetAllCalendars()
        {
            _logger.LogInformation("GetAllCalendars endpoint called.");
            return await _mediator.Send(new GenericGetQuery<Calendar>());
        }

        [HttpPost]
        public async Task<Calendar> Create(GenericCreateCommand<Calendar> input)
        {
            _logger.LogInformation("Create endpoint called.");
            return await _mediator.Send(input);
        }
        [HttpPut]
        public async Task<Calendar> Update(GenericUpdateCommand<Calendar> input)
        {
            _logger.LogInformation("Update endpoint called.");
            return await _mediator.Send(input);
        }
        
        [HttpDelete]
        public async Task<bool> Delete(GenericRemoveCommand<Calendar> input)
        {
            _logger.LogInformation("Remove endpoint called.");
            return await _mediator.Send(input);
        }
        [HttpPost]
        [Route("/build")]
        public async Task<bool> Build(BuildCalendarCommand input)
        {
            _logger.LogInformation("Create endpoint called.");
            return await _mediator.Send(input);
        }
 
    }
}