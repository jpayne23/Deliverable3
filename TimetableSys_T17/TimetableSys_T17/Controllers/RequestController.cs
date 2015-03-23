using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TimetableSys_T17.Controllers
{
    public class RequestController : Controller
    {
        
        // GET: /Request/

        protected int ReturnRound()
        {

            int round = 4;

            using (var db = new TimetableDbEntities())
            {

                var req_line = from datesTables in db.DayInfoes select datesTables;
                var dates_list = req_line.ToArray();

                

                for (int i = 0; i < 3; i++)
                {

                            

                }
            }

            return round;
        }

        public void ReturnResult(int round) // This will be round(), database dates determine r 1 - 3 adhoc
        {
            /*I was watching Jeremy Kyle whilst coding this, we don't know who the father is...
              Return result will execute the appropriate submit request, and return, if any a suitable 
              view, i.e. error - adhoc - Room taken etc.*/

            switch(round)
            {

                case 1: 
                    // Do Round 1 - i.e. priority is different
                    Console.WriteLine("temp"); break;
                case 2:
                    // Do Round 2 & 3 here, as neither have a difference. function will take arg, r, 1 | 3
                    Console.WriteLine("temp 2"); break;
                case 3:
                    // Adhoc here, auto approval;
                    Console.WriteLine("temp 3"); break;
                default:
                    // Adhoc? Throw a message? They shouldn't reach this far, unless dates are wrong in db.
                    Console.WriteLine("temp 4"); break;

            }

        }


        public ActionResult Index()
        {

            return View();
        }
	}
}