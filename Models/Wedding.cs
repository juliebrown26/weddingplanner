using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

namespace weddingplanner.Models
{
    public class Wedding
    {
        [Key]
        public int WeddingId { get; set; }

        [Required]
        [MinLength(2)]
        [Display(Name = "Partner One:")]
        public string PartnerOne { get; set; }

        [Required]
        [MinLength(2)]
        [Display(Name = "Partner Two:")]
        public string PartnerTwo { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Wedding Date:")]
        public DateTime WeddingDate { get; set; }

        [Required]
        [Display(Name = "Venue Address:")]
        public string Address { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public List<Rsvp> Rsvps { get; set; }
    }
}