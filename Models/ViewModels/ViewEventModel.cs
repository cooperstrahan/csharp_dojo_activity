using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeltExam.Models
{
    public class ViewEvent
    {
        public User CurrentUser { get; set; }
        public Event CurrentEvent { get; set; }
        public ViewEvent() {}
        public ViewEvent(User user, Event curevent)
        {
            CurrentUser = user;
            CurrentEvent = curevent;
        }
    }
}