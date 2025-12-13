using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Eventify.Data;
using Eventify.Models;
using Eventify.DTOs.Admin;

namespace Eventify.Controllers
{
    [ApiController]
    [Route("api/admin")]
    public class AdminController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        // ---------------- EVENTS ----------------

        // GET: api/admin/events
        [HttpGet("events")]
        public async Task<IActionResult> GetAllEvents()
        {
            var events = await _context.Events.ToListAsync();
            return Ok(events);
        }

        // POST: api/admin/events
        [HttpPost("events")]
        public async Task<IActionResult> CreateEvent(Event newEvent)
        {
            _context.Events.Add(newEvent);
            await _context.SaveChangesAsync();
            return Ok(newEvent);
        }

        // PUT: api/admin/events/{id}
        [HttpPut("events/{id}")]
        public async Task<IActionResult> UpdateEvent(int id, Event updatedEvent)
        {
            var existingEvent = await _context.Events.FindAsync(id);
            if (existingEvent == null)
                return NotFound();

            existingEvent.Title = updatedEvent.Title;
            existingEvent.Description = updatedEvent.Description;
            existingEvent.StartDateTime = updatedEvent.StartDateTime;
            existingEvent.EndDateTime = updatedEvent.EndDateTime;
            existingEvent.Capacity = updatedEvent.Capacity;
            existingEvent.RemainingCapacity = updatedEvent.RemainingCapacity;
            existingEvent.PricePerTicket = updatedEvent.PricePerTicket;
            existingEvent.IsPublic = updatedEvent.IsPublic;

            await _context.SaveChangesAsync();
            return Ok(existingEvent);
        }

        // DELETE: api/admin/events/{id}
        [HttpDelete("events/{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var ev = await _context.Events.FindAsync(id);
            if (ev == null)
                return NotFound();

            _context.Events.Remove(ev);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // ---------------- REFUNDS ----------------

        // GET: api/admin/refunds
        [HttpGet("refunds")]
        public async Task<IActionResult> GetRefunds()
        {
            var refunds = await _context.Refunds
                .Select(r => new RefundAdminDto
                {
                    RefundId = r.Id,
                    OrderId = r.OrderId,
                    Amount = r.Amount,
                    Status = r.Status,
                    RequestedAt = r.RequestedAt,
                    ProcessedAt = r.ProcessedAt
                })
                .ToListAsync();

            return Ok(refunds);
        }

        // PUT: api/admin/refunds/{id}/status
        [HttpPut("refunds/{id}/status")]
        public async Task<IActionResult> UpdateRefundStatus(int id, [FromBody] string status)
        {
            var refund = await _context.Refunds.FindAsync(id);
            if (refund == null)
                return NotFound();

            refund.Status = status;
            refund.ProcessedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return Ok(refund);
        }
    }
}
