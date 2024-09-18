using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Code.Models
{
    public class studentDetails
    {
        [Key, ForeignKey("Students")] // Mark this as both the primary key and the foreign key
        public int ID { get; set; }

        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public int NumOfSiblings { get; set; }

        // Navigation Property for Students
        public virtual Students Students { get; set; }
    }

}