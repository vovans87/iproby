using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iproby.Data_Model;
using iproby.Models;
using System.Web.Script.Serialization;

namespace iproby.Controllers
{
    public class AnnounController : Controller
    {
        //
        // GET: /Announ/

        public ActionResult Index()
        {
           
            return View();
        }
        
        [HttpPost]
        public string GetChildTypes(string parent_type)
        {
            List<string> type_list = new List<string>();
            if (parent_type == null)
            {
                type_list.Add("Не найден");
                ViewBag.type_arr = type_list.ToList();
                JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
                return JsSerializer.Serialize(type_list);
            }
            else { 
                    var type_arr = (from a in db.announ_type
                                    where a.parent_type.ToLower() == parent_type.ToLower()
                                           select a.type);
                    if (type_arr.Count() == 0) {
                        type_list.Add("Не найден");
                    }
                    JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
                    return JsSerializer.Serialize(type_arr);
                  }
        }
        public ActionResult AddClients()
        {
           
            if (1==1)
            {    
            GetChildTypes(null);
                var parent_type_arr = (from a in db.announ_type
                                        select a.parent_type).Distinct();
                ViewBag.parent_type_arr = parent_type_arr.ToList();
                iproby.Models.announ_clients model = new iproby.Models.announ_clients();
                
                return View(model);
                
            }
            else
            {
                return View();
            }
        }

        private iproby94_cust_dbEntities db = new iproby94_cust_dbEntities();

        [HttpPost]
        public ActionResult AddClients(iproby.Models.announ_clients model)
        {
            if (Session["login"] != null)
            {
                iproby.Data_Model.announ announ = new iproby.Data_Model.announ();
                announ.about = model.about;
                var type_id_arr = (from a in db.announ_type
                                   where a.type == model.type
                                   select a.id);
                int type_id = 0;
                foreach (int item in type_id_arr)
                {
                    type_id = item;
                }
                announ.type_id = type_id;
                announ.description = model.description;
                announ.header = model.header;
                announ.subjects = model.subjects;
                announ.price = model.price;
                db.announs.Add(announ);
                db.SaveChanges();

                iproby.Data_Model.customer_announ customer_announ = new iproby.Data_Model.customer_announ();
                customer_announ.announ_id = announ.id;
                string login = Session["login"].ToString();
                var customer_id_arr = (from a in db.customers
                                       where a.login == login
                                       select a.customer_id);
                int customer_id = 0;
                foreach (int item in customer_id_arr)
                {
                    customer_id = item;
                }
                customer_announ.customer_id = customer_id;
                DateTime Now = DateTime.Now;
                customer_announ.date_from = Now;
                string dateToString = "09/09/9999";
                DateTime dateTo = DateTime.Parse(dateToString, System.Globalization.CultureInfo.InvariantCulture);
                customer_announ.date_to = dateTo;
                customer_announ.active = 1;
                db.customer_announ.Add(customer_announ);
                db.SaveChanges();
                return View("~/Views/Status/AddAnnounSuccess.cshtml");
            }
            else {
                return View("~/Views/Status/NoAuthorization.cshtml");
            }

            
        }

    }
}

