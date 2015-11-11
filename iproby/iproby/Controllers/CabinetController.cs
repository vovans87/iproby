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
        private static bool isSaved = false;

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
                    details.facebook = item.facebook;
                    details.site = item.site;
                    details.vkontakte = item.vkontakte;
                    details.icq = item.icq;
                    details.first_name = item.first_name;
                    details.login = Session["login"].ToString();
                    details.mobile = item.mobile;
                    details.password = string.Empty;
                }
                if (isSaved)
                {
                    ViewData["isSaved"] = "isSaved";
                    isSaved = false;
                }
                ViewData["login"] = "isLogin";
                return View(details);
            }
            else
            {
                return View();
            }
        }

        public ActionResult EditDetails()
        {
            if (Session["login"] != null)
            {
                ViewData["login"] = "isLogin";
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
                    regist.facebook = item.facebook;
                    regist.site = item.site;
                    regist.vkontakte = item.vkontakte;
                    regist.icq = item.icq;
                    regist.first_name = item.first_name;
                    regist.login = Session["login"].ToString();
                    regist.mobile = item.mobile;
                    regist.password = string.Empty;
                }
                return View(regist);
            }
            else
            {
                ViewData["login"] = "notLogin";
                return View();
            }
        }

        [HttpPost]
        public ActionResult SaveDetails(iproby.Models.announ_details model)
        {
            if (Session["login"] != null)
            {
                ViewData["login"] = "isLogin";
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
                    contact.facebook = model.facebook;
                    contact.site = model.site;
                    contact.vkontakte = model.vkontakte;
                    contact.icq = model.icq;
                    db.SaveChanges();
                }
                isSaved = true;
                return RedirectToAction("Index");
            }
            else
            {
                isSaved = false;
                return RedirectToAction("Index");
            }

        }

        public ActionResult EditAnnoun()
        {
            if (Session["login"] != null)
            {
                ViewData["login"] = "isLogin";
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
                iproby.Models.announ_clients announ = new iproby.Models.announ_clients();

                foreach (var item in announ_arr)
                {
                    announ.price = item.price;
                    announ.subjects = item.subjects;
                    announ.header = item.header;
                    announ.description = item.description;
                    announ.about = item.about;
                }
                if (isSaved)
                {
                    ViewData["isSaved"] = "isSaved";
                    isSaved = false;
                }
                return View(announ);
            }
            else
            {
                ViewData["login"] = "notLogin";
                return View();
            }
        }


        [HttpPost]
        public ActionResult SaveAnnoun(iproby.Models.announ_clients model)
        {
            if (Session["login"] != null)
            {
                ViewData["login"] = "isLogin";
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
                var announ_id_arr = (from a in db.customer_announ
                                     where a.customer_id == customer_id
                                     select a.announ_id);
                int announ_id = 0;
                foreach (int item in announ_id_arr)
                {
                    announ_id = item;
                }
                var announ = db.announs.Find(announ_id);

                if (announ != null)
                {
                    announ.about = model.about;
                    announ.description = model.description;
                    announ.header = model.header;
                    db.SaveChanges();
                }
                isSaved = true;
                return RedirectToAction("Index");
            }
            else
            {
                ViewData["login"] = "notLogin";
                return View();
            }
        }

        public ActionResult MyMessages()
        {

            return View();
        }

        public ActionResult EmptySession()
        {
            ViewData["login"] = null;
            Session["login"] = null;
            Session["fio"] = null;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult EditOptions(iproby.Models.options model)
        {
            string login = Session["login"].ToString();
            var contact_id_arr = (from a in db.customers
                                  where a.login == login
                                  select a);
            int customer_id = 0;
            foreach (var item in contact_id_arr)
            {
                customer_id = item.customer_id;
            }
            var options_arr = (from a in db.options
                                  where a.customer_id == customer_id
                                  select a);
            int flag_send_from_clients = 0;
            foreach (var item in options_arr)
            {
                flag_send_from_clients = item.send_email_from_clients_flag.Value;
            }
            iproby.Models.options options = new iproby.Models.options();
            options.customer_id = customer_id;
            options.send_email_from_clients = flag_send_from_clients;

            return View(options);
        }

        [HttpPost]
        public ActionResult SaveOptions(iproby.Data_Model.option model)
        {
            string login = Session["login"].ToString();
            var contact_id_arr = (from a in db.customers
                                  where a.login == login
                                  select a);
            int customer_id = 0;
            foreach (var item in contact_id_arr)
            {
                customer_id = item.customer_id;
            }
            var option_id = (from a in db.options
                                   where a.customer_id == customer_id
                                  select a.id);
            int opt_id = 0;
            foreach (var item in option_id)
            {
                opt_id = item;
            }
            var options = db.options.Find(opt_id);

            if (options != null)
            {
                options.send_email_from_clients_flag = model.send_email_from_clients_flag;
                db.SaveChanges();
            }
            return RedirectToAction("EditOptions", "Cabinet"); ;
        }
    }
}
