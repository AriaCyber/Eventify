using System;

namespace Eventify.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public int UserId { get; set; }

        public int TicketCount { get; set; }

    }
}
