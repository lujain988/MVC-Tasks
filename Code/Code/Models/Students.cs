using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Code.Models
{
    public class Students
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Student Name Required.")]
        [Display(Name = "Name")]
        public string StudentName { get; set; }

        [Required]
        public int Age { get; set; }

        // Navigation Property for studentDetails
        public virtual studentDetails StudentDetails { get; set; }
    }

}