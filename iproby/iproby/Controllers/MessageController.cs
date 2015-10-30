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
            if (Session["login"] != null)
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
            var message_id_arr = (from a in db.messages
                                where a.to_customer_id == customer_id
                                group a by a.from_customer_id into g
                                select new
                                  {
                                      from_customer_id = g.Key,
                                      id = (from t2 in g select t2.id).Max()
                                  });
                int message_id = 0;
                List<iproby.Models.message> all_messages = new List<iproby.Models.message>();
                foreach (var item in message_id_arr)
                {
                    message_id = item.id;
                
            var messages_arr = (from a in db.messages
                                      where a.id == message_id
                                select a);
                
            foreach (var item_inside in messages_arr)
            {
                iproby.Models.message message = new iproby.Models.message();
                message.id = message_id;
                message.header = item_inside.header;
                message.text = item_inside.text_mess;
                message.date_from = item_inside.date_from;
                var contact_id_arr = (from a in db.customers
                                      where a.customer_id == item_inside.from_customer_id
                                       select a.contact_id);
                int contact_id = 0;
                foreach (int item_inside_more in contact_id_arr)
                {
                    contact_id = item_inside_more;
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
                }
                return View(all_messages);
            }
            else
            {
                return View();
            }
         }

        public int CountMessages()
        {
            if (Session["login"] != null)
            {
                string login = Session["login"].ToString();
                var customer_id_arr = (from a in db.customers
                                       where a.login == login
                                       select a.customer_id);
                int customer_id_in = 0;
                foreach (int item_inside in customer_id_arr)
                {
                    customer_id_in = item_inside;
                }
                var messages_arr = (from a in db.messages
                                    where a.to_customer_id == customer_id_in
                                    select a).Count();
                int count = messages_arr;

                return count;
            }
            else {
                return 0;
            }
        }

        public ActionResult Answer(int message_id)
        {
            if (Session["login"] != null)
            {
                string login = Session["login"].ToString();
                var customer_id_arr = (from a in db.customers
                                       where a.login == login
                                       select a.customer_id);
                int customer_id = 0;
                foreach (int item_inside in customer_id_arr)
                {
                    customer_id = item_inside;
                }
                var messages_arr = (from a in db.messages
                                    where a.id == message_id
                                    select a);
                int from_id = 0;
                foreach (var item in messages_arr)
                {
                    from_id = item.from_customer_id;
                }
                var messages_arr_all = (from a in db.messages
                                        where ((a.from_customer_id == from_id && a.to_customer_id == customer_id)
                                        || (a.from_customer_id == customer_id && a.to_customer_id == from_id)
                                        )
                                        select a);
                var contact_id_arr = (from a in db.customers
                                      where a.customer_id == from_id
                                      select a.contact_id);
                int contact_id = 0;
                foreach (int item_inside_more in contact_id_arr)
                {
                    contact_id = item_inside_more;
                }
                var contact_arr = (from a in db.contacts
                                   where a.contact_id == contact_id
                                   select a);
                List<iproby.Models.message> all_messages = new List<iproby.Models.message>();
                foreach (var item in messages_arr_all)
                {
                    iproby.Models.message message = new iproby.Models.message();
                    message.header = item.header;
                    message.text = item.text_mess;
                    message.date_from = item.date_from;
                    message.from_customer = from_id;
                    if (item.from_customer_id == customer_id)
                    {
                        message.is_answer = 1;
                    };
                    foreach (var item_inside_inside in contact_arr)
                    {
                        message.fio = item_inside_inside.first_name;
                        message.email = item_inside_inside.email;
                    }
                    all_messages.Add(message);
                }
                return View(all_messages);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
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
                                       select a);
                int customer_id = 0; int contact_id = 0;
                foreach (var item in customer_id_arr)
                {
                    customer_id = item.customer_id;
                    contact_id = item.contact_id;
                }
                var contact_arr = (from a in db.contacts
                                   where a.contact_id == contact_id
                                   select a);
                string email = string.Empty; 
                foreach (var item in contact_arr)
                {
                    email = item.email;
                }
                message.from_customer_id = customer_id;
                message.to_customer_id = model.to_customer;
                message.header = model.header;
                message.text_mess = model.text;
                db.messages.Add(message);
                db.SaveChanges();
                InformationController notification = new InformationController();
                notification.SendMail(email, "Вам пришло сообщение на сайте IPRO. Пожалуйста проверьте в личном кабинете.");
                if (model.form_flag != null && model.form_flag == 1) {
                    return RedirectToAction("Answer", "Message", new { message_id = model.message_id});
                }
                else
                { 
                    return View("~/Views/Status/SendMessageSuccess.cshtml");
                }
            }
            else
            {
                return View("~/Views/Status/NoAuthorization.cshtml");
            }
        }


    }
}
