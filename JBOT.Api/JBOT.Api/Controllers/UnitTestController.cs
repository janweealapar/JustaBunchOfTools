using JBOT.Application.Commands;
using JBOT.Application.Dtos;
using JBOT.Application.Queries.UnitTests;
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

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int? Id, [FromQuery] string? server, string? database)
        {
            try
            {
                if (Id == null)
                {
                    var result = await _mediator.Send(new GetAllUnitTestsQuery(server, database));
                    return Ok(result);
                }
                else
                {
                    var result = await _mediator.Send(new GetUnitTestByIdQuery() { Id = Id.Value });
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TestableObjectDetailsDto dto)
        {
            var command = new CreateUnitTestCommand() { TestableObjectDetailsDto = dto };
            var result = await _mediator.Send(command);
            return Ok(dto);
        }

        [HttpDelete]
        public async Task<IActionResult> Remove([FromQuery] int id)
        {
            var cmd = new RemoveUnitTestCommand() { UnitTestId = id };
            return Ok(await _mediator.Send(cmd));
        }
        
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] TestableObjectDetailsDto dto)
        {
            var cmd = new UpdateUnitTestCommand() { TestableObjectDetailsDto = dto };
            var result = await _mediator.Send(cmd);

            switch (result)
            {
                case 200:
                    return Ok(result);
                case 404:
                    return NotFound(result);
                case 500:
                    return StatusCode(StatusCodes.Status500InternalServerError, result.ToString());
                default:
                    return BadRequest(result);
            }
        }

        [HttpPost("Run")]
        
        public async Task<IActionResult> RunUnitTest([FromBody] TestableObjectDetailsDto unitTest, [FromQuery] string? server)
        {
            var command = new ExecuteUnitTestCommand(server) { UnitTest = unitTest };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("Run/All")]
        public async Task<IActionResult> RunAllUnitTest([FromQuery] string? server, string? database)
        {
            var command = new RunTestsPerServerAndDatabaseCommand(server, database);
            var result = await _mediator.Send(command);

            return Ok(result);
        }
    }
}
