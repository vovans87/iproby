using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iproby.Data_Model;

namespace iproby.Controllers
{
    public class CatalogController : Controller
    {
        //
        // GET: /Catalog/
        private iproby94_cust_dbEntities db = new iproby94_cust_dbEntities();

        public ActionResult Index(int type_id,string target="workers")
        {
            var announ_id_arr = (from a in db.announs
                                 where a.type_id == type_id
                                 where a.description!=null
                                 join db_target in db.announ_target on a.id equals db_target.announ_id where db_target.target_type.Contains(target)
                                 join db_cust_ann in db.customer_announ on a.id equals db_cust_ann.announ_id
                                 orderby db_cust_ann.date_from descending
                                 where db_target.target_type.Contains(target)
                                 select a);
            var type_arr = (from a in db.announ_type
                                 where a.id == type_id
                                 select a);
            string type_desc = string.Empty;
            string seo_header = string.Empty;
            foreach (var item in type_arr)
            {
                type_desc = item.description;
                seo_header = item.seo_header;
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
                foreach (var item_inside in announ_arr)
                {
                    announ.description = item_inside.description.Trim();
                    announ.header = item_inside.header;
                    announ.announ_id = item_inside.id;
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
                    announ.avatar = item_inside.avatar_cropped;
                }
                all_announs.Add(announ);
            }

            return View(all_announs);
        }

        public ActionResult Announs(string target = "workers")
        {
            var announ_id_arr = (from a in db.announs
                                 where a.description!=null
                                 join db_target in db.announ_target on a.id equals db_target.announ_id
                                 where db_target.target_type.Contains(target)
                                 join db_cust_ann in db.customer_announ on a.id equals db_cust_ann.announ_id
                                 orderby db_cust_ann.date_from descending
                                 group a by a.type_id into g
                                 select new
                                 {
                                     announ_id = g.Key,
                                     id = (from t2 in g select t2.id).Max()
                                 });
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
                    announ.description = item_inside.description;
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
                all_announs.Add(announ);
            }

            return View(all_announs);
        }
    }
}
