namespace Eventify.DTOs.Admin
{
    public class PromoCodeAdminDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public decimal DiscountValue { get; set; }
        public bool IsPercentage { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int UsageLimit { get; set; }
        public int Uses { get; set; }
    }
}