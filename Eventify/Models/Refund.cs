using System;

namespace Eventify.Models
{
    public class Refund
    {
        public int Id { get; set; }

        // FK to Order
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;

        public decimal Amount { get; set; }

        public string Reason { get; set; } = null!;

        // Requested / Approved / Declined / Processed
        public string Status { get; set; } = null!;

        public DateTime RequestedAt { get; set; }
        public DateTime? ProcessedAt { get; set; }
    }
}
