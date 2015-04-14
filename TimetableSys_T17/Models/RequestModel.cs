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

        public IEnumerable<SelectListItem> ID { get; set; }
        public SelectList parkNames { get; set; }

    }
}