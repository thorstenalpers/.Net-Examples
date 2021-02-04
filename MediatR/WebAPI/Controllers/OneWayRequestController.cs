using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Examples.MediatR.Core.Requests;
using System.Diagnostics;
using MediatR;

namespace Examples.MediatR.WebAPI.Controllers
{
	[ApiController]
    [Route("api/[controller]")]
    public class OneWayRequestController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OneWayRequestController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            Debug.WriteLine("Controller - Start send SomeOneWayRequest");
            await _mediator.Send(new SomeOneWayRequest("Some OneWay Request"));
            Debug.WriteLine("Controller - End send SomeOneWayRequest");
            return Ok();
        }
    }
}