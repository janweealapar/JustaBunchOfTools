using JBOT.Application.Queries.Databases;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JBOT.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DatabaseController : BaseController<DatabaseController>
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;
        public DatabaseController(IMediator mediator, IConfiguration configuration, ILogger<DatabaseController> logger) : base(logger)
        {
            _mediator = mediator;
            _configuration = configuration;
        }

        [HttpGet("Servers")]
        public async Task<IActionResult> Get()
        {
            var servers = _configuration.GetSection("Servers:List").Get<string[]>()
                                .Select(s => s.Replace("\\", @"\")).ToList();
            return Ok(servers);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string? server)
        {
            var result = await _mediator.Send(new GetAllDatabasesQuery(server));
            return Ok(result);
        }

        [HttpGet("{id:int}/TestableObjects")]
        public async Task<IActionResult> GetTestableObjects([FromQuery] string? server, int id)
        {
            var result = await _mediator.Send(new GetTestableObjectsQuery(server, id));
            return Ok(result);
        }

        [HttpGet("{id:int}/TestableObjects/details")]
        public async Task<IActionResult> GetTestableObjects([FromQuery] string? server, int id, [FromQuery] int? objId)
        {
            var result = await _mediator.Send(new GetTestableObjectDetailsByIdQuery(server, id, objId));
            return Ok(result);
        }
    }
}
