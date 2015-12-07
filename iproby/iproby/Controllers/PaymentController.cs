using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iproby.Data_Model;
using System.Security.Cryptography;
using System.Text;

namespace iproby.Controllers
{
    public class PaymentController : Controller
    {
        private iproby94_cust_dbEntities db = new iproby94_cust_dbEntities();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ResultPayment()
        {
            iproby.Data_Model.payment model = new iproby.Data_Model.payment();
            model.invid = Int32.Parse(GetPrm("InvId"));
            model.outsum= Int32.Parse(GetPrm("OutSum"));
            string sCrc = GetPrm("SignatureValue");
            model.description = GetPrm("Description");
            DateTime date_from = DateTime.Now;
            model.date_from = date_from;
            model.signaturevalue = GetPrm("SignatureValue");
            model.Code = GetPrm("Code");
            model.IncAccount = Int32.Parse(GetPrm("IncAccount"));
            model.IncCurrLabel = GetPrm("IncCurrLabel");
            model.IncSum = Int32.Parse(GetPrm("IncSum"));
            model.Info = GetPrm("Info");
            model.OutCurrLabel = GetPrm("OutCurrLabel");
            model.PaymentMethod = GetPrm("PaymentMethod");
            model.state = GetPrm("State");

                     var invid_arr = (from a in db.payments
                                      where a.invid == model.invid
                                      select a);
                int customer_id = 0;
                foreach (var item in invid_arr)
                {
                    customer_id = item.customer_id;
                }
                var announ_arr = (from a in db.customer_announ
                                  where a.customer_id == customer_id
                                  select a);
                int announ_id = 0;
                foreach (var item in announ_arr)
                {
                    announ_id = item.announ_id.Value;
                }
                var contact_arr = (from a in db.customers
                                  where a.customer_id == customer_id
                                   join db_target1 in db.contacts on a.contact_id equals db_target1.contact_id
                                   select db_target1);
                string email = string.Empty;
                foreach (var item in contact_arr)
                {
                    email = item.email;
                }
                iproby.Models.payment paym = new iproby.Models.payment();
                string status = "success";
                string sCrcBase = string.Format("{0}:{1}:{2}",
                                                 paym.outsum, model.invid, paym.password2);
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                byte[] bSignature = md5.ComputeHash(Encoding.ASCII.GetBytes(sCrcBase));
                StringBuilder sbSignature = new StringBuilder();
                foreach (byte b in bSignature)
                    sbSignature.AppendFormat("{0:x2}", b);
                string sMyCrc = sbSignature.ToString();
                if (sMyCrc.ToUpper() != model.signaturevalue.ToUpper())
                {
                    status = "signatureincorrect-" + sMyCrc.ToUpper() + "-" + sCrcBase + "-" + model.signaturevalue.ToUpper() + "-" + model.outsum + model.invid;
                }
                model.announ_id = announ_id;
                model.customer_id = customer_id;
                model.status = status;
                db.payments.Add(model);
                db.SaveChanges();
                iproby.Models.payment paymentModel = new iproby.Models.payment();
                paymentModel.status_text = "OK" + model.invid + "\n";
                InformationController info = new InformationController();
                string text_email=@"Благодарим за платеж!

                Данные платежа:
                id Объявления: "+announ_id+@"
                id Пользователя: "+customer_id+@"
                id Транзакции: "+model.invid+@"
                Сумма платежа: "   +model.outsum+@"
                Время платежа: " +date_from.ToString()+@"
                Статус: "+status+@"


                Это сообщение сгенерировано автоматически. По всем вопросам пишите на info@iproby.ru";
                info.SendMail(email, text_email);
                return View("~/Views/Cabinet/OKPayment.cshtml", paymentModel);
            
        }

