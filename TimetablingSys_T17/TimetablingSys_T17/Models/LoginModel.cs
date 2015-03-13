using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TimetablingSys_T17.Models
{
    public class LoginModel
    {

        [Required]
        [StringLength(50)]
        [Display(Name="Department ID (Case sensitive for now): ")]
        public string deptIn { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength=4)]
        [Display(Name="Password: ")]
        public string password { get; set; }

        // Salt and pepper later
        // public string PasswordSalt { get; set; }
        
    }
}