using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iproby.Models
{
    public class announ_preview
    {
        public int announ_id { get; set; }
        public int customer_id { get; set; }
        public string header { get; set; }
        public string description { get; set; }
        public string first_name { get; set; }
        public string address { get; set; }
        public string mobile { get; set; }
        public string search_word { get; set; }
        public string type_desc { get; set; }
        public string seo_header { get; set; }
        public string seo_keywords { get; set; }
        public string seo_description { get; set; }
        public string avatar { get; set; }
        public int type_id { get; set; }
        public string target { get; set; }
        public int status_vip_flag { get; set; }
        public Nullable<System.DateTime> date_from { get; set; }
    }
}