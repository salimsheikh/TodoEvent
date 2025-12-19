using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TodoEvent.DTOs;
using TodoEvent.Models;
using TodoEvent.Services;

namespace TodoEvent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EventsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventDtoList>>> GetEvents()
        {
            var results = await _context.Event
                .OrderByDescending(e => e.Id)
                .Select(e => new EventDtoList
                {
                    Id = e.Id,
                    Title = e.Title,
                    Description = e.Description,
                    Start = e.Start,
                    End = e.End,
                    AllDay = e.AllDay
                }).ToListAsync();

            return Ok(results);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEvent(int id)
        {
            var row = await _context.Event.FindAsync(id);

            if (row is null)
                return NotFound($"Event with id {id} not found.");

            return Ok(row);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent(EventDtoCreate dto)
        {
            var evt = new Event
            {
                Title = dto.Title,
                Description = dto.Description,
                Start = dto.Start,
                End = dto.End,
                AllDay = dto.AllDay,
                CreateAt = DateTime.UtcNow
            };

            await _context.Event.AddAsync(evt);
            await _context.SaveChangesAsync();

            return Ok(evt);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> EditEvent(int id, EventDtoUpdate dto)
        {
            var evt = await _context.Event.FindAsync(id);

            if(evt is null){
                return NotFound();
            }

            evt.Title = dto.Title;
            evt.Description = dto.Description;
            evt.Start = dto.Start;
            evt.End = dto.End;
            evt.AllDay = dto.AllDay;

            await _context.SaveChangesAsync();

            return Ok(evt);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var evt = await _context.Event.FindAsync(id);
            if(evt is null)
            {
                return NotFound();
            }

            _context.Event.Remove(evt);
            await _context.SaveChangesAsync();

            return Ok();
        }

    }
}
