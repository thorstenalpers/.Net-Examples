namespace WebAPI.Controllers
{
    using Microsoft.AspNetCore.Routing;
    using Microsoft.AspNetCore.Mvc;
    using MediatR;
    using System.Threading.Tasks;
    using Core.Requests;
    using System.Diagnostics;

    /// <summary>
    /// Represents a RESTful service to send one way requests with mediatR
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class OneWayRequestController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Constructor
        /// </summary>
        public OneWayRequestController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Sends a OneWayRequest
        /// </summary>
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