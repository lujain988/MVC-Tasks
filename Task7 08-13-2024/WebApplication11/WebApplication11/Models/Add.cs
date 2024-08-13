using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication11.Models
{
    public class Add
    {
        public class AddProduct
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public long Price { get; set; }
            public string ImageUrl { get; set; }
        }

    }
}