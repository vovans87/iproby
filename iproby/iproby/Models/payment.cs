using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iproby.Models
{
    public class payment
    {
        public string mrchlogin { get; set; }
        public float outsum { get; set; }
        public int invid { get; set; }
        public string desc { get; set; }
        public string signaturevalue { get; set; }
        public string password1 { get; set; }
        public string status_text { get; set; }
    }
}