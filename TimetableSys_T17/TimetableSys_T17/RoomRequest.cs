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
    
    public partial class RoomRequest
    {
        public RoomRequest()
        {
            this.Requests = new HashSet<Request>();
        }
    
        public int roomRequestID { get; set; }
        public Nullable<int> roomID { get; set; }
        public Nullable<int> groupSize { get; set; }
    
        public virtual Room Room { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
    }
}
