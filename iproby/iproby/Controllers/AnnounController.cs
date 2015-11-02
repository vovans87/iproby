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
        private iproby94_cust_dbEntities db = new iproby94_cust_dbEntities();
        private static bool isSaved=false;
        private static bool notLogin = false;

        public ActionResult Index(int announ_id)
        {
            if (isSaved)
            {
                ViewData["isSaved"] = "isSaved";
                isSaved = false;
            }
            if (notLogin)
            {
                ViewData["notLogin"] = "notLogin";
                notLogin = false;
            }
            var announ_arr = (from a in db.announs
                              where a.id == announ_id
                              select a);
            iproby.Models.announ_full announ = new iproby.Models.announ_full();
            foreach (var item_inside in announ_arr)
            {
                announ.description = item_inside.description;
                announ.header = item_inside.header;
                announ.announ_id = item_inside.id;
                announ.about = item_inside.about;
                announ.price = item_inside.price;
            }
            var customer_id_arr = (from a in db.customer_announ
                                   where a.announ_id == announ_id
                                   select a.customer_id);
            int customer_id = 0;
            foreach (int item_inside in customer_id_arr)
            {
                customer_id = item_inside;
            }
            announ.customer_id = customer_id;
            var contact_id_arr = (from a in db.customers
                                  where a.customer_id == customer_id
                                  select a.contact_id);
            int contact_id = 0;
            foreach (int item_inside in contact_id_arr)
            {
                contact_id = item_inside;
            }
            var contact_arr = (from a in db.contacts
                               where a.contact_id == contact_id
                               select a);
            foreach (var item_inside in contact_arr)
            {
                announ.first_name = item_inside.first_name;
                announ.mobile = item_inside.mobile;
                announ.address = item_inside.address;
                announ.skype = item_inside.skype;
                announ.email = item_inside.email;
            }
            
            return View(announ);
        }

        public ActionResult Reviews(int announ_id)
        {

            var reviews_arr = (from a in db.reviews
                               where a.announ_id == announ_id
                               select a);
            List<iproby.Models.review> review_list = new List<iproby.Models.review>();
            foreach (var item in reviews_arr)
            {
                iproby.Models.review review = new iproby.Models.review();
                review.header = item.header;
                review.description = item.description;
                review.date_from = item.date_from;
                review.reviewer_id = item.reviewer_id;
                var contact_id_arr = (from a in db.customers
                                       where a.customer_id == item.reviewer_id
                                       select a.contact_id);
                int contact_id = 0;
                foreach (int item_customer in contact_id_arr)
                {
                    contact_id = item_customer;
                }
                var contact_arr = (from a in db.contacts
                               where a.contact_id == contact_id
                               select a);
                foreach (var item_inside in contact_arr)
                {
                    review.first_name = item_inside.first_name;
                }
                review_list.Add(review);
            }
            return View("~/Views/Announ/ReviewsList.cshtml",review_list);
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

        [HttpPost]
        public ActionResult AddReview(iproby.Models.review model)
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
                iproby.Data_Model.review review = new iproby.Data_Model.review();
                review.reviewer_id = customer_id;
                review.announ_id = model.announ_id;
                review.customer_id = model.customer_id;
                review.header = model.header;
                review.description = model.description;
                DateTime Now = DateTime.Now;
                review.date_from = Now;
                db.reviews.Add(review);
                db.SaveChanges();
                isSaved = true;
                return RedirectToAction("Index", new { announ_id = model.announ_id });
            }
            else {
                notLogin = true;
                return RedirectToAction("Index", new { announ_id = model.announ_id });
            }
        }

        public int GetLikes(int announ_id)
        {
            var likes_arr = (from a in db.likes
                             where a.announ_id == announ_id
                             select a.like_num);
            int like_num = 0;
            foreach (int item in likes_arr)
            {
                like_num = item;
            }
            return like_num;
        }

        public int GetDislikes(int announ_id)
        {
            var likes_arr = (from a in db.likes
                             where a.announ_id == announ_id
                             select a.disline_num);
            int dislike_num = 0;
            foreach (int item in likes_arr)
            {
                dislike_num = item;
            }
            return dislike_num;
        }

        [HttpPost]
        public void AddLike(int announ_id)
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
                var likes_arr = (from a in db.likes
                                       where a.announ_id == announ_id
                                 select a);
                int like_num = 0;
                int dislike_num = 0;
                int like_id = 0;
                foreach (var item in likes_arr)
                {
                    like_num = item.like_num.Value;
                    dislike_num = item.disline_num.Value;
                    like_id = item.id;
                }
                if (like_num == 0 && dislike_num == 0)
                {
                    iproby.Data_Model.like like = new iproby.Data_Model.like();
                    like.announ_id = announ_id;
                    like.like_num = like_num + 1;
                    like.disline_num = dislike_num;
                    db.likes.Add(like);
                    db.SaveChanges();
                }
                else {
                    var like = db.likes.Find(like_id);
                    if (like != null)
                    {
                        like.like_num = like_num+1;
                        db.SaveChanges();
                    }
                }
            }
            else {      }
        }

        [HttpPost]
        public void AddDislike(int announ_id)
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
                var likes_arr = (from a in db.likes
                                 where a.announ_id == announ_id
                                 select a);
                int like_num = 0;
                int dislike_num = 0;
                int like_id = 0;
                foreach (var item in likes_arr)
                {
                    like_num = item.like_num.Value;
                    dislike_num = item.disline_num.Value;
                    like_id = item.id;
                }
                if (like_num == 0 && dislike_num == 0)
                {
                    iproby.Data_Model.like like = new iproby.Data_Model.like();
                    like.announ_id = announ_id;
                    like.like_num = like_num + 1;
                    like.disline_num = dislike_num;
                    db.likes.Add(like);
                    db.SaveChanges();
                }
                else
                {
                    var like = db.likes.Find(like_id);
                    if (like != null)
                    {
                        like.disline_num = dislike_num + 1;
                        db.SaveChanges();
                    }
                }
            }
            else
            {       }
        }
    }
}

