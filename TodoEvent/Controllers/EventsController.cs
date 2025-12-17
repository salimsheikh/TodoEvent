using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> GetEvents()
        {
            var results = await _context.Event.OrderByDescending(e => e.Id).ToListAsync();
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

    }
}
