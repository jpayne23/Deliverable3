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
    
    public partial class Facility
    {
        public Facility()
        {
            this.Request1 = new HashSet<Request1>();
            this.Room1 = new HashSet<Room1>();
        }
    
        public int facilityID { get; set; }
        public string facilityName { get; set; }
    
        public virtual ICollection<Request1> Request1 { get; set; }
        public virtual ICollection<Room1> Room1 { get; set; }
    }
}
