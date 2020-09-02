using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Migrations;
using SpaParadise.Models;
using System.Net;

namespace SpaParadise.Controllers
{
    public class CartController : Controller
    {

        Customer c = new Customer();
        Context c1 = new Context();

        public ActionResult CartDetails()
        {
            string cphno = Session["CustomerPhNo"].ToString();
            var c = c1.customers.Where(cust => cust.PhoneNo == cphno).FirstOrDefault();
            var item = c1.carts.Where(a => a.CustomerId == c.CustomerId).ToList();
            return View(item);
        }



        public ActionResult CartEmpty()
        {
            TempData["CartEmpty"] = "Empty Cart";
            return View();
        }



        [HttpGet]
        public ActionResult Proceed(Cart cart)
        {

            string cphno = Session["CustomerPhNo"].ToString();
            var c = c1.customers.Where(cust => cust.PhoneNo == cphno).FirstOrDefault();
            var Cartlist = c1.carts.Where(a => a.CustomerId == c.CustomerId).ToList();
            if (Cartlist.Count == 0)
            {
                double Total = 0;
                TempData["Total"] = Convert.ToString(Total);
                return RedirectToAction("cartEmpty", "CartT");

            }
            else
            {
                double Total = Cartlist.Sum(i => i.Amount);
                TempData["Total"] = Convert.ToString(Total);

                return View(Cartlist);
            }
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cart cdel = c1.carts.Find(id);
            if (cdel == null)
            {
                return HttpNotFound();
            }
            return View(cdel);
        }



        [HttpPost]
        public ActionResult Delete(int id)
        {
            Cart cdel = c1.carts.Find(id);
            c1.carts.Remove(cdel);
            c1.SaveChanges();
            return RedirectToAction("CartDetails");

        }
        public ActionResult Payment()
        {

            string cphno = Session["CustomerPhNo"].ToString();
            var customer1 = c1.customers.Where(cust => cust.PhoneNo == cphno).FirstOrDefault();
            var Cartlist = c1.carts.Where(a => a.CustomerId == customer1.CustomerId).ToList();
            foreach (var item in Cartlist)
            {

                var serv = c1.services.Where(a => a.ServiceId == item.ServiceId).FirstOrDefault();
                c1.bookings.Add(new Booking { CustomerId = item.CustomerId, ServiceId = item.ServiceId, ServiceName = item.ServiceName, Specialist = item.Specialist, BookingTime = item.BookingTime, BookingDate = item.BookingDate, Amount = 100 });



                var customer = c1.customers.Where(c => c.CustomerId == item.CustomerId).FirstOrDefault();
                customer.WalletAmount = customer.WalletAmount - 100;
                c1.customers.AddOrUpdate(customer);
                var cart = c1.carts.Where(d => d.CartId == item.CartId).FirstOrDefault();
                c1.carts.Remove(cart);
                c1.SaveChanges();
            }
            return View();
        }




    }
}