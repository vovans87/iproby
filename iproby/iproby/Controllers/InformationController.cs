using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;

namespace iproby.Controllers
{
    public class InformationController : Controller
    {
        //
        // GET: /Information/

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contacts()
        {
            return View();
        }

        [HttpPost]
        public void SendMail(string email)
        {
            SmtpClient smtpClient = new SmtpClient("iproby.ru", 2525);
            smtpClient.Credentials = new System.Net.NetworkCredential("info@iproby.ru", "Qlo9s85@");
            //smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            //smtpClient.EnableSsl = true;
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("info@iproby.ru", "From IPRO footer");
            mail.To.Add(new MailAddress("info@iproby.ru"));
            mail.Subject = "Сообщене на запрос рассылки RSS IPRO";
            mail.Body = "Сообщене на запрос рассылки RSS IPRO от " + email;
            smtpClient.Send(mail);

        }

        [HttpPost]
        public void SendMail(string email, string message)
        {
            SmtpClient smtpClient = new SmtpClient("iproby.ru", 2525);
            smtpClient.Credentials = new System.Net.NetworkCredential("info@iproby.ru", "Qlo9s85@");
            //smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            //smtpClient.EnableSsl = true;
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("info@iproby.ru", "From IPRO notification");
            mail.To.Add(new MailAddress("info@iproby.ru"));
            mail.Subject = "Нотификация с сайта IPRO";
            mail.Body = message;
            smtpClient.Send(mail);

        }


    }
}
