using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyWebApp.Models.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string Photo { get; set; }
        public virtual Category Category { get; set; }
    }
}