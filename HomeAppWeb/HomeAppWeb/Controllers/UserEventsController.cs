using HomeAppWeb.Interfaces.Services;
using HomeAppWeb.Models;
using HomeAppWeb.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeAppWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserEventsController : ControllerBase
    {
        private readonly IUserEventService _userEventService;

        public UserEventsController(IUserEventService userEventService)
        {
            _userEventService = userEventService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<IEnumerable<UserEvent>>> GetUserEvents()
        {
            var userEvents = await _userEventService.GetAllAsync();
            return Ok(userEvents);
        }

        [HttpGet("{userId}/{eventId}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<UserEvent>> GetUserEvent(Guid userId, string eventId)
        {
            var userEvent = await _userEventService.GetByIdAsync(userId, eventId);
            if (userEvent == null)
            {
                return NotFound();
            }
            return Ok(userEvent);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> PostUserEvent(UserEvent userEvent)
        {
            await _userEventService.AddAsync(userEvent);
            return CreatedAtAction(nameof(GetUserEvent), new { userId = userEvent.UserId, eventId = userEvent.EventId }, userEvent);
        }

        [HttpPut("{userId}/{eventId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutUserEvent(Guid userId, string eventId, UserEvent userEvent)
        {
            if (userId != userEvent.UserId || eventId != userEvent.EventId)
            {
                return BadRequest();
            }

            await _userEventService.UpdateAsync(userEvent);
            return NoContent();
        }

        [HttpDelete("{userId}/{eventId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUserEvent(Guid userId, string eventId)
        {
            await _userEventService.DeleteAsync(userId, eventId);
            return NoContent();
        }
    }
}
