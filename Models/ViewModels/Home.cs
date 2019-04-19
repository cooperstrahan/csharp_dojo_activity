using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeltExam.Models
{
    public class Home
    {
        public User CurrentUser { get; set; }
        public List<Event> AllEvents { get; set; }
        public Home() {}
        public Home(User user, List<Event> events)
        {
            CurrentUser = user;
            AllEvents = events;
        }
    }
}