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

        public ActionResult GetResultPayment(iproby.Models.payment model)
        {
            if (Session["login"] != null)
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
                var announ_arr = (from a in db.customer_announ
                                  where a.customer_id == customer_id
                                  select a);
                int announ_id = 0;
                foreach (var item in announ_arr)
                {
                    announ_id = item.announ_id.Value;
                }
                string password2 = "4Rq3BjBS";
                string source = model.outsum + ":" + model.invid + ":" + password2;
                //var my_crc = h.MD5(out_summ + ":" + inv_id + ":" + mrh_pass2 + ":Shp_item=" + shp_item);
                string status = string.Empty;
                using (MD5 md5Hash = MD5.Create())
                {
                    string hash = GetMd5Hash(md5Hash, source);
                    if (VerifyMd5Hash(md5Hash, model.signaturevalue, hash))
                    {
                        status = "successresult";
                    }
                    else
                    {
                        status = "signatureincorrect";
                    }
                }

                iproby.Data_Model.payment payment = new iproby.Data_Model.payment();
                payment.announ_id = announ_id;
                payment.customer_id = customer_id;
                payment.invid = model.invid;
                payment.mrchlogin = model.mrchlogin;
                payment.outsum = model.outsum;
                payment.description = model.desc;
                payment.date_from = DateTime.Now;
                payment.signaturevalue = model.signaturevalue;
                payment.status = status;
                db.payments.Add(payment);
                db.SaveChanges();
                iproby.Models.payment paymentModel = new iproby.Models.payment();
                paymentModel.status_text = "OK" + model.invid + "\n";
                return View("~/Views/Cabinet/OKPayment.cshtml", paymentModel);

            }
            else
            {
                iproby.Models.payment paymentModel = new iproby.Models.payment();
                paymentModel.status_text = "OK" + model.invid + "\n";
                return View("~/Views/Cabinet/OKPayment.cshtml", paymentModel);
            }
        }

        [HttpPost]
        public ActionResult ResultPayment(iproby.Models.payment model)
        {
            if (Session["login"] != null)
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
                var announ_arr = (from a in db.customer_announ
                                  where a.customer_id == customer_id
                                  select a);
                int announ_id = 0;
                foreach (var item in announ_arr)
                {
                    announ_id = item.announ_id.Value;
                }
                string password2 = "4Rq3BjBS";
                string source = model.outsum + ":" + model.invid + ":" + password2;
                //var my_crc = h.MD5(out_summ + ":" + inv_id + ":" + mrh_pass2 + ":Shp_item=" + shp_item);
                string status = string.Empty;
                using (MD5 md5Hash = MD5.Create())
                {
                    string hash = GetMd5Hash(md5Hash, source);
                    if (VerifyMd5Hash(md5Hash, model.signaturevalue, hash))
                    {
                        status = "successresult";
                    }
                    else {
                        status = "signatureincorrect";
                    }
                }
                
                iproby.Data_Model.payment payment = new iproby.Data_Model.payment();
                payment.announ_id = announ_id;
                payment.customer_id = customer_id;
                payment.invid = model.invid;
                payment.mrchlogin = model.mrchlogin;
                payment.outsum = model.outsum;
                payment.description = model.desc;
                payment.date_from = DateTime.Now;
                payment.signaturevalue = model.signaturevalue;
                payment.status = status;
                db.payments.Add(payment);
                db.SaveChanges();
                iproby.Models.payment paymentModel = new iproby.Models.payment();
                paymentModel.status_text = "OK" + model.invid + "\n";
                return View("~/Views/Cabinet/OKPayment.cshtml", paymentModel);
            }
            else
            {
                iproby.Models.payment paymentModel = new iproby.Models.payment();
                paymentModel.status_text = "OK" + model.invid + "\n";
                return View("~/Views/Cabinet/OKPayment.cshtml", paymentModel);
            }
        }

        [HttpPost]
        public ActionResult SuccessPayment(iproby.Models.payment model)
        {
            if (Session["login"] != null)
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
                var announ_arr = (from a in db.customer_announ
                                      where a.customer_id == customer_id
                                      select a);
                int announ_id = 0;
                foreach (var item in announ_arr)
                {
                    announ_id = item.announ_id.Value;
                }
            iproby.Data_Model.payment payment = new iproby.Data_Model.payment();
            payment.announ_id = announ_id;
            payment.customer_id = customer_id;
            payment.invid = model.invid;
            payment.mrchlogin = model.mrchlogin;
            payment.outsum = model.outsum;
            payment.description = model.desc;
            payment.date_from = DateTime.Now;
            payment.signaturevalue = model.signaturevalue;
            payment.status = "success";
            db.payments.Add(payment);
            db.SaveChanges();
            return RedirectToAction("EditOptions", "Cabinet"); 

            }else{
                return RedirectToAction("EditOptions", "Cabinet"); 
            }
        }

        [HttpPost]
        public ActionResult FailPayment(iproby.Models.payment model)
        {
            if (Session["login"] != null)
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
                var announ_arr = (from a in db.customer_announ
                                  where a.customer_id == customer_id
                                  select a);
                int announ_id = 0;
                foreach (var item in announ_arr)
                {
                    announ_id = item.announ_id.Value;
                }
                iproby.Data_Model.payment payment = new iproby.Data_Model.payment();
                payment.announ_id = announ_id;
                payment.customer_id = customer_id;
                payment.invid = model.invid;
                payment.mrchlogin = model.mrchlogin;
                payment.outsum = model.outsum;
                payment.description = model.desc;
                payment.date_from = DateTime.Now;
                payment.signaturevalue = model.signaturevalue;
                payment.status = "fail";
                db.payments.Add(payment);
                db.SaveChanges();
                return RedirectToAction("EditOptions", "Cabinet");

            }
            else
            {
                return RedirectToAction("EditOptions", "Cabinet");
            }
        }

        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
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

    }
}
