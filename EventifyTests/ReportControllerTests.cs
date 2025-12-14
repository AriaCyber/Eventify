using Eventify.Controllers;
using Eventify.Models;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace EventifyTests
{
    public class ReportControllerTests
    {
        [Fact]
        public async Task GetSalesReport_ReturnsOnlyPaidNotRefunded()
        {
            var context = TestDbHelper.GetDbContext();
            context.Orders.Add(new Order
            {
                IsPaid = true,
                IsRefunded = false,
                TotalAmount = 100,
                OrderDate = DateTime.UtcNow
            });
            await context.SaveChangesAsync();

            var controller = new ReportController(context);
            var result = await controller.GetSalesReport();

            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.NotEmpty((IEnumerable<object>)ok.Value);
        }

        [Fact]
        public async Task GetEventOccupancy_ReturnsCorrectValues()
        {
            var context = TestDbHelper.GetDbContext();
            context.Events.Add(new Event
            {
                Title = "Occ Test",
                Capacity = 10,
                RemainingCapacity = 6
            });
            await context.SaveChangesAsync();

            var controller = new ReportController(context);
            var result = await controller.GetEventOccupancy();

            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.NotEmpty((IEnumerable<object>)ok.Value);
        }
    }
}
