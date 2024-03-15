using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.Context;

namespace WebShop.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        WebMVC obj = new WebMVC();
        public ActionResult Index()
        {
            var listProduct = obj.products.ToList();
            return View(listProduct);
        }
        public ActionResult Detail(int id)
        {
            var product = obj.products.Where(n => n.id == id).FirstOrDefault();
            return View(product);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(product product)
        {
            try
            {
                obj.products.Add(product);
                obj.SaveChanges();
                RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            product existingProduct = obj.products.Find(id);
            if (existingProduct == null)
            {
                return HttpNotFound();
            }

            return View(existingProduct);
        }

        [HttpPost]
        public ActionResult Edit(product product)
        {
            try
            {
                obj.Entry(product).State = EntityState.Modified;
                obj.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            product existingProduct = obj.products.Find(id);
            if (existingProduct == null)
            {
                return HttpNotFound();
            }

            return View(existingProduct);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                product existingProduct = obj.products.Find(id);
                obj.products.Remove(existingProduct);
                obj.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}