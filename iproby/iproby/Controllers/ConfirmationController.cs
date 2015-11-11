using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Text;
using iproby.Data_Model;

namespace iproby.Controllers
{
    public class ConfirmationController : Controller
    {
        private iproby94_cust_dbEntities db = new iproby94_cust_dbEntities();

        public string GetCode(string login)
        {
            string source = login;
            string hash = string.Empty;
            using (MD5 md5Hash = MD5.Create())
            {
                hash = GetMd5Hash(md5Hash, source);
            }
            return hash;
        }

        public ActionResult Index(string user_code,string login)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                if (VerifyMd5Hash(md5Hash, login, user_code))
                {
                    Session["login"] = login;
                    string login_str = Session["login"].ToString();
                    var contact_id_arr = (from a in db.customers
                                          where a.login == login_str
                                          select a);
                    int customer_id = 0;
                    foreach (var item in contact_id_arr)
                    {
                        customer_id = item.customer_id;
                    }
                    var customers = db.customers.Find(customer_id);

                    if (customers != null)
                    {
                        customers.confirmed_flag = 1;
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index", "Home", new { emailstatus="confirmed"});
                }
                else
                {
                    return RedirectToAction("Index", "Home", new { emailstatus = "notconfirmed" });
                }
            }
        }

        private string GetMd5Hash(MD5 md5Hash, string input)
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
        private bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
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