using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CodeFirst.Models
{
    public class Teacher
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "Product Name Required.")]
        [DisplayName("Name")]
        public string TeacherName { get; set; }
        [Required]
        public int age { get; set; }

    }

  
}