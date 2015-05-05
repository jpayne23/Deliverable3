using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TimetableSys_T17.Controllers
{
    public class TimetableController : Controller
    {
        // GET: Timetable
        public ActionResult Index(string modOrLec, string nameOrCode)
        {
            var db = new TimetableDbEntities();
            var getTimeInfo = from t in db.Requests where t.statusID == 1 select t;
            
            if(modOrLec != null && nameOrCode !=null){
                getTimeInfo.Where(a=>);
            }

            var getTimeArray = getTimeInfo.ToArray();
            List<Models.TimetableModel> timeList = new List<Models.TimetableModel>();
            foreach (var x in getTimeArray)
            {
                Models.TimetableModel tmp = new Models.TimetableModel();
                tmp.moduleID = x.moduleID;
                tmp.periodID = x.periodID;
                tmp.requestID = x.requestID;
                tmp.statusID = x.statusID;
                timeList.Add(tmp);
            }

            return View(timeList);
        }
    }
}