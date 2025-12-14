using System;

namespace Eventify.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int BookingId { get; set; }
        public decimal Amount { get; set; }
        public bool IsPaid { get; set; } = true;
        public bool IsRefunded { get; set; } = false;

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public decimal TotalAmount { get; set; }

        public string Status { get; set; }
    }

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
