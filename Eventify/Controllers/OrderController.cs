using Eventify.Data;
using Eventify.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eventify.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrderController(AppDbContext context)
        {
            _context = context;
        }

        //GET api/order
        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _context.Orders.ToListAsync();
            return Ok(orders);
        }

        //GET api/order/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
            if (order == null)
                return NotFound(new { message = "Order does not exist" });
            return Ok(order);
        }

        //POST api/order
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] Order order)
        {
            //check
            if (order.Amount < 0)
                return BadRequest(new { message = "Amount must be 0 or more" });
            //Requires booking as Order is linked to Booking
            var booking = await _context.Bookings.FirstOrDefaultAsync(b => b.Id == order.BookingId);
            if (booking == null)
                return NotFound(new { message = "Booking does not exist" });
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return Ok(order);
        }

        //POST api/order/refund/{id}
        [HttpPost("refund/{id}")]
        public async Task<IActionResult> RefundOrder(int id)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
            if (order == null)
                return NotFound(new { message = "Order does not exist" });
            if (order.IsRefunded)
                return BadRequest(new { message = "Order already refunded" });
            // restore capacity booking + event
            var booking = await _context.Bookings.FirstOrDefaultAsync(b => b.Id == order.BookingId);
            if (booking != null)
            {
                var evt = await _context.Events.FirstOrDefaultAsync(e => e.Id == booking.EventId);
                if (evt != null)
                {
                    evt.RemainingCapacity += booking.TicketCount;
                    if (evt.RemainingCapacity > evt.Capacity)
                        evt.RemainingCapacity = evt.Capacity;
                }
            }
            order.IsRefunded = true;
            await _context.SaveChangesAsync();
            return Ok(new { message = "Order refunded" });
        }
    }
}
