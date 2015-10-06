using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations; 

namespace iproby.Models
{
    public class registrator
    {
        [Required]
        [Remote("IsLoginAvailable", "Registration", AdditionalFields = "InitialLogin")]
        public string login { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string first_name { get; set; }
        public string address { get; set; }
        public string mobile { get; set; }
        public string skype { get; set; }
        
    }
}