using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SpaParadise.Models;

namespace SpaParadise.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer

        Context custContext = new Context();
        private object a1;

        public ActionResult CustomerName()
        {

            List<Customer> c2 = custContext.customers.ToList();
            return View(c2);
        }


        public ActionResult Details()
        {
            string cphno = Session["CustomerPhNo"].ToString();
            var c = custContext.customers.Where(cust => cust.PhoneNo == cphno).FirstOrDefault();

            return View(c);





        }
        [HttpGet]
        public ActionResult CustomerSignup()
        {

            return View();
        }
        [HttpPost]
        public ActionResult CustomerSignup(Customer customer)
        {
            if (ModelState.IsValid)
            {
                custContext.customers.Add(customer);
                custContext.SaveChanges();
                return RedirectToAction("CustomerDash");
            }
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(SpaParadise.Models.Customer customer)
        {
            using (Context O = new Context())
            {
                Customer cDetails = custContext.customers.SingleOrDefault(a => a.PhoneNo == customer.PhoneNo && a.Password == customer.Password);
                if (cDetails != null)
                {

                    Session["CustomerPhNo"] = cDetails.PhoneNo;
                    return RedirectToAction("CustomerDash", "Customer");
                }
                else
                {
                    ModelState.AddModelError("", "Phone Number or Password is Incorrect!!!");
                }
            }
            return View();
        }
        public ActionResult CustomerDash()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {

            Customer c = custContext.customers.Single(cust => cust.CustomerId == id);
            return View(c);


        }
        //Edit Method
        [HttpPost]
        public ActionResult Edit(int id, Customer c1)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    custContext.customers.AddOrUpdate(c1);
                    custContext.SaveChanges();
                    return RedirectToAction("CustomerDash");
                }
                return View();



            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer cust = custContext.customers.Find(id);
            if (cust == null)
            {
                return HttpNotFound();
            }
            return View(cust);
        }

        [HttpPost]

        public ActionResult Delete(int id)
        {
            Customer cust = custContext.customers.Find(id);
            custContext.customers.Remove(cust);
            custContext.SaveChanges();
            return RedirectToAction("Login");
        }


    }
}