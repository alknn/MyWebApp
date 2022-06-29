using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyWebApp.Models.ViewModel
{
    public class CartViewModel
    {
        public string Photo { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}