//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TimetableSys_T17
{
    using System;
    using System.Collections.Generic;
    
    public partial class Week
    {
        public Week()
        {
            this.Requests = new HashSet<Request>();
        }
    
        public int weekID { get; set; }
        public string week1 { get; set; }
    
        public virtual ICollection<Request> Requests { get; set; }
    }
}