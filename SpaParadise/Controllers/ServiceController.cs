using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SpaParadise.Models;

namespace SpaParadise.Controllers
{
    public class ServiceController : Controller
    {

        Context s = new Context();

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddNewServices()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddNewServices(Service serv, HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                string filename = Path.GetFileName(file.FileName);
                string imagepath = Path.Combine(Server.MapPath("~/Content/img/"), filename);
                file.SaveAs(imagepath);
                serv.ServiceImage = "~/Content/img/" + file.FileName;
            }
            else
            {
                serv.ServiceImage = "~/Content/img/";
            }
            s.services.Add(serv);
            s.SaveChanges();
            ViewData["Message"] = serv.ServiceName + "is added sucessfully";
            return RedirectToAction("ServiceList");
        }
        public ActionResult ServiceList()
        {

            List<Service> ml = s.services.ToList();
            return View(ml);
        }
        [HttpGet]
        public ActionResult ViewService(int id)
        {
            Service sobj = new Service();
            sobj = s.services.Where(x => x.ServiceId == id).FirstOrDefault();
            return View(sobj);
        }
        public ActionResult ServiceListAdmin()
        {
            List<Service> ml = s.services.ToList();
            return View(ml);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service sdel = s.services.Find(id);
            if (sdel == null)
            {
                return HttpNotFound();
            }
            return View(sdel);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            Service sdel = s.services.Find(id);
            s.services.Remove(sdel);
            s.SaveChanges();
            return RedirectToAction("CartDetails");

        }
        public ActionResult Edit(int id)
        {

            Service sedit = s.services.SingleOrDefault(movie => movie.ServiceId == id);
            return View(sedit);


        }


        [HttpPost]
        public ActionResult Edit(int id, Service se, HttpPostedFileBase file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        string filename = Path.GetFileName(file.FileName);
                        string imagepath = Path.Combine(Server.MapPath("~/Content/img/"), filename);
                        file.SaveAs(imagepath);
                        se.ServiceImage = "~/Images/" + file.FileName;
                    }
                    else
                    {
                        se.ServiceImage = "~/Images/default";
                    }

                    s.services.AddOrUpdate(se);
                    s.SaveChanges();
                    return RedirectToAction("ServiceListAdmin");
                }
                return View();



            }
            catch
            {
                return View();
            }
        }
        public ActionResult ViewServiceA(int id)
        {
            Service sobj = new Service();
            sobj = s.services.Where(x => x.ServiceId == id).FirstOrDefault();
            return View(sobj);
        }


    }
}