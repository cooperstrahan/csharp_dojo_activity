using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeltExam.Models
{
    public class CreateActivity
    {
        [Required(ErrorMessage="The Activity Title is required")]
        [MinLength(3, ErrorMessage="The Activity Title is must be longer than 3 characters")]
        public string NewActivity { get; set; }
        [Required(ErrorMessage="A Valid Date and Time are required")]
        [DataType(DataType.DateTime)]
        public DateTime? CreateDate { get; set; }
        [Required(ErrorMessage="An activity duration is required")]
        [Range(1, int.MaxValue)]
        public int? CreateDuration { get; set; }
        [Required(ErrorMessage="An activity duration type is required")]
        public string CreateDurationType { get; set; }
        [Required(ErrorMessage="The Description is required")]
        [MinLength(5, ErrorMessage="The Description must be at least 5 characters")]
        public string CreateDescription { get; set; }
        public CreateActivity() {}
    }
}