namespace Examples.MediatR.WebAPI.Controllers
{
    using Core.Notifications;
    using global::MediatR;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents a RESTful service to send notifications with mediatR
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Constructor
        /// </summary>
        public NotificationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Publishs a notification
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Post()
        {
            Debug.WriteLine("Controller - Start publish SomeNotification");
            await _mediator.Publish(new SomeNotification("Some Notification"));
            Debug.WriteLine("Controller - End publish SomeNotification");
            return Ok();
        }
    }
}