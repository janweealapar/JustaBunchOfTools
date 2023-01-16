using JBOT.Application.Commands;
using JBOT.Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JBOT.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UnitTestController : BaseController<UnitTestController>
    {
        private readonly IMediator _mediator;
        public UnitTestController(IMediator mediator, ILogger<UnitTestController> logger) : base(logger)
        {
            _mediator = mediator;
        }

        [HttpPost("Run")]
        
        public async Task<IActionResult> RunUnitTest([FromBody] TestableObjectDetailsDto unitTest)
        {
            var command = new ExecuteUnitTestCommand() { UnitTest = unitTest };
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
