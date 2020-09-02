using QRCoder;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SpaParadise.Models;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;


namespace SpaParadise.Controllers
{
    public class BookingController : Controller
    {
        Context Tc = new Context();
        Customer c = new Customer();
        int count = 1;
        bool flag = true;

        [HttpGet]
        public ActionResult BookNow(int id)
        {
            //Session["CustomerId"] = cDetails.CustomerId;



            Booking t = new Booking();

            string cphno = Session["CustomerPhNo"].ToString();
            var customer = Tc.customers.Where(cust => cust.PhoneNo == cphno).FirstOrDefault();

            var item = Tc.services.Where(a => a.ServiceId == id).FirstOrDefault();
            t.ServiceId = item.ServiceId;
            t.ServiceName = item.ServiceName;
            t.Specialist = item.Specialist;
            t.Amount = item.Amount;

            t.CustomerId = customer.CustomerId;
            string mid = Convert.ToString(t.BookingId);
            return View(t);
        }
        [HttpPost]
        public ActionResult BookNow(Cart t)
        {
            if (ModelState.IsValid)
            {
                Tc.carts.Add(t);
                Tc.SaveChanges();
                return RedirectToAction("CartDetails", "Cart");
            }
            return View();


        }
        public ActionResult Details()
        {
            string cphno = Session["CustomerPhNo"].ToString();
            var c = Tc.customers.Where(cust => cust.PhoneNo == cphno).FirstOrDefault();

            return View(c);

        }
        public ActionResult Edit(int id)
        {

            Booking b = Tc.bookings.Single(cust => cust.BookingId == id);
            return View(b);


        }
        //Edit Method
        [HttpPost]
        public ActionResult Edit(int id, Booking b1)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Tc.bookings.AddOrUpdate(b1);
                    Tc.SaveChanges();
                    return RedirectToAction("Login");
                }
                return View();
            }
            catch
            {
                return View();
            }
        }


        public ActionResult Qrcode()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Qrcode(string InputText)
        {
            string cphno = Session["CustomerPhNo"].ToString();
            var customer = Tc.customers.Where(cust => cust.PhoneNo == cphno).FirstOrDefault();
            int custId = customer.CustomerId;
            var book = Tc.bookings.Where(ti => ti.CustomerId == custId).FirstOrDefault();
            int tid = book.BookingId;
            int cid = book.CustomerId;
            double amt = book.Amount;
            //int mid = book.ser;
            string mnam = book.ServiceName;
            //int not = book.NoOfTickets;
            string loc = book.Location;
            string spl = book.Specialist;
            DateTime dt = (DateTime)book.BookingTime;
            DateTime dd = (DateTime)book.BookingDate;

            var qr = tid + "\n" + cid + "\n" + amt + "\n" + mnam + "\n" + dd + "\n" + loc + "\n" + spl + "\n" + dt;
            using (MemoryStream ms = new MemoryStream())
            {
                QRCodeGenerator oQRCodeGenerator = new QRCodeGenerator();
                QRCodeData oQRCodeData = oQRCodeGenerator.CreateQrCode(qr, QRCodeGenerator.ECCLevel.Q);
                QRCode oQRCode = new QRCode(oQRCodeData);
                using (Bitmap oBitmap = oQRCode.GetGraphic(20))
                {
                    oBitmap.Save(ms, ImageFormat.Png);
                    ViewBag.QRCode = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
                }
            }
            return View();
        }


    }

}





