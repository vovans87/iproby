using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iproby.Models;
using iproby.Data_Model;

namespace iproby.Controllers
{
    public class RegistrationController : Controller
    {
        private iproby94_cust_dbEntities db = new iproby94_cust_dbEntities();

        public ActionResult Registration()
        {
            return View(new iproby.Data_Model.customer());
        }

        [HttpPost]
        public ActionResult Registration(iproby.Data_Model.customer model)
        {


            db.customers.Add(model);
            db.SaveChanges();
            return Content("Спасибо за регистрацию!", "text/html");
        }
    }
}