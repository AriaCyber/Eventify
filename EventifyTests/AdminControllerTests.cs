using Eventify.Controllers;
using Eventify.Models;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace EventifyTests
{
    public class AdminControllerTests
    {
        [Fact]
        public async Task GetAllEvents_ReturnsEvents()
        {
            var context = TestDbHelper.GetDbContext();
            context.Events.Add(new Event { Title = "Admin Event", Capacity = 5 });
            await context.SaveChangesAsync();

            var controller = new AdminController(context);
            var result = await controller.GetAllEvents();

            var ok = Assert.IsType<OkObjectResult>(result);
            var events = Assert.IsAssignableFrom<IEnumerable<Event>>(ok.Value);
            Assert.Single(events);
        }

        [Fact]
        public async Task CreateEvent_AddsEvent()
        {
            var context = TestDbHelper.GetDbContext();
            var controller = new AdminController(context);

            var evt = new Event { Title = "Created By Admin", Capacity = 10 };

            var result = await controller.CreateEvent(evt);

            Assert.IsType<OkObjectResult>(result);
            Assert.Single(context.Events);
        }

        [Fact]
        public async Task UpdateEvent_ReturnsNotFound_WhenMissing()
        {
            var context = TestDbHelper.GetDbContext();
            var controller = new AdminController(context);

            var result = await controller.UpdateEvent(999, new Event());

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteEvent_RemovesEvent()
        {
            var context = TestDbHelper.GetDbContext();
            context.Events.Add(new Event { Title = "Delete", Capacity = 5 });
            await context.SaveChangesAsync();

            var controller = new AdminController(context);
            var evt = context.Events.First();

            var result = await controller.DeleteEvent(evt.Id);

            Assert.IsType<NoContentResult>(result);
            Assert.Empty(context.Events);
        }

        [Fact]
        public async Task GetRefunds_ReturnsRefundDtos()
        {
            var context = TestDbHelper.GetDbContext();
            context.Refunds.Add(new Refund
            {
                OrderId = 1,
                Amount = 50,
                Status = "Requested",
                RequestedAt = DateTime.UtcNow
            });
            await context.SaveChangesAsync();

            var controller = new AdminController(context);
            var result = await controller.GetRefunds();

            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.NotEmpty((IEnumerable<object>)ok.Value);
        }

        [Fact]
        public async Task UpdateRefundStatus_UpdatesStatusAndProcessedAt()
        {
            var context = TestDbHelper.GetDbContext();
            var refund = new Refund
            {
                OrderId = 1,
                Amount = 25,
                Status = "Requested",
                RequestedAt = DateTime.UtcNow
            };
            context.Refunds.Add(refund);
            await context.SaveChangesAsync();

            var controller = new AdminController(context);

            var result = await controller.UpdateRefundStatus(refund.Id, "Approved");

            Assert.IsType<OkObjectResult>(result);
            var updated = context.Refunds.First();
            Assert.Equal("Approved", updated.Status);
            Assert.NotNull(updated.ProcessedAt);
        }
    }
}