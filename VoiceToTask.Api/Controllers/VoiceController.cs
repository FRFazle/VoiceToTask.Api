using System;
using Microsoft.AspNetCore.Mvc;
using VoiceToTask.Api.Data;
using VoiceToTask.Api.Models;
using VoiceToTask.Api.Services;

namespace VoiceToTask.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VoiceController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IVoiceParsingService _voiceService;

        public VoiceController(AppDbContext context, IVoiceParsingService voiceService)
        {
            _context = context;
            _voiceService = voiceService;
        }

        [HttpPost("parse")]
        public IActionResult Parse([FromBody] VoiceRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Text))
                return BadRequest("Text required");

            VoiceResponse result = _voiceService.Parse(request.Text);
            return Ok(result);
        }

        [HttpPost("create-task")]
        public async Task<IActionResult> CreateTask([FromBody] VoiceRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Text))
                return BadRequest("Text required");

            VoiceResponse parsed = _voiceService.Parse(request.Text);

            WorkTask task = new WorkTask
            {
                Title = parsed.Title,
                Description = parsed.Description,
                Location = parsed.Location,
                ScheduledTime = parsed.ScheduledTime ?? DateTime.UtcNow,
                Priority = parsed.Priority,
                Status = "Pending",
                CreatedAt = DateTime.UtcNow
            };

            _context.WorkTasks.Add(task);
            await _context.SaveChangesAsync();

            return Ok(task);
        }
    }
}