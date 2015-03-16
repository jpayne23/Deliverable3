﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TimetableSys_T17.Controllers
{
    public class LoginController : Controller
    {
        private bool validateDetails(string deptIn, string password)
        {

            bool returnVal = false;

            using (var db = new TimetableDbEntities())
            {

                var user = from userDB in db.User1 from dInfoDB in db.DeptInfoes where userDB.deptID == dInfoDB.deptID && dInfoDB.deptName == deptIn select userDB.email;

                // search for email, i.e. username, email = uniq to display. Are doing usernames or just depts?

                if (user.FirstOrDefault() != null)
                {

                    var inputPass = from userDB in db.User1 from dInfoDB in db.DeptInfoes where userDB.deptID == dInfoDB.deptID && dInfoDB.deptName == deptIn select userDB.password;

                    if (inputPass.FirstOrDefault() == password)
                    {
                        returnVal = true;
                    }
                }
            }

            return returnVal;
        }

        [HttpGet]
        public ActionResult Index()
        {

            return View();

        }

        [HttpPost] // validation - rtn deptlogin from input, validate
        public ActionResult Index(Models.LoginModel deptLogin)
        {


            if (ModelState.IsValid)
            {

                if (validateDetails(deptLogin.deptIn, deptLogin.password))
                {

                   /* return RedirectToRoute(new
                    {
                        Controller = "Temp",
                        action = "Index",
                        id = 1,
                        //id = returnDeptId(deptLogin.deptIn),
                        userName = deptLogin.deptIn,



                    });*/

                }
                else
                {
                    ModelState.AddModelError("", "Bullshit Bruh.");
                }


            }
            return View();
        }
	}
}