        [HttpPost]
        public ActionResult SuccessPayment(iproby.Data_Model.payment model)
        {
                var invid_arr = (from a in db.payments
                                 where a.invid == model.invid
                                 select a);
                int customer_id = 0;
                foreach (var item in invid_arr)
                {
                    customer_id = item.customer_id;
                }
                var announ_arr = (from a in db.customer_announ
                                      where a.customer_id == customer_id
                                      select a);
                int announ_id = 0;
                foreach (var item in announ_arr)
                {
                    announ_id = item.announ_id.Value;
                }
                iproby.Models.payment paym = new iproby.Models.payment();
                string status = "success";
                string sCrcBase = string.Format("{0}:{1}:{2}",
                                                 paym.outsum, model.invid, paym.password1);
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                byte[] bSignature = md5.ComputeHash(Encoding.ASCII.GetBytes(sCrcBase));
                StringBuilder sbSignature = new StringBuilder();
                foreach (byte b in bSignature)
                    sbSignature.AppendFormat("{0:x2}", b);
                string sMyCrc = sbSignature.ToString();
                if (sMyCrc.ToUpper() != model.signaturevalue.ToUpper())
                {
                    status = "signatureincorrect-" + sMyCrc.ToUpper() + "-" + sCrcBase + "-" + model.signaturevalue.ToUpper() + "-" + model.outsum + model.invid;
                }

            iproby.Data_Model.payment payment = new iproby.Data_Model.payment();
            payment.announ_id = announ_id;
            payment.customer_id = customer_id;
            payment.invid = model.invid;
            payment.mrchlogin = model.mrchlogin;
            payment.outsum = model.outsum;
            payment.description = model.description;
            payment.date_from = DateTime.Now;
            payment.signaturevalue = model.signaturevalue;
            payment.status = status;
            payment.Code = model.Code;
            payment.IncAccount = model.IncAccount;
            payment.IncCurrLabel = model.IncCurrLabel;
            payment.IncSum = model.IncSum;
            payment.Info = model.Info;
            payment.OutCurrLabel = model.OutCurrLabel;
            payment.PaymentMethod = model.PaymentMethod;
            payment.state = model.state;
            db.payments.Add(payment);
            db.SaveChanges();
            return RedirectToAction("EditOptions", "Cabinet"); 
        }

        [HttpPost]
        public ActionResult FailPayment(iproby.Data_Model.payment model)
        {
                var invid_arr = (from a in db.payments
                                 where a.invid == model.invid
                                 select a);
                int customer_id = 0;
                foreach (var item in invid_arr)
                {
                    customer_id = item.customer_id;
                }
                var announ_arr = (from a in db.customer_announ
                                  where a.customer_id == customer_id
                                  select a);
                int announ_id = 0;
                foreach (var item in announ_arr)
                {
                    announ_id = item.announ_id.Value;
                }
                var contact_arr = (from a in db.customers
                                   where a.customer_id == customer_id
                                   join db_target1 in db.contacts on a.contact_id equals db_target1.contact_id
                                   select db_target1);
                string email = string.Empty;
                foreach (var item in contact_arr)
                {
                    email = item.email;
                }
                iproby.Data_Model.payment payment = new iproby.Data_Model.payment();
                payment.announ_id = announ_id;
                payment.customer_id = customer_id;
                payment.invid = model.invid;
                payment.mrchlogin = model.mrchlogin;
                payment.outsum = model.outsum;
                payment.description = model.description;
                DateTime date_from = DateTime.Now;
                payment.date_from = date_from;
                payment.signaturevalue = model.signaturevalue;
                payment.Code = model.Code;
                payment.IncAccount = model.IncAccount;
                payment.IncCurrLabel = model.IncCurrLabel;
                payment.IncSum = model.IncSum;
                payment.Info = model.Info;
                payment.OutCurrLabel = model.OutCurrLabel;
                payment.PaymentMethod = model.PaymentMethod;
                payment.state = model.state;
                payment.status = "fail";
                db.payments.Add(payment);
                db.SaveChanges();
                string text_email = @"К сожалению что-то пошло не так!

                Данные платежа:
                id Объявления: " + announ_id + @"
                id Пользователя: " + customer_id + @"
                id Транзакции: " + model.invid + @"
                Сумма платежа: " + model.outsum + @"
                Время платежа: " + date_from.ToString() + @"
                Статус: fail


                Это сообщение сгенерировано автоматически. По всем вопросам пишите на info@iproby.ru";
                InformationController info = new InformationController();
                info.SendMail(email, text_email);
                return RedirectToAction("EditOptions", "Cabinet");
        }

        static string GetMd5Hash(MD5 md5Hash, string input)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        // Verify a hash against a string.
        static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            // Hash the input.
            string hashOfInput = GetMd5Hash(md5Hash, input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private string GetPrm(string sName)
        {
            string sValue;
            sValue = Request.Form[sName] as string;

            if (string.IsNullOrEmpty(sValue))
                sValue = Request.QueryString[sName] as string;

            if (string.IsNullOrEmpty(sValue))
                sValue = String.Empty;

            return sValue;
        }

    }
}
