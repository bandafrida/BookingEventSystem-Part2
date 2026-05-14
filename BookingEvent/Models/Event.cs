using System.ComponentModel.DataAnnotations;

namespace BookingEvent.Models
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }

        public required string EventName { get; set; }

        public DateTime EventDate { get; set; }

        public DateTime EventTime { get; set; }

        public string? ImageUrl { get; set; }

        public ICollection<Booking>? Bookings { get; set; }
    }
}