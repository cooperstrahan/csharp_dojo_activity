using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeltExam.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<Participant> Events { get; set; }
        public User() {}
        public User(Register user)
        {
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            Password = user.Password;
        }
    }
}