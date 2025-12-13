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

        //GET api/events
        [HttpGet]
        public async Task<IActionResult> GetEvents()
        {
            var eventsList = await _context.Events.ToListAsync();
            return Ok(eventsList);
        }

        //GET api/events/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEvent(int id)
        {
            var evt = await _context.Events.FirstOrDefaultAsync(e => e.Id == id);
            if (evt == null)
                return NotFound(new { message = "Event does not exist" });
            return Ok(evt);
        }

        //POST api/events
        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromBody] Event evt)
        {
            evt.RemainingCapacity = evt.Capacity;
            _context.Events.Add(evt);
            await _context.SaveChangesAsync();
            return Ok(evt);
        }

        //PUT api/events/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(int id, [FromBody] Event updated)
        {
            var evt = await _context.Events.FirstOrDefaultAsync(e => e.Id == id);
            if (evt == null)
                return NotFound(new { message = "Event does not exist" });
            evt.Title = updated.Title;
            evt.Description = updated.Description;
            evt.StartDateTime = updated.StartDateTime;
            evt.EndDateTime = updated.EndDateTime;
            evt.IsPublic = updated.IsPublic;
            evt.Capacity = updated.Capacity;
            evt.PricePerTicket = updated.PricePerTicket;
            evt.RemainingCapacity = evt.Capacity;
            await _context.SaveChangesAsync();
            return Ok(new { message = "Event updated" });
        }

        //DELETE api/events/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var evt = await _context.Events.FirstOrDefaultAsync(e => e.Id == id);
            if (evt == null)
                return NotFound(new { message = "Event does not exist" });
            _context.Events.Remove(evt);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Event deleted" });
        }
    }
}
