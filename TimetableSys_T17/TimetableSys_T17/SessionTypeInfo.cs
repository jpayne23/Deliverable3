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
    
    public partial class SessionTypeInfo
    {
        public SessionTypeInfo()
        {
            this.Requests = new HashSet<Request>();
        }
    
        public int sessionTypeID { get; set; }
        public string sessionType { get; set; }
    
        public virtual ICollection<Request> Requests { get; set; }
    }
}