using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CodeFirst.Models
{
    public class Students4
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "Product Name Required.")]
        [DisplayName("Name")]
        public string StudentName { get; set; }
        [Required]
        public int age { get; set; }


    }

 
}