using CalendarBuilder.Application.Common.GenericCrud;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CalendarBuilder.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CoincidenceRestrictionsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CoincidenceRestrictionsController> _logger;

        public CoincidenceRestrictionsController(IMediator mediator, ILogger<CoincidenceRestrictionsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
            _logger.LogInformation("CoincidenceRestrictions controller mapped");
        }

        [HttpGet]
        public async Task<List<CoincidenceRestriction>> GetAllCoincidenceRestrictions()
        {
            _logger.LogInformation("GetAllCoincidenceRestrictions endpoint called.");
            return await _mediator.Send(new GenericGetQuery<CoincidenceRestriction>());
        }

        [HttpPost]
        public async Task<CoincidenceRestriction> Create(GenericCreateCommand<CoincidenceRestriction> input)
        {
            _logger.LogInformation("Create endpoint called.");
            return await _mediator.Send(input);
        }
        [HttpPut]
        public async Task<CoincidenceRestriction> Update(GenericUpdateCommand<CoincidenceRestriction> input)
        {
            _logger.LogInformation("Update endpoint called.");
            return await _mediator.Send(input);
        }
        
        [HttpDelete]
        public async Task<bool> Delete(GenericRemoveCommand<CoincidenceRestriction> input)
        {
            _logger.LogInformation("Remove endpoint called.");
            return await _mediator.Send(input);
        }
    }
}