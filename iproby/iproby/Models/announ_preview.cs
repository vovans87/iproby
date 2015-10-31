using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iproby.Models
{
    public class announ_preview
    {
        public int message_id { get; set; }
        public int customer_id { get; set; }
        public string header { get; set; }
        public string description { get; set; }
        public string first_name { get; set; }
        public string address { get; set; }
        public string mobile { get; set; }
    }
}