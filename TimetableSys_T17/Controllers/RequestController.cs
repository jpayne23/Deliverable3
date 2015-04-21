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


        private TimetableDbEntities _db = new TimetableDbEntities();

        private Int16 ReturnRound()
        {

            Int16 round = 4;
            bool satisfied = false;

            var req_line = from datesTables in _db.RoundInfoes select datesTables;
            var dates_list = req_line.ToList();

            DateTime current_date = DateTime.Today;

            Int16 rs_day = 0, rs_month = 0, re_day = 0, re_month = 0;

            for (Int16 i = 0; i < 3; i++)
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

                    if (current_date.Day >= rs_day && current_date.Day <= re_day && current_date.Month >= rs_month && current_date.Month <= re_month)
                    {

                        round = i++;
                        satisfied = true;

                    }
                }

                if (satisfied)
                {
                    break;
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


        public ActionResult Index()
        {
            RequestModel global_model = new RequestModel();

            return View(global_model);
        }
        /*
        [HttpGet]
        public JsonResult ReturnParks(string input)
        {

            RequestModel local = new RequestModel(); // Sort this out, I'm pretty sure this goes against MVC philosophy. 


            if (input == "")
            {

                var park_names = from parkTable in _db.Parks select parkTable.parkName;
                local.parkName = park_names.ToList();
            }
            else
            {
                var park_names = from parkTable in _db.Parks where parkTable.parkName.Contains(input) select parkTable.parkName;
                local.parkName = park_names.ToList();

            }

            return Json(local, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ReturnBuildings(string input)
        {

            RequestModel local = new RequestModel();

            if (input != "")
            {

                var parkIDs = (from parkTable in _db.Parks where parkTable.parkName.Contains(input) select parkTable.parkID);
                var tempx = parkIDs.FirstOrDefault();
                var temp = from buildingTable in _db.Buildings where buildingTable.parkID == tempx select buildingTable.buildingName;
                var temp2 = temp.ToList();
                local.buildingName = temp2;
            }
            else
            {
                var temp = (from buildingTable in _db.Buildings select buildingTable.buildingName);
                local.buildingName = temp.ToList();

            }

            return Json(local, JsonRequestBehavior.AllowGet);
        }
        */

        [HttpGet]
        public JsonResult RequestModelUpdater(string park, string building, string roomcode, List<string> facilities, string additional_requirements)
        {
            // data-in sent as an array - therefore iterate to replace '--' (default) with ""
            RequestModel local_return = new RequestModel();

            if (park == "")
            {

                IQueryable<string> park_names = from parkTable in _db.Parks select parkTable.parkName;
                local_return.parkName = park_names.ToList();

            }

            if (building == "" && park == "")
            {

                IQueryable<string> building_names = from buildingTable in _db.Buildings select buildingTable.buildingName;
                local_return.buildingName = building_names.ToList();

            }

            if (roomcode == "" && park == "" && building == "")
            {

                IQueryable<string> room_codes = from roomsTable in _db.Rooms select roomsTable.roomCode;
                local_return.roomCode = room_codes.ToList();

            }

            if (park != "")
            {

                IQueryable<string> park_names = from parkTable in _db.Parks where parkTable.parkName.Contains(park) select parkTable.parkName;
                local_return.parkName = park_names.ToList();

            }

            if (park != "" && building != "")
            {

                Int16 parkID = (Int16)(from parkTable in _db.Parks where parkTable.parkName.Contains(park) select parkTable.parkID).FirstOrDefault();
                IQueryable<string> building_names = from buildingTable in _db.Buildings where (Int16)buildingTable.parkID == parkID && buildingTable.buildingName.Contains(building) select buildingTable.buildingName;
                local_return.buildingName = building_names.ToList();

            }

            if (park != "" && building == "")
            {

                Int16 parkID = (Int16)(from parkTable in _db.Parks where parkTable.parkName.Contains(park) select parkTable.parkID).FirstOrDefault();
                IQueryable<string> building_names = from buildingTable in _db.Buildings where (Int16)buildingTable.parkID == parkID select buildingTable.buildingName;
                local_return.buildingName = building_names.ToList();

            }




            return Json(local_return, JsonRequestBehavior.AllowGet);
        }


	}
}