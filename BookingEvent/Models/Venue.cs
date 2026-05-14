using System.ComponentModel.DataAnnotations;

namespace BookingEvent.Models
{
    public class Venue
    {
        [Key]
        public int VenueId { get; set; }
        public required string VenueName { get; set; }
        public required string VenueLocation { get; set; }
        public int VenueCapacity { get; set; }
        public string? ImageUrl { get; set; }
    }
}
