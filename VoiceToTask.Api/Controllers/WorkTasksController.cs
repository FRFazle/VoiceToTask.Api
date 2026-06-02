using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VoiceToTask.Api.Data;
using VoiceToTask.Api.DTOs;
using VoiceToTask.Api.Models;

namespace VoiceToTask.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkTasksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public WorkTasksController(AppDbContext context)
        {
            _context = context;
        }

        // CREATE
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] WorkTaskDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            DateTime scheduled = dto.ScheduledTime != default ? dto.ScheduledTime : DateTime.UtcNow;

            WorkTask task = new WorkTask
            {
                Title = dto.Title,
                Description = dto.Description,
                Location = dto.Location,
                ScheduledTime = scheduled,
                Materials = dto.Materials,
                Status = dto.Status,
                Priority = dto.Priority,
                CreatedAt = DateTime.UtcNow
            };

            _context.WorkTasks.Add(task);
            await _context.SaveChangesAsync();

            return Ok(task);
        }

        // GET ALL
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<WorkTask> tasks = await _context.WorkTasks
                .AsNoTracking()
                .OrderByDescending(t => t.Id)
                .ToListAsync();

            return Ok(tasks);
        }

        // GET BY ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            WorkTask task = await _context.WorkTasks
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (task == null)
                return NotFound();

            return Ok(task);
        }

        // UPDATE
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] WorkTaskDto dto)
        {
            WorkTask? task = await _context.WorkTasks.FindAsync(id);

            if (task == null)
                return NotFound();

            task.Title = dto.Title ?? task.Title;
            task.Description = dto.Description ?? task.Description;
            task.Location = dto.Location ?? task.Location;
            task.ScheduledTime = dto.ScheduledTime != default ? dto.ScheduledTime : task.ScheduledTime;
            task.Materials = dto.Materials ?? task.Materials;
            task.Status = dto.Status ?? task.Status;
            task.Priority = dto.Priority ?? task.Priority;

            await _context.SaveChangesAsync();

            return Ok(task);
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            WorkTask? task = await _context.WorkTasks.FindAsync(id);

            if (task == null)
                return NotFound();

            _context.WorkTasks.Remove(task);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Task deleted successfully" });
        }

        // FILTER BY STATUS
        [HttpGet("status/{status}")]
        public async Task<IActionResult> GetByStatus(string status)
        {
            List<WorkTask> tasks = await _context.WorkTasks
                .AsNoTracking()
                .Where(t => t.Status == status)
                .ToListAsync();

            return Ok(tasks);
        }

        // SEARCH
        [HttpGet("search")]
        public async Task<IActionResult> Search(string keyword)
        {
            List<WorkTask> tasks = await _context.WorkTasks
                .AsNoTracking()
                .Where(t =>
                    t.Title.Contains(keyword) ||
                    t.Description.Contains(keyword) ||
                    t.Location.Contains(keyword))
                .ToListAsync();

            return Ok(tasks);
        }
    }
}