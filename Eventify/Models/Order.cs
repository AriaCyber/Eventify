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
    }
}
