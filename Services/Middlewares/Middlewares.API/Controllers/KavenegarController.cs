using Microsoft.AspNetCore.Mvc;
using Middlewares.Application.Features.SMS.Commands.SendVerify;

namespace Middlewares.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class KavenegarController : ControllerBase
    {
        [HttpPost(Name = "SMSVerify")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> SMSVerify([FromBody] SendVerifyCommand command)
        {
            var result = new SendVerifyCommand(command.MobileNumber,command.Code);
            return Ok(result);
        }
    }
}
