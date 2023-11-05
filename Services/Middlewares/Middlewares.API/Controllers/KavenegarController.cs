using MediatR;
using Microsoft.AspNetCore.Mvc;
using Middlewares.Application.Features.SMS.Commands.SendVerify;

namespace Middlewares.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class KavenegarController : ControllerBase
    {
        private readonly IMediator _mediator;

        public KavenegarController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        [HttpPost(Name = "SMSVerify")]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> SMSVerify([FromBody] SendVerifyCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
