using System;
using System.Collections.Generic;

namespace Eventify.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public bool IsPublic { get; set; } = true;
        public int Capacity { get; set; }
        public int RemainingCapacity { get; set; }
        public decimal PricePerTicket { get; set; }
    }
}