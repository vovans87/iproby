using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iproby.Models
{
    public class message
    {
        public string header { get; set; }
        public string text { get; set; }
        public int from_customer { get; set; }
        public int to_customer { get; set; }
        public string fio { get; set; }
        public string email { get; set; }
        public Nullable<System.DateTime> date_from { get; set; }
    }
}