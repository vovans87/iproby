//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace iproby.Data_Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class payment
    {
        public int id { get; set; }
        public int announ_id { get; set; }
        public int customer_id { get; set; }
        public string mrchlogin { get; set; }
        public Nullable<double> outsum { get; set; }
        public Nullable<int> invid { get; set; }
        public string description { get; set; }
        public string signaturevalue { get; set; }
        public Nullable<System.DateTime> date_from { get; set; }
        public string status { get; set; }
    
        public virtual announ announ { get; set; }
        public virtual customer customer { get; set; }
    }
}
