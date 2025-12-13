namespace Eventify.DTOs.Admin
{
    public class SalesReportDto
    {
        public DateTime Date { get; set; }

        public int TicketsSold { get; set; }

        public decimal TotalRevenue { get; set; }
    }
}
