using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iproby.Models
{
    public class review
    {
        public string header { get; set; }
        public string description { get; set; }
        public int announ_id { get; set; }
        public int customer_id { get; set; }
        public int reviewer_id { get; set; }
        public string first_name { get; set; }
        public string address { get; set; }
        public string avatar { get; set; }
        public Nullable<System.DateTime> date_from { get; set; }

    }
}