using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TimetableSys_T17.Models
{
    public class RequestModel

    {

        public Int16 round { get; set; }
        public Int16 userId { get; set; }
        public SelectList parks { get; set; }

    }
}