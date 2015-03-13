using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TimetablingSys_T17.Controllers
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

        // GET: /Login/
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet] // Used when we initially go into login
        public ActionResult Login()
        {

            return View();

        }

        [HttpPost] // validation - rtn deptlogin from input, validate
        public ActionResult Login(Models.LoginModel deptLogin) {

            if (ModelState.IsValid)
            {

                if (validateDetails(deptLogin.deptIn, deptLogin.password))
                {

                    return RedirectToAction("Temp", "Index");
                }
                else
                {

                }
    

            }
            return View();
        }
	}
}