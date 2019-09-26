using System.ComponentModel.DataAnnotations;
using System;

namespace weddingplanner.Models
{
    public class Rsvp
    {
        [Key]
        public int RsvpId { get; set; }

        public int UserId { get; set; }

        public int WeddingId { get; set; }

        public User User { get; set; }

        public Wedding Wedding { get; set; }
    }
}