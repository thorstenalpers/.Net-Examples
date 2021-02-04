using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using System.Threading.Tasks;
using System.Diagnostics;
using Examples.MediatR.Core.Requests;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Represents a RESTful service to send requests with mediatR
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class RequestController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Constructor
        /// </summary>
        public RequestController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Sends a request
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Post()
        {
            Debug.WriteLine("Controller - Start send SomeRequest");
            var result = await _mediator.Send(new SomeRequest("Some Request"));
            Debug.WriteLine("Controller - End send SomeRequest");

            if (result != true) throw new System.Exception();

            return Ok();
        }
    }
}