using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace CodeFirst.Models
{
    public class Asign
    {


        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "Product Name Required.")]
        [DisplayName("Name")]
        public string AsignName { get; set; }
        
        public DateTime? date  { get; set; }



    }
}