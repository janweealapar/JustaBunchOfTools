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
        public DatabaseController(IMediator mediator, ILogger<DatabaseController> logger) : base(logger)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetAllDatabasesQuery());
            return Ok(result);
        }

        [HttpGet("{id:int}/TestableObjects")]
        public async Task<IActionResult> GetTestableObjects(int id)
        {
            var result = await _mediator.Send(new GetTestableObjectsQuery(id));
            return Ok(result);
        }
        [HttpGet("{id:int}/TestableObjects/details")]
        public async Task<IActionResult> GetTestableObjects(int id, [FromQuery] int? objId)
        {
            var result = await _mediator.Send(new GetTestableObjectDetailsByIdQuery(id, objId));
            return Ok(result);
        }
    }
}
