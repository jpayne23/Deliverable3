using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimetableSys_T17.Models;

namespace TimetableSys_T17.Controllers
{
    public class RequestController : Controller
    {
        
        // GET: /Request/
        // Done by Adam Dryden.

        /***********************
         *  NOTES: 
         *  
         *  Check if changes made to edit,
         *  pass 0/1 for edit? maybe catch it
         *  in the view using js? 
         *  
         * *********************/

        protected int ReturnRound()
        {

            int round = 4;
            bool satisfied = false;
            
            using (var db = new TimetableDbEntities())
            {

                var req_line = from datesTables in db.RoundInfoes select datesTables;
                var dates_list = req_line.ToList();

                DateTime current_date = DateTime.Today;

                int rs_day = 0, rs_month = 0, re_day = 0, re_month = 0;
                

                for (int i = 0; i < 3; i++)
                {

                    String[] round_start = dates_list[i].startDate.Split(new String[] { "/" }, StringSplitOptions.None);
                    String[] round_end = dates_list[i].endDate.Split(new String[] { "/" }, StringSplitOptions.None);

                    try
                    {
                        // Convert strings to integers :-

                        rs_day = Convert.ToInt16(round_start[0]); // round start day. 
                        rs_month = Convert.ToInt16(round_start[1]); // round start month           

                        re_day = Convert.ToInt16(round_end[0]); // round end day
                        re_month = Convert.ToInt16(round_end[1]); // round end month

                    }
                    catch (FormatException error)
                    {

                        Console.WriteLine("ReqCon L51 - ConStrToInt" + error); // Render error view.

                    }
                    finally
                    {

                        if (current_date.Day >= rs_day && current_date.Day <= re_day && current_date.Month >= rs_month && current_date.Month <= re_month) {

                            round = i + 1;
                            satisfied = true;
                        
                        }
                    }

                    if (satisfied)
                    {
                        break;
                    }
                }
            }

            return round;
        }

        protected void SubmitRoundI()
        {
             
        }

        protected void SubmitRoundII_III()
        { 

        }

        protected void SubmitAdHoc()
        {
            // If Adhoc finds a pending request, it'll decline pending and approve this
            // Because there should not be any pending at this stage - assume admin is on it.
            

        }

        protected void SubmitEdit()
        {

            // Edit, check before this is executed if edit != original

        }

        public void ReturnResult(Boolean edit)
        {
            /*I was watching Jeremy Kyle whilst coding this, we don't know who the father is...
              Return result will execute the appropriate submit request, and return, if any, a suitable 
              view, i.e. error - adhoc - Room taken etc.*/

            if (!edit)
            {
                int round = ReturnRound();

                switch (round)
                {

                    case 1:
                        // Do Round 1 - i.e. priority is different
                        SubmitRoundI(); break;
                    case 2:
                        // Do Round 2 & 3 here, as neither have a difference. function will take arg, r, 1 | 3
                        SubmitRoundII_III(); break;
                    case 3:
                        // Adhoc here, auto approval;
                        SubmitAdHoc(); break;
                    default:
                        // Adhoc? Throw a message? They shouldn't reach this far, unless dates are wrong in db.
                        Console.WriteLine("temp 4"); break; // Return Error Page/Message

                }
            }
            else
            {


                SubmitEdit();

            }

        
        }

        public SelectList returnParks()
        {
            using (var db = new TimetableDbEntities())
            {

                var park_names = from parkTable in db.Parks select parkTable.parkName;
                var keep_context = park_names.ToList();
                SelectList parks = new SelectList(keep_context, "Name");
                

                return parks;
            }
        }


        public ActionResult Index()
        {
            var model = new RequestModel();

            model.parkNames = returnParks();
      
            return View(model);

        }
	}
}