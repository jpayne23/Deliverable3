//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TimetablingSys_T17
{
    using System;
    using System.Collections.Generic;
    
    public partial class DayInfo
    {
        public DayInfo()
        {
            this.Request1 = new HashSet<Request1>();
        }
    
        public int dayID { get; set; }
        public string day { get; set; }
    
        public virtual ICollection<Request1> Request1 { get; set; }
    }
}