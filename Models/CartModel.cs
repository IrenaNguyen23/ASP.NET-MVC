using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebShop.Context;

namespace WebShop.Models
{
    public class CartModel
    {
        public product Product { get; set; }

        public int Quantity { get; set; }
    }
}