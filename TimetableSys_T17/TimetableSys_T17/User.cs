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
    
    public partial class User
    {
        public User()
        {
            this.Requests = new HashSet<Request>();
        }
    
        public int userID { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public Nullable<int> admin { get; set; }
        public Nullable<int> deptID { get; set; }
    
        public virtual DeptInfo DeptInfo { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
    }
}
