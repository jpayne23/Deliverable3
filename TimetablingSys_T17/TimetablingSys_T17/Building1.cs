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
    
    public partial class Building1
    {
        public Building1()
        {
            this.Room1 = new HashSet<Room1>();
            this.DeptInfoes = new HashSet<DeptInfo>();
        }
    
        public int buildingID { get; set; }
        public string buildingName { get; set; }
        public Nullable<int> parkID { get; set; }
    
        public virtual Park Park { get; set; }
        public virtual ICollection<Room1> Room1 { get; set; }
        public virtual ICollection<DeptInfo> DeptInfoes { get; set; }
    }
}