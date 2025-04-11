using MediatR;
using Microsoft.AspNetCore.Mvc;
using MSA.EventPlatform.API.Models.Commands.Auth;
using MSA.EventPlatform.API.Models.Responses.Auth;

namespace MSA.EventPlatform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IMediator mediator) : ControllerBase
    {
        [HttpPost("register")]
        [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AuthResponseDto>> Register([FromBody] RegisterUserCommand command)
        {
            return Ok(await mediator.Send(command));
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto>> Login([FromBody] LoginUserCommand command)
        {
            return Ok(await mediator.Send(command));
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<AuthResponseDto>> RefreshToken([FromBody] RefreshTokenCommand command)
        {
            return Ok(await mediator.Send(command));
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] LogoutUserCommand command)
        {
            await mediator.Send(command);
            return Ok();
        }
    }
}
