using Eventify.Controllers;
using Eventify.Models;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace EventifyTests
{
    public class OrderControllerTests
    {
        [Fact]
        public async Task RefundOrder_SetsIsRefunded_AndRestoresCapacity()
        {
            var context = TestDbHelper.GetDbContext();
            var evt = new Event { Title = "E1", Capacity = 10, RemainingCapacity = 7 };
            var user = new User { FullName = "U1" };
            context.Events.Add(evt);
            context.Users.Add(user);
            await context.SaveChangesAsync();

            var booking = new Booking { EventId = evt.Id, UserId = user.Id, TicketCount = 3 };
            context.Bookings.Add(booking);
            await context.SaveChangesAsync();

            var order = new Order
            {
                BookingId = booking.Id,
                Amount = 75,
                TotalAmount = 75,
                IsPaid = true,
                IsRefunded = false,
                Status = "Paid"
            };

            context.Orders.Add(order);
            await context.SaveChangesAsync();
            var controller = new OrderController(context);
            var result = await controller.RefundOrder(order.Id);
            Assert.IsType<OkObjectResult>(result);
            var updatedOrder = context.Orders.First(o => o.Id == order.Id);
            Assert.True(updatedOrder.IsRefunded);
            var updatedEvent = context.Events.First(e => e.Id == evt.Id);
            Assert.Equal(10, updatedEvent.RemainingCapacity);
        }

        [Fact]
        public async Task RefundOrder_ReturnsBadRequest_WhenAlreadyRefunded()
        {
            var context = TestDbHelper.GetDbContext();

            var evt = new Event { Title = "E1", Capacity = 10, RemainingCapacity = 10 };
            context.Events.Add(evt);
            await context.SaveChangesAsync();

            var booking = new Booking { EventId = evt.Id, UserId = 1, TicketCount = 1 };
            context.Bookings.Add(booking);
            await context.SaveChangesAsync();

            var order = new Order 
            {   
                BookingId = booking.Id,
                Amount = 25,
                TotalAmount = 25,
                IsPaid = true,
                IsRefunded = true,
                Status = "Refunded"
            };
            context.Orders.Add(order);
            await context.SaveChangesAsync();

            var controller = new OrderController(context);
            var result = await controller.RefundOrder(order.Id);
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
