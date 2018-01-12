using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UnileverEmailDelivery.Models;

namespace UnileverEmailDelivery.Controllers
{
    public class HomeController : Controller
    {
        public DBWorker dbWorker;

        public ActionResult Index()
        {
            dbWorker = new DBWorker(@User.Identity.Name);
            ViewBag.isAdmin = dbWorker.User.isAdmin();

            var records = dbWorker.Read();
            return View(records);
        }

        [HttpPost]
        public void SaveContact(int id, string brand, string cause, string email, string concretization, string color)
        {
            var delivery = new Delivery(id, brand, cause, email, concretization, color);

            if (dbWorker == null)
            {
                DBWorkerInitialize();
            }

            if (id == -1)
            {
                dbWorker.Create(delivery);
            }
            else
            {
                dbWorker.Update(delivery);
            }
        }

        [HttpPost]
        public void Delete(int id)
        {
            if (dbWorker == null)
            {
                DBWorkerInitialize();
            }
            dbWorker.Delete(id);
        }

        private void DBWorkerInitialize()
        {
            dbWorker = new DBWorker(User.Identity.Name);
        }
    }
}