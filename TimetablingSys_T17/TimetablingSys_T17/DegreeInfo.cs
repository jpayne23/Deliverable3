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
    
    public partial class DegreeInfo
    {
        public DegreeInfo()
        {
            this.Module1 = new HashSet<Module1>();
        }
    
        public int degreeID { get; set; }
        public string degreeName { get; set; }
        public Nullable<int> deptID { get; set; }
    
        public virtual DeptInfo DeptInfo { get; set; }
        public virtual ICollection<Module1> Module1 { get; set; }
    }
}
