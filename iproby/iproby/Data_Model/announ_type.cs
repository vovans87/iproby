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
    
    public partial class announ_type
    {
        public announ_type()
        {
            this.announs = new HashSet<announ>();
        }
    
        public int id { get; set; }
        public string type { get; set; }
        public string description { get; set; }
        public string parent_type { get; set; }
    
        public virtual ICollection<announ> announs { get; set; }
    }
}