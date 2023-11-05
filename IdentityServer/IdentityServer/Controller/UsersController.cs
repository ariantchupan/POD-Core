using IdentityServer.Application.Features.Users.Commands.AddUser;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace IdentityServer.Controller
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        [HttpPost(Name = "LoginSignupMobile")]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> LoginSignupMobile([FromBody] LoginSignupMobileCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
