namespace Eventify.DTOs.Admin
{
    public class RefundAdminDto
    {
        public int RefundId { get; set; }

        public int OrderId { get; set; }

        public decimal Amount { get; set; }

        public string Status { get; set; }

        public DateTime RequestedAt { get; set; }

        public DateTime? ProcessedAt { get; set; }
    }
}
