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
    
    public partial class Park
    {
        public Park()
        {
            this.Building1 = new HashSet<Building1>();
        }
    
        public int parkID { get; set; }
        public string parkName { get; set; }
    
        public virtual ICollection<Building1> Building1 { get; set; }
    }
}