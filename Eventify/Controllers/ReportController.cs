using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Eventify.Data;
using Eventify.DTOs.Admin;

namespace Eventify.Controllers
{
    [ApiController]
    [Route("api/reports")]
    public class ReportController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReportController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/reports/sales
        [HttpGet("sales")]
        public async Task<IActionResult> GetSalesReport()
        {
            var report = await _context.Orders
                .Where(o => o.IsPaid && !o.IsRefunded)
                .GroupBy(o => o.OrderDate.Date)
                .Select(g => new SalesReportDto
                {
                    Date = g.Key,
                    TicketsSold = g.Count(),
                    TotalRevenue = g.Sum(o => o.TotalAmount)
                })
                .OrderBy(r => r.Date)
                .ToListAsync();

            return Ok(report);
        }

        // GET: api/reports/occupancy
        [HttpGet("occupancy")]
        public async Task<IActionResult> GetEventOccupancy()
        {
            var report = await _context.Events
                .Select(e => new EventOccupancyDto
                {
                    EventId = e.Id,
                    EventTitle = e.Title,
                    Capacity = e.Capacity,
                    TicketsSold = e.Capacity - e.RemainingCapacity,
                    OccupancyRate = e.Capacity == 0
                        ? 0
                        : (double)(e.Capacity - e.RemainingCapacity) / e.Capacity * 100
                })
                .ToListAsync();

            return Ok(report);
        }
    }
}
