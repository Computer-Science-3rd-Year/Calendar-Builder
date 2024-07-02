using CalendarBuilder.Application.Common.GenericCrud;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CalendarBuilder.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuantityRestrictionsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<QuantityRestrictionsController> _logger;

        public QuantityRestrictionsController(IMediator mediator, ILogger<QuantityRestrictionsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
            _logger.LogInformation("QuantityRestrictions controller mapped");
        }

        [HttpGet]
        public async Task<List<QuantityRestriction>> GetAllQuantityRestrictions()
        {
            _logger.LogInformation("GetAllQuantityRestrictions endpoint called.");
            return await _mediator.Send(new GenericGetQuery<QuantityRestriction>());
        }

        [HttpPost]
        public async Task<QuantityRestriction> Create(GenericCreateCommand<QuantityRestriction> input)
        {
            _logger.LogInformation("Create endpoint called.");
            return await _mediator.Send(input);
        }
        [HttpPut]
        public async Task<QuantityRestriction> Update(GenericUpdateCommand<QuantityRestriction> input)
        {
            _logger.LogInformation("Update endpoint called.");
            return await _mediator.Send(input);
        }
        
        [HttpDelete]
        public async Task<bool> Delete(GenericRemoveCommand<QuantityRestriction> input)
        {
            _logger.LogInformation("Remove endpoint called.");
            return await _mediator.Send(input);
        }
    }
}