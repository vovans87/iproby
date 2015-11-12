using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iproby.Models
{
    public class news
    {
        public Nullable<System.DateTime> date_from { get; set; }
        public string header { get; set; }
        public string description { get; set; }
        public string link { get; set; }
    }
}