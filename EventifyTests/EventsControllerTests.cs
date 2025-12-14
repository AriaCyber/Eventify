using Eventify.Controllers;
using Eventify.Models;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace EventifyTests
{
    public class EventsControllerTests
    {
        [Fact]
        public async Task CreateEvent_SetsRemainingCapacity()
        {
            var context = TestDbHelper.GetDbContext();
            var controller = new EventsController(context);
            var evt = new Event
            {
                Title = "Test Event",
                Capacity = 10
            };

            var result = await controller.CreateEvent(evt);
            var ok = Assert.IsType<OkObjectResult>(result);
            var saved = Assert.IsType<Event>(ok.Value);
            Assert.Equal(10, saved.RemainingCapacity);
        }

        [Fact]
        public async Task GetEvent_ReturnsNotFound_WhenMissing()
        {
            var context = TestDbHelper.GetDbContext();
            var controller = new EventsController(context);
            var result = await controller.GetEvent(999);
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task DeleteEvent_DeletesExistingEvent()
        {
            var context = TestDbHelper.GetDbContext();
            context.Events.Add(new Event { Title = "Delete Me", Capacity = 5, RemainingCapacity = 5 });
            await context.SaveChangesAsync();
            var controller = new EventsController(context);
            var existing = context.Events.First();
            var result = await controller.DeleteEvent(existing.Id);
            Assert.IsType<OkObjectResult>(result);
            Assert.Empty(context.Events.ToList());
        }
    }
}
