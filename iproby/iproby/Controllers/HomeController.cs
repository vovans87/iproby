﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using iproby.Data_Model;
using effetto.Sape;



namespace iproby.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        private iproby94_cust_dbEntities db = new iproby94_cust_dbEntities();

        public ActionResult Index(string target)
        {
            var parent_type_arr = (from a in db.announ_type
                                   select a.parent_type).Distinct();
            iproby.Models.announ_clients.parent_type_list = parent_type_arr.ToList();
            if (target != null) { 
            ViewData["title"] = "Work";
            }
            return View();
        }

        public string ReturnLinks()
        {
            string test=SapeFactory.Factory.GetUser().GetHost().GetPageOrDefault().GetLinksAsString();
            return test;
        }

        
    }
}
