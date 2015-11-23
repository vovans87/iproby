using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iproby.Models
{
    public class options
    {
        public int customer_id { get; set; }
        public int confirmed_flag { get; set; }
        public int send_email_from_clients { get; set; }
        public int status_vip_flag { get; set; }
    }
}