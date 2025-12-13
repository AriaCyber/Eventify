namespace Eventify.Models
{
    public class Order
    {
        public int Id { get; set; }

        // Who placed the order
        public int UserId { get; set; }
        public User User { get; set; }

        // Event being purchased
        public int EventId { get; set; }
        public Event Event { get; set; }

        // Order details
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public decimal TotalAmount { get; set; }

        // Status for admin + refunds
        // e.g. Pending, Paid, Cancelled, Refunded
        public string Status { get; set; }

        // Navigation
        public ICollection<Payment> Payments { get; set; }
        public ICollection<Refund> Refunds { get; set; }
    }
}
