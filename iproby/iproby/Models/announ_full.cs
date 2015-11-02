using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iproby.Models
{
    public class announ_full
    {
        public int announ_id { get; set; }
        public int customer_id { get; set; }
        public string header { get; set; }
        public string description { get; set; }
        public string first_name { get; set; }
        public string address { get; set; }
        public string mobile { get; set; }
        public string about { get; set; }
        public string skype { get; set; }
        public string email { get; set; }
        public string price { get; set; }
    }
}