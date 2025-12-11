using Eventify.Data;
using Eventify.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eventify.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public EventsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/events
        [HttpGet]
        public async Task<IActionResult> GetEvents()
        {
            var eventsList = await _context.Events.ToListAsync();
            return Ok(eventsList);
        }

        // POST: api/events
        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromBody] Event evt)
        {
            evt.RemainingCapacity = evt.Capacity;
            _context.Events.Add(evt);
            await _context.SaveChangesAsync();
            return Ok(evt);
        }
    }
}
