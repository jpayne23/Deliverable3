using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TimetableSys_T17.Controllers
{
    public class CreateRoomController : Controller
    {

        private int isLab(bool lab)
        {

            int labValue = 0;

            if (lab == true)
            {
                labValue = 1;
            }
            else
            {
                labValue = 0;
            }

            return labValue;
        }


        //
        // GET: /CreateRoom/
        public ActionResult Index()
        [HttpGet]
        public ActionResult Index(Models.CreateRoomModel room)
        {
            if (isLab(room.lab) == 1)
            {
                ViewData["Message"] = "Success";
            }
            else 
            {
                ViewData["Message"] = "Failure";
            }

            return View();
        }

	}
}
