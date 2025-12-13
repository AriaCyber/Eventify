namespace Eventify.DTOs.Admin
{
    public class EventOccupancyDto
    {
        public int EventId { get; set; }

        public string EventTitle { get; set; }

        public int Capacity { get; set; }

        public int TicketsSold { get; set; }

        public double OccupancyRate { get; set; }
    }
}
