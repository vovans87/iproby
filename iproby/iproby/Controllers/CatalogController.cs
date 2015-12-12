using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iproby.Data_Model;
using System.Text.RegularExpressions;

namespace iproby.Controllers
{
    public class CatalogController : Controller
    {
        //
        // GET: /Catalog/
        private iproby94_cust_dbEntities db = new iproby94_cust_dbEntities();
        
        private string SkipHtml(string html) {
            Regex regex = new Regex("\\<[^\\>]*\\>");
            string clear_text = regex.Replace(html, String.Empty);
            clear_text = clear_text.Replace("&nbsp;", " ").Replace("&laquo;", "«").Replace("&raquo;", "»");

            return clear_text;
        }

        private static string TruncateAtWord(string input, int length)
        {
            if (input == null || input.Length < length)
                return input;
            int iNextSpace = input.LastIndexOf(" ", length);
            return string.Format("{0}...", input.Substring(0, (iNextSpace > 0) ? iNextSpace : length).Trim());
        }

        public ActionResult Index(int type_id,string target="workers")
        {
            var announ_id_arr = (from b in db.announs
                                 where b.type_id == type_id
                                 where b.description != null
                                 join db_target1 in db.payments on b.id equals db_target1.announ_id
                                 where db_target1.status.Contains("success")
                                 join db_target in db.announ_target on b.id equals db_target.announ_id
                                 where db_target.target_type.Contains(target)
                                 join db_cust_ann in db.customer_announ on b.id equals db_cust_ann.announ_id
                                 group new { db_cust_ann.date_from, b.id } by b.id into g
                                 select new {id = (from t2 in g select t2.id).Max(),
                                     date_from = (from t3 in g select t3.date_from).Max(),flag = 2 }).Concat(from a in db.announs
                                                                    where a.type_id == type_id
                                                                    where a.description != null
                                                                    where !db.payments.Any(es => (a.id == es.announ_id) && (es.status.Contains("success")))
                                                                    join db_target in db.announ_target on a.id equals db_target.announ_id
                                                                    where db_target.target_type.Contains(target)
                                                                    join db_cust_ann in db.customer_announ on a.id equals db_cust_ann.announ_id
                                                                    select new { a.id, db_cust_ann.date_from, flag = 1 }).OrderByDescending(a => a.flag).ThenByDescending(b => b.date_from);
            var type_arr = (from a in db.announ_type
                                 where a.id == type_id
                                 select a);
            string type_desc = string.Empty;
            string seo_header = string.Empty;
            string seo_keywords = string.Empty;
            string seo_description = string.Empty;
            foreach (var item in type_arr)
            {
                if (target == "workers") { 
                type_desc = SkipHtml(item.description);
                seo_header = item.seo_header;
                seo_keywords = item.seo_keywords;
                seo_description = item.seo_description;
                }
                else if (target == "clients")
                {
                    type_desc = SkipHtml(item.description_clients);
                    seo_header = item.seo_header_clients;
                    seo_keywords = item.seo_keywords_clients;
                    seo_description = item.seo_description_clients;
                }
            }
            int announ_id = 0;
            List<iproby.Models.announ_preview> all_announs = new List<iproby.Models.announ_preview>();
            foreach (var item in announ_id_arr)
            {
                announ_id = item.id;
                var announ_arr = (from a in db.announs
                                  where a.id == announ_id
                                  select a);
                iproby.Models.announ_preview announ = new iproby.Models.announ_preview();
                announ.target = target;
                foreach (var item_inside in announ_arr)
                {
                    announ.description = TruncateAtWord(SkipHtml(item_inside.description.Trim()),360);
                    announ.header = item_inside.header;
                    announ.announ_id = item_inside.id;
                    announ.type_id = item_inside.type_id.Value;
                }
                DateTime date_from = DateTime.Now;
                var customer_id_arr = (from a in db.customer_announ
                                       where a.announ_id == announ_id
                                       select a);
                int customer_id = 0;
                foreach (var item_inside in customer_id_arr)
                {
                    customer_id = item_inside.customer_id.Value;
                    date_from = item_inside.date_from;
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
                    announ.date_from = date_from;
                    announ.type_desc = type_desc;
                    announ.seo_header = seo_header;
                    announ.seo_description = seo_description;
                    announ.seo_keywords = seo_keywords;
                    announ.avatar = item_inside.avatar_cropped;
                }
                if (item.flag == 2)
                {
                    announ.status_vip_flag = 1;
                }
                all_announs.Add(announ);
            }

            return View(all_announs);
        }

        public ActionResult Announs(string target = "workers")
        {
            var announ_id_arr = (from b in db.announs
                                 join db_target in db.payments on b.id equals db_target.announ_id
                                 where db_target.status.Contains("success")
                                 join db_target1 in db.announ_target on b.id equals db_target1.announ_id
                                 where db_target1.target_type.Contains(target)
                                 join db_cust_ann in db.customer_announ on b.id equals db_cust_ann.announ_id
                                 orderby db_cust_ann.date_from descending
                                 group new{db_cust_ann.date_from,b.id} by b.type_id into g
                                 select new
                                 {
                                     announ_id = g.Key,
                                     id = (from t2 in g select t2.id).Max(),
                                     date_from = (from t3 in g select t3.date_from).Max(),
                                     flag=2
                                 }
                                 ).Concat(from a in db.announs
                                 where a.description!=null
                                 where !db.payments.Any(es => (a.id == es.announ_id) && (es.status.Contains("success")))
                                 join db_target in db.announ_target on a.id equals db_target.announ_id
                                 where db_target.target_type.Contains(target)
                                 join db_cust_ann in db.customer_announ on a.id equals db_cust_ann.announ_id
                                 orderby db_cust_ann.date_from descending
                                 group new{db_cust_ann.date_from,a.id} by a.type_id into g
                                 select new
                                 {
                                     announ_id = g.Key,
                                     id = (from t2 in g select t2.id).Max(),
                                     date_from = (from t3 in g select t3.date_from).Max(),
                                     flag = 1
                                 }).OrderByDescending(a => a.flag).ThenByDescending(a => a.date_from);
            int announ_id = 0;
            List<iproby.Models.announ_preview> all_announs = new List<iproby.Models.announ_preview>();
            foreach (var item in announ_id_arr)
            {
                announ_id = item.id;
                var announ_arr = (from a in db.announs
                                  where a.id == announ_id
                                  select a);
                iproby.Models.announ_preview announ = new iproby.Models.announ_preview();
                foreach (var item_inside in announ_arr)
                {
                    announ.header = item_inside.header;
                    announ.announ_id = item_inside.id;
                    announ.description = TruncateAtWord(SkipHtml(item_inside.description),360);
                    announ.type_id = item_inside.type_id.Value;
                }
                var customer_id_arr = (from a in db.customer_announ
                                       where a.announ_id == announ_id
                                       select a);
                int customer_id = 0;
                DateTime date_from = DateTime.Now;
                foreach (var item_inside in customer_id_arr)
                {
                    customer_id = item_inside.customer_id.Value;
                    date_from = item_inside.date_from;
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
                    announ.avatar = item_inside.avatar_cropped;
                    announ.date_from = date_from;
                }
                if (item.flag == 2) {
                    announ.status_vip_flag = 1;
                }
                all_announs.Add(announ);
            }

            return View(all_announs);
        }
    }
}
