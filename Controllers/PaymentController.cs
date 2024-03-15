using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.Context;
using WebShop.Models;

namespace WebShop.Controllers
{
    public class PaymentController : Controller
    {
        // GET: Payment
        WebMVC obj = new WebMVC();
        public ActionResult Index()
        {
            if (Session["idUser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                var lstCart = (List<CartModel>)Session["cart"];
                Order objOrder = new Order();
                objOrder.name = "Đonhang-" + DateTime.Now.ToString("yyyyMMddHHmmss");
                objOrder.UserId = int.Parse(Session["idUser"].ToString());
                objOrder.CreateOnUtc = DateTime.Now;
                objOrder.Status = 1;
                obj.Orders.Add(objOrder);
                obj.SaveChanges();

                int inOrderId = objOrder.id;

                List<OrderDetail> lstOrderDetail = new List<OrderDetail>();

                foreach (var item in lstCart)
                {
                    OrderDetail objj = new OrderDetail();
                    objj.Quantity = item.Quantity;
                    objj.OrderId = inOrderId;
                    objj.ProductId = item.Product.id;
                    lstOrderDetail.Add(objj);
                }
                obj.OrderDetails.AddRange(lstOrderDetail);
                obj.SaveChanges();
            }
            return View();
        }

    }
}