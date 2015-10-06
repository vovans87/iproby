using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iproby.Data_Model;
using System.Net;
using System.Net.Http;

namespace iproby.Controllers
{
    public class StatusController:Controller
    {

        
        public ActionResult CheckLogin(string login)
        {
          
            if (login.Length > 2)
            {
                using (iproby94_cust_dbEntities db = new iproby94_cust_dbEntities())
                {
                    var Login_Count = (from a in db.customers
                                       where a.login == login
                                       select a.login).Count();
                    if (Login_Count > 0)
                    {

                        return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                    }
                    else
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.OK);
                    }
                }
            }
            else { return new HttpStatusCodeResult(HttpStatusCode.NotFound); }
          
        }

        
    }
}