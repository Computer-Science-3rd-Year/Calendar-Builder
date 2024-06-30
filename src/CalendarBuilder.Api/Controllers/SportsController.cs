using CalendarBuilder.Application.Common.GenericCrud;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CalendarBuilder.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SportsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<SportsController> _logger;

        public SportsController(IMediator mediator, ILogger<SportsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
            _logger.LogInformation("Sports controller mapped");
        }

        [HttpGet]
        public async Task<List<Sport>> GetAllSports()
        {
            _logger.LogInformation("GetAllSports endpoint called.");
            return await _mediator.Send(new GenericGetQuery<Sport>());
        }

        [HttpPost]
        public async Task<Sport> Create(GenericCreateCommand<Sport> input)
        {
            _logger.LogInformation("Create endpoint called.");
            return await _mediator.Send(input);
        }
        [HttpPut]
        public async Task<Sport> Update(GenericUpdateCommand<Sport> input)
        {
            _logger.LogInformation("Update endpoint called.");
            return await _mediator.Send(input);
        }
        
        [HttpDelete]
        public async Task<bool> Delete(GenericRemoveCommand<Sport> input)
        {
            _logger.LogInformation("Remove endpoint called.");
            return await _mediator.Send(input);
        }
    }
}