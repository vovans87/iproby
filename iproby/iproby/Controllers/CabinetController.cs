using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iproby.Data_Model;

namespace iproby.Controllers
{
    public class CabinetController : Controller
    {
        //
        // GET: /Cabinet/

        private iproby94_cust_dbEntities db = new iproby94_cust_dbEntities();

        public ActionResult Index()
        {
            if (Session["login"] != null)
            {
                string login = Session["login"].ToString();
                var contact_id_arr = (from a in db.customers
                                      where a.login == login
                                      select a);
                int contact_id = 0; int customer_id = 0;
                foreach (var item in contact_id_arr)
                {
                    contact_id = item.contact_id;
                    customer_id = item.customer_id;
                }
                var contact_arr = (from a in db.contacts
                                   where a.contact_id == contact_id
                                   select a);
                var announ_id_arr = (from a in db.customer_announ
                                  where a.customer_id == customer_id
                                   select a.announ_id);
                int announ_id = 0;
                foreach (int item in announ_id_arr)
                {
                    announ_id = item;
                }
                var announ_arr = (from a in db.announs
                                  where a.id == announ_id
                                  select a);
                iproby.Models.announ_details details = new iproby.Models.announ_details();
                
                foreach (var item in announ_arr)
                {
                    details.price = item.price;
                    details.subjects = item.subjects;
                    details.header = item.header;
                    details.description = item.description;
                    details.about = item.about;
                }
                foreach (var item in contact_arr)
                {
                    details.address = item.address;
                    details.email = item.email;
                    details.skype = item.skype;
                    details.first_name = item.first_name;
                    details.login = Session["login"].ToString();
                    details.mobile = item.mobile;
                    details.password = string.Empty;
                }
                return View(details);
            }
            else {
                return View("~/Views/Status/NoAuthorization.cshtml");
            }
        }

        public ActionResult EditDetails()
        {
            if (Session["login"] != null) {
                string login = Session["login"].ToString();
                var contact_id_arr = (from a in db.customers
                                      where a.login == login
                                      select a.contact_id);
                int contact_id = 0;
                foreach (int item in contact_id_arr)
                {
                    contact_id = item;
                }
                var contact_arr = (from a in db.contacts
                                   where a.contact_id == contact_id
                                      select a);
                iproby.Models.announ_details regist = new iproby.Models.announ_details();
                foreach (var item in contact_arr)
                {
                    regist.address = item.address;
                    regist.email = item.email;
                    regist.skype = item.skype;
                    regist.first_name = item.first_name;
                    regist.login = Session["login"].ToString();
                    regist.mobile = item.mobile;
                    regist.password = string.Empty;
                }
                return View(regist);
            } else {
                return View("~/Views/Status/NoAuthorization.cshtml");
            }
        }

        [HttpPost]
        public ActionResult SaveDetails(iproby.Models.announ_details model)
        {
            if (Session["login"] != null)
            {
                string login = Session["login"].ToString();
                var contact_id_arr = (from a in db.customers
                                      where a.login == login
                                      select a.contact_id);
                int contact_id = 0;
                foreach (int item in contact_id_arr)
                {
                    contact_id = item;
                }
                var contact = db.contacts.Find(contact_id);

                if (contact != null)
                {
                    contact.address = model.address;
                    contact.first_name = model.first_name;
                    contact.mobile = model.mobile;
                    contact.skype = model.skype;
                    db.SaveChanges();
                }

                return View("~/Views/Status/SaveDetailsSuccess.cshtml");
            }
            else
            {
                return View("~/Views/Status/NoAuthorization.cshtml");
            }

        }


    }
}
