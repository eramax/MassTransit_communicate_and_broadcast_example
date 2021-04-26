using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Shared;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Service1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MultiplyController : ControllerBase
    {
        private readonly IRequestClient<MultiplyStatment> client;

        public MultiplyController(IRequestClient<MultiplyStatment> client)
        {
            this.client = client;
        }

        [HttpPost]
        public async Task<ActionResult<MultiplyResult>> Post([FromBody][Required] MultiplyStatment x)
        {
            // request from the remote service
            var request = client.Create(x);
            var response = await request.GetResponse<MultiplyResult>();
            return Ok(response);
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok("Hello");
        }
    }
}
