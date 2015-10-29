using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iproby.Data_Model;

namespace iproby.Controllers
{
    public class MessageController : Controller
    {
        //
        // GET: /Message/

        private iproby94_cust_dbEntities db = new iproby94_cust_dbEntities();

        public ActionResult MyMessages()
        {
            string login = Session["login"].ToString();
            var customer_id_arr = (from a in db.customers
                                   where a.login == login
                                   select a.customer_id);
            int customer_id = 0;
            foreach (int item in customer_id_arr)
            {
                customer_id = item;
            }
            var messages_arr = (from a in db.messages
                                where a.to_customer_id == customer_id
                                   select a);
            List<iproby.Models.message> all_messages = new List<iproby.Models.message>();
            foreach (var item in messages_arr)
            {
                iproby.Models.message message = new iproby.Models.message();
                message.header = item.header;
                message.text = item.text_mess;
                message.date_from = item.date_from;
                var contact_id_arr = (from a in db.customers
                                      where a.customer_id == item.to_customer_id
                                       select a.contact_id);
                int contact_id = 0;
                foreach (int item_inside in contact_id_arr)
                {
                    contact_id = item_inside;
                }
                var contact_arr = (from a in db.contacts
                                      where a.contact_id == contact_id
                                      select a);
                foreach (var item_inside_inside in contact_arr)
                {
                    message.fio = item_inside_inside.first_name;
                    message.email = item_inside_inside.email;
                }
                all_messages.Add(message);
            }
        
            


            return View();
        }

        public ActionResult SendMessage(iproby.Models.message model)
        {
            if (Session["login"] != null)
            {
                iproby.Data_Model.message message = new iproby.Data_Model.message();
                DateTime Now = DateTime.Now;
                message.date_from = Now;
                string login = Session["login"].ToString();
                var customer_id_arr = (from a in db.customers
                                       where a.login == login
                                       select a.customer_id);
                int customer_id = 0;
                foreach (int item in customer_id_arr)
                {
                    customer_id = item;
                }
                message.from_customer_id = customer_id;
                message.to_customer_id = model.to_customer;
                message.header = model.header;
                message.text_mess = model.text;
                db.messages.Add(message);
                db.SaveChanges();

                return View("~/Views/Status/SaveDetailsSuccess.cshtml");
            }
            else
            {
                return View("~/Views/Status/NoAuthorization.cshtml");
            }
        }


    }
}
