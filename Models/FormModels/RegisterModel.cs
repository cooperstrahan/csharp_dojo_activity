using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeltExam.Models
{
    public class Register
    {
        [Required]
        [MinLength(2)]
        public string FirstName { get; set; }
        [Required]
        [MinLength(2)]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$", 
            ErrorMessage="Password must be between 8-15 characters at least one of the following: one uppercase letter, one lowercase letter, one number, and one special character" )]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [CompareAttribute("Password", ErrorMessage = "Passwords Must Match!")]
        public string PasswordConfirm { get; set; }
    }
}