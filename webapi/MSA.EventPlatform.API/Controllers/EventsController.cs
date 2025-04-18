using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MSA.EventPlatform.API.Extensions;
using MSA.EventPlatform.API.Models.Queries.Events;
using MSA.EventPlatform.API.Models.Queries.Events.EventRegistrations;
using MSA.EventPlatform.API.Models.Responses.Events;

namespace MSA.EventPlatform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController(IMediator mediator) : ControllerBase
    {
        // GET	/api/events
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventDto>>> GetAllEvents()
        {
            var events = await mediator.Send(new GetAllEventsQuery());
            return Ok(events);
        }

        // GET	/api/events/{eventId}
        [HttpGet("{eventId:long}")]
        public async Task<ActionResult<EventDto>> GetEventById(long eventId)
        {
            return await mediator.Send(new GetEventByIdQuery(eventId));
        }

        // POST	/api/events
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<long>> CreateEvent(CreateEventCommand command)
        {
            command.OrganizerId = User.GetUserId();
            var eventId = await mediator.Send(command);

            return CreatedAtAction(nameof(GetEventById), new { eventId }, eventId);
        }

        // PUT	/api/events/{eventId}
        [Authorize]
        [HttpPut("{eventId:long}")]
        public async Task<IActionResult> UpdateEvent([FromRoute] long eventId, [FromBody] UpdateEventCommand command)
        {
            if (eventId != command.Id) return BadRequest("Event ID in the URL does not match the ID in the request body.");

            command.OrganizerId = User.GetUserId();
            await mediator.Send(command);

            return NoContent();
        }

        // DELETE	/api/events/{eventId}
        [Authorize]
        [HttpDelete("{eventId:long}")]
        public async Task<IActionResult> DeleteEvent(long eventId)
        {
            await mediator.Send(new DeleteEventCommand(eventId));
            return NoContent();
        }

        // POST	/api/events/{eventId}/register
        [Authorize]
        [HttpPost("{eventId:long}/register")]
        public async Task<IActionResult> RegisterToEvent([FromRoute] long eventId)
        {
            var userId = User.GetUserId();
            await mediator.Send(new RegisterToEventCommand { EventId = eventId, ParticipantId = userId });
            return Ok();
        }

        // DELETE	/api/events/{eventId}/register
        [Authorize]
        [HttpDelete("{eventId:long}/register")]
        public async Task<IActionResult> CancelRegistration([FromRoute] long eventId)
        {
            var userId = User.GetUserId();
            await mediator.Send(new CancelEventRegistrationCommand { EventId = eventId, ParticipantId = userId });
            return NoContent();
        }

        // GET	/api/events/{eventId}/participants
        [Authorize]
        [HttpGet("{eventId:long}/participants")]
        public async Task<ActionResult<IEnumerable<EventRegistrationDto>>> GetEventParticipants([FromRoute] long eventId)
        {
            var participants = await mediator.Send(new GetEventParticipantsQuery(eventId));
            return Ok(participants);
        }
    }
}
