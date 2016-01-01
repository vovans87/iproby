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
        public string email { get; set; }
        public string vkontakte { get; set; }
        public string facebook { get; set; }
        public string skype { get; set; }
        public string search_word { get; set; }
        public string type_desc { get; set; }
        public string subjects { get; set; }
        public string seo_header { get; set; }
        public string seo_keywords { get; set; }
        public string seo_description { get; set; }
        public string avatar { get; set; }
        public int type_id { get; set; }
        public string target { get; set; }
        public int status_vip_flag { get; set; }
        int _from_search_flag = 0;
        public int from_search_flag { get{return _from_search_flag;} set{_from_search_flag=value;} }
        int _is_index_flag = 0;
        public int is_index_flag { get { return _is_index_flag; } set { _is_index_flag = value; } }
        public Nullable<System.DateTime> date_from { get; set; }
    }
}