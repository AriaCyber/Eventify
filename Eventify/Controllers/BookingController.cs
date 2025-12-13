using Eventify.Data;
using Eventify.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eventify.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly AppDbContext _context;
        public BookingController(AppDbContext context)
        {
            _context = context;
        }

        //GET api/booking
        [HttpGet]
        public async Task<IActionResult> GetBookings()
        {
            var bookings = await _context.Bookings.ToListAsync();
            return Ok(bookings);
        }

        //GET api/booking/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBooking(int id)
        {
            var booking = await _context.Bookings.FirstOrDefaultAsync(b => b.Id == id);
            if (booking == null)
                return NotFound(new { message = "Booking does not exist" });
            return Ok(booking);
        }

        //POST api/booking
        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] Booking booking)
        {
            if (booking.TicketCount <= 0)
                return BadRequest(new { message = "TicketCount must be greater than 0" });
            //make sure event exists
            var evt = await _context.Events.FirstOrDefaultAsync(e => e.Id == booking.EventId);
            if (evt == null)
                return NotFound(new { message = "Event does not exist" });
            //make sure user exists
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == booking.UserId);
            if (user == null)
                return NotFound(new { message = "User does not exist" });
            //capacity check
            if (evt.RemainingCapacity < booking.TicketCount)
                return BadRequest(new { message = "Not enough remaining capacity" });
            evt.RemainingCapacity -= booking.TicketCount; // update remaining capacity
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
            return Ok(booking);
        }

        //DELETE api/booking/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var booking = await _context.Bookings.FirstOrDefaultAsync(b => b.Id == id);
            if (booking == null)
                return NotFound(new { message = "Booking does not exist" });
            //restore event capacity
            var evt = await _context.Events.FirstOrDefaultAsync(e => e.Id == booking.EventId);
            if (evt != null)
            {
                evt.RemainingCapacity += booking.TicketCount;
                //ensure we don't exceed total capacity
                if (evt.RemainingCapacity > evt.Capacity)
                    evt.RemainingCapacity = evt.Capacity;
            }
            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Booking deleted" });
        }
    }
}