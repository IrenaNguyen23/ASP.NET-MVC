 using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.Context;
using static WebShop.Common;

namespace WebShop.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        // GET: Admin/Product
        WebMVC obj = new WebMVC();
        public ActionResult Index()
        {
            var listProduct = obj.products.ToList();
            return View(listProduct);
        }

        [HttpGet]
        public ActionResult Create()
        {
            Common objCom = new Common();
            var listCat = obj.categories.ToList();
            ListToDataTableConverter converter = new ListToDataTableConverter();
            DataTable dtCategory = converter.ToDataTable(listCat);
            ViewBag.ListCategory = objCom.ToSelectList(dtCategory, "id", "name");

            var listBrand = obj.brands.ToList();
            DataTable dtBrand = converter.ToDataTable(listBrand);
            ViewBag.ListBrand = objCom.ToSelectList(dtBrand, "id", "name");

            List<ProductType> lstProType = new List<ProductType>();
            ProductType objProType = new ProductType();
            objProType.id = 01;
            objProType.Name = "Sale";
            lstProType.Add(objProType);

            objProType = new ProductType();
            objProType.id = 02;
            objProType.Name = "Recommend";
            lstProType.Add(objProType);

            DataTable dtProducttype = converter.ToDataTable(lstProType);
            ViewBag.ListProductType = objCom.ToSelectList(dtProducttype, "id", "name");

            return View();
        }

        [HttpPost]
        public ActionResult Create(product objproduct)
        {
            try
            {
                if(objproduct.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(objproduct.ImageUpload.FileName);
                    string extension = Path.GetExtension(objproduct.ImageUpload.FileName);
                    fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyymmddhhss")) + extension;
                    objproduct.image = fileName;
                    objproduct.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items/"), fileName));
                }
                obj.products.Add(objproduct);
                obj.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("index");           
            }
            
        }

        [HttpGet]
        public ActionResult Detail(int id)
        {
            var objProduct = obj.products.Where(obj => obj.id == id).FirstOrDefault();
            return View(objProduct);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objProduct = obj.products.Where(obj => obj.id == id).FirstOrDefault();
            return View(objProduct);
        }

        [HttpPost]
        public ActionResult Delete(product product)
        {
            var objProduct = obj.products.Where(n => n.id == product.id).FirstOrDefault();
            obj.products.Remove(objProduct);
            obj.SaveChanges();
            return RedirectToAction("index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var objProduct = obj.products.Where(obj => obj.id == id).FirstOrDefault();
            return View(objProduct);
        }

        [HttpPost]
        public ActionResult Edit(product objproduct)
        {
            if(objproduct.ImageUpload != null) 
            {
                string fileName = Path.GetFileNameWithoutExtension(objproduct.ImageUpload.FileName);
                string extension = Path.GetExtension(objproduct.ImageUpload.FileName);
                fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyymmddhhss")) + extension;
                objproduct.image = fileName;
                objproduct.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items/"), fileName));
            }
            obj.Entry(objproduct).State = EntityState.Modified;
            obj.SaveChanges();
            return RedirectToAction("index");
        }
    }
}