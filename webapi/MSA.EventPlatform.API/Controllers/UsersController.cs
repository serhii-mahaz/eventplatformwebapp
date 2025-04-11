using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MSA.EventPlatform.API.Extensions;
using MSA.EventPlatform.API.Models.Queries.Users;
using MSA.EventPlatform.API.Models.Responses.Events;
using MSA.EventPlatform.API.Models.Responses.Users;

namespace MSA.EventPlatform.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController(IMediator mediator) : ControllerBase
    {
        [HttpGet("{userId:long}")]
        [ProducesResponseType(typeof(PublicUserInfoDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PublicUserInfoDto>> GetPublicUserInfo(long userId)
        {
            return Ok(await mediator.Send(new GetPublicUserInfoQuery(userId)));
        }

        // GET	/api/users/me
        [Authorize]
        [HttpGet("me")]
        [ProducesResponseType(typeof(UserProfielDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<UserProfielDto>> GetUserProfile()
        {
            var userId = User.GetUserId();
            return Ok(await mediator.Send(new GetUserProfileQuery(userId)));
        }

        // PUT	/api/users/me
        [Authorize]
        [HttpPut("me")]
        public async Task<IActionResult> UpdateUserProfile()
        {
            await mediator.Send(new UpdateUserProfileCommand { UserId = User.GetUserId() });
            return NoContent();
        }

        // GET	/api/users/me/registrations
        [Authorize]
        [HttpGet("me/registrations")]
        [ProducesResponseType(typeof(IEnumerable<EventRegistrationDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<EventRegistrationDto>>> GetMyEventRegistrations()
        {
            var userId = User.GetUserId();
            var registrations = await mediator.Send(new GetMyEventRegistrationsQuery(userId));
            return Ok(registrations);
        }
    }
}
