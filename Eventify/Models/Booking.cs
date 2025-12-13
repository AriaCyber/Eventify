using System;

namespace Eventify.Models;

public class Booking
{
    public int Id { get; set; }

    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public int EventId { get; set; }
    public Event Event { get; set; } = null!;

    public int TicketCount { get; set; }

    public DateTime BookingDate { get; set; } = DateTime.UtcNow;
}
