using System.ComponentModel.DataAnnotations;

namespace BookingEvent.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }

        public DateTime BookingDate { get; set; }
        public int VenueId { get; set; }
        public Venue? Venue { get; set; }
        public int EventId { get; set; }
        public Event? Event { get; set; }
    }
}