using Eventify.Controllers;
using Eventify.Models;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace EventifyTests
{
    public class BookingControllerTests
    {
        [Fact]
        public async Task CreateBooking_ReturnsBadRequest_WhenTicketCountInvalid()
        {
            var context = TestDbHelper.GetDbContext();
            var controller = new BookingController(context);

            var booking = new Booking { EventId = 1, UserId = 1, TicketCount = 0 };

            var result = await controller.CreateBooking(booking);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task CreateBooking_ReducesEventRemainingCapacity()
        {
            var context = TestDbHelper.GetDbContext();

            var evt = new Event { Title = "E1", Capacity = 10, RemainingCapacity = 10 };
            var user = new User { FullName = "U1" };
            context.Events.Add(evt);
            context.Users.Add(user);
            await context.SaveChangesAsync();

            var controller = new BookingController(context);

            var booking = new Booking
            {
                EventId = evt.Id,
                UserId = user.Id,
                TicketCount = 3
            };

            var result = await controller.CreateBooking(booking);

            Assert.IsType<OkObjectResult>(result);

            var updatedEvent = context.Events.First(e => e.Id == evt.Id);
            Assert.Equal(7, updatedEvent.RemainingCapacity);
        }

        [Fact]
        public async Task DeleteBooking_RestoresEventRemainingCapacity()
        {
            var context = TestDbHelper.GetDbContext();

            var evt = new Event { Title = "E1", Capacity = 10, RemainingCapacity = 7 };
            var user = new User { FullName = "U1" };
            context.Events.Add(evt);
            context.Users.Add(user);
            await context.SaveChangesAsync();

            var booking = new Booking
            {
                EventId = evt.Id,
                UserId = user.Id,
                TicketCount = 3
            };

            context.Bookings.Add(booking);
            await context.SaveChangesAsync();

            var controller = new BookingController(context);

            var result = await controller.DeleteBooking(booking.Id);

            Assert.IsType<OkObjectResult>(result);

            var updatedEvent = context.Events.First(e => e.Id == evt.Id);
            Assert.Equal(10, updatedEvent.RemainingCapacity);
        }
    }
}
