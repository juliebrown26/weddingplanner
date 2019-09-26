using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

namespace weddingplanner.Models
{
    public class DetailsViewModel
    {
        public List<User> Users { get; set; }
        public Wedding Wedding { get; set; }
        public Rsvp Rsvp { get; set; }
    }
}