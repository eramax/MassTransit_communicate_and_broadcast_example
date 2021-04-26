using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Shared;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Service1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        readonly IPublishEndpoint _publishEndpoint;

        public HomeController(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] [Required] User us)
        {
            await _publishEndpoint.Publish(us);
            return Ok();
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok("Hello");
        }
    }
}
