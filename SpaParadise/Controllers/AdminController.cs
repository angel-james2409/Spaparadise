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
    public class AdminController : Controller
    {

        // GET: Admin
        Context a = new Context();

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(SpaParadise.Models.Admin admin)
        {
            using (Context O = new Context())
            {
                Admin adDetails = a.admins.SingleOrDefault(a => a.PhoneNo == admin.PhoneNo && a.Password == admin.Password);
                if (adDetails != null)
                {

                    Session["AdminId"] = adDetails.AdminId;
                    return RedirectToAction("AdminDash", "Admin");
                }
                else
                {
                    ModelState.AddModelError("", "Phone Number or Password is Incorrect!!!");
                }
            }
            return View();
        }

        //show method
        public ActionResult Details(int id)
        {
            Admin ad = a.admins.Single(cust => cust.AdminId == id);
            return View(ad);

        }

        public ActionResult Create()
        {
            return View();
        }
        //create method

        [HttpPost]
        public ActionResult Create(Admin a1)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    a.admins.Add(a1);
                    a.SaveChanges();
                    return RedirectToAction("AdminDash");
                }
                return View();


            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {

            Admin ad = a.admins.Single(cust => cust.AdminId == id);
            return View(ad);


        }
        //Edit Method
        [HttpPost]
        public ActionResult Edit(int id, Admin a1)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    a.admins.AddOrUpdate(a1);
                    a.SaveChanges();
                    return RedirectToAction("AdminDash");
                }
                return View();



            }
            catch
            {
                return View();
            }
        }
        public ActionResult AdminDash()
        {
            return View();
        }



        public ActionResult Index()
        {
            return View();
        }





    }
}