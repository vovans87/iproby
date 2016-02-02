using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iproby.Data_Model;


namespace iproby.Controllers
{
    public class AuthorizationController: Controller
    {
        private iproby94_cust_dbEntities db = new iproby94_cust_dbEntities();

        public ActionResult Registration()
        {
            return View(new iproby.Models.registrator());
        }

        [HttpPost]
        public ActionResult Registration(iproby.Models.registrator model)
        {
            if (model.first_name != null) {
                iproby.Data_Model.contact contact = new iproby.Data_Model.contact();
                contact.first_name = model.first_name;
                contact.email = Session["login"].ToString();
                contact.mobile = model.mobile;
                contact.skype = model.skype;
                contact.address = model.address;
                contact.facebook = model.facebook;
                contact.site = model.site;
                contact.vkontakte = model.vkontakte;
                contact.icq = model.icq;
                db.contacts.Add(contact);
                db.SaveChanges();

                var login=Session["login"].ToString();
                var customer_id = (from a in db.customers
                                   where a.login == login
                                   select a.customer_id);

                var customer_ex = db.customers.Find(customer_id.First());
                customer_ex.contact_id = contact.contact_id;
                db.SaveChanges();
                return RedirectToAction("Service","Catalog");
            }
            else { 

            iproby.Data_Model.customer customer = new iproby.Data_Model.customer();
            //customer.contact_id = contact.contact_id;
            customer.login = model.email;
            customer.password = model.password;
            customer.status_id = 0;
            DateTime Now = DateTime.Now;
            customer.date_from = Now;
            string dateToString = "09/09/9999";
            DateTime dateTo = DateTime.Parse(dateToString, System.Globalization.CultureInfo.InvariantCulture);
            customer.date_to = dateTo;
            customer.active = 1;
            db.customers.Add(customer);
            db.SaveChanges();
            Session["fio"] = model.email;
            Session["login"] = model.email;
            InformationController notification = new InformationController();
            ConfirmationController confirmation = new ConfirmationController();
            string code = Session["login"].ToString();
            string hash_login = confirmation.GetCode(code);
            string email_text = @"
            Благодарим за регистрацию на сайте IPRO!

            Ваш логин: " + code + @"
            Пароль: " + model.password + @"
            
            Пожалуйста подтвердите Ваш email перейдя по ссылке http://iproby.ru/Confirmation?user_code=" + hash_login + "&login=" + code + @"
            или скопируйте текст: 'http://iproby.ru/Confirmation?user_code=" + hash_login + "&login=" + code + @"'
            в адресную строку браузера и нажмите enter.

            Спасибо!


            Письмо сгенерировано автоматически, если у вас есть вопросы пишите на почту info@iproby.ru";
            notification.SendMail(model.email, email_text);
            return View("~/Views/Registration/RegistrationStep2.cshtml");
            }
            
        }

        public ActionResult Login()
        {
            return View(new iproby.Data_Model.customer());
        }


        [HttpPost]
        public ActionResult Login(iproby.Data_Model.customer model)
        {
                var Login_Count = (from a in db.customers
                                   where a.login == model.login
                                   where a.password == model.password
                                   select a.login).Count();
                if (Login_Count > 0)
                {
                    Session["login"] = model.login;
                    var contact_id_arr = (from a in db.customers
                                       where a.login == model.login
                                       where a.password == model.password
                                      select a.contact_id);
                    int contact_id=0;
                    foreach (int item in contact_id_arr)
                    {
                        contact_id = item;
                    }
                    var fio_arr = (from a in db.contacts
                               where a.contact_id == contact_id
                                       select a.first_name);
                    string fio = String.Empty;
                    foreach (string item in fio_arr)
                    {
                        fio = item;
                    }
                    Session["fio"] = fio;
                    return View("~/Views/Status/LoginSuccess.cshtml");
                }
                else
                {
                    return View("~/Views/Status/LoginFailed.cshtml");
                }
        }
    }
}