using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JBOT.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController<T> : ControllerBase
        where T : class
    {
        private readonly ILogger<T> _logger;
        public BaseController(ILogger<T> logger)
        {
            _logger = logger;
        }
    }
}
