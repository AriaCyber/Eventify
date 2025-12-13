namespace Eventify.Models
{
    public class Payment
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public decimal Amount { get; set; }

        public DateTime PaymentDate { get; set; }

        public string PaymentMethod { get; set; }  // e.g. Card, PayPal

        public bool IsSuccessful { get; set; }

        // Navigation
        public Order Order { get; set; }
    }
}
