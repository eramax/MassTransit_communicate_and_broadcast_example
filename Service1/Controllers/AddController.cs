using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Shared;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Service1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddController : ControllerBase
    {
        private readonly IRequestClient<AddStatment> client;

        public AddController(IRequestClient<AddStatment> client)
        {
            this.client = client;
        }

        [HttpPost]
        public async Task<ActionResult<AdderResult>> Post([FromBody][Required] AddStatment x)
        {
            // request from the remote service
            var request = client.Create(x);
            var response = await request.GetResponse<AdderResult>();
            return Ok(response);
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok("Hello");
        }
    }
}
