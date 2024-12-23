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
    public class ActionLogsController : ControllerBase
    {
        private readonly IActionLogService _actionLogService;

        public ActionLogsController(IActionLogService actionLogService)
        {
            _actionLogService = actionLogService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<ActionLog>>> GetActionLogs()
        {
            var actionLogs = await _actionLogService.GetAllAsync();
            return Ok(actionLogs);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ActionLog>> GetActionLog(Guid id)
        {
            var actionLog = await _actionLogService.GetByIdAsync(id);
            if (actionLog == null)
            {
                return NotFound();
            }
            return Ok(actionLog);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> PostActionLog(ActionLog actionLog)
        {
            await _actionLogService.AddAsync(actionLog);
            return CreatedAtAction(nameof(GetActionLog), new { id = actionLog.ActionLogId }, actionLog);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutActionLog(Guid id, ActionLog actionLog)
        {
            if (id != actionLog.ActionLogId)
            {
                return BadRequest();
            }

            await _actionLogService.UpdateAsync(actionLog);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteActionLog(Guid id)
        {
            await _actionLogService.DeleteAsync(id);
            return NoContent();
        }
    }
}

