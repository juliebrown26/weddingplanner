using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

namespace weddingplanner.Models
{
    public class DashboardViewModel
    {
        public List<Wedding> Weddings {get;set;}
        public User User {get;set;}

        public Rsvp Rsvp {get;set;}
    }
}