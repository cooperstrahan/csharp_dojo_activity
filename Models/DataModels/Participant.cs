using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeltExam.Models
{
    public class Participant
    {
        [Key]
        public int ParticipantId { get; set; }
        public int UserId { get; set; }
        public int EventId { get; set; }
        public User User { get; set; }
        public Event Event { get; set; }
        public Participant() {}
        
    }
}