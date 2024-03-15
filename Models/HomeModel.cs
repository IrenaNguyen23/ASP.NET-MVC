using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebShop.Context;

namespace WebShop.Models
{
    public class HomeModel
    {
        public List<category> ListCategory { get; set; }
        public List<product> ListProduct { get; set; }
    }
}