using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.Context;

namespace WebShop.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        WebMVC obj = new WebMVC();
        public ActionResult Index()
        {
            var lstCategory = obj.categories.ToList();
            return View(lstCategory);
        }
        public ActionResult ProductCategory(int Id)
        {
            var listProduct = obj.products.Where(n=>n.categoryid == Id).ToList();
            return View(listProduct);
        }
    }
}