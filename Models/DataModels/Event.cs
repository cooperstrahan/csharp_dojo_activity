using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeltExam.Models
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }
        public string Activity { get; set; }
        public DateTime Date { get; set; }
        public int Duration { get; set; }
        public string DurationType { get; set; }
        public int CreatorId { get; set; }
        public string CreatorFirstName { get; set; }
        public string CreatorLastName { get; set; }
        public List<Participant> Participants { get; set; } = new List<Participant>();
        public string Description { get; set; }
        public Event() {}
        public Event(CreateActivity created)
        {
            Activity = created.NewActivity;
            Date = (DateTime)created.CreateDate;
            Duration = (int)created.CreateDuration;
            DurationType = created.CreateDurationType;
            Description = created.CreateDescription;
        }
    }
}