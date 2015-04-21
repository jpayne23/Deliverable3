using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimetableSys_T17.Models;

using System.Diagnostics;

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
               // local_return.roomCode = room_codes.ToList();

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

            if (park != "" && roomcode == "")
            {

                
                // If the user wants to be in a certain park, but does not care for which building, provides list of rooms (in park).

                Int16 parkID = _db.Parks.Where(x => x.parkName == park).Select(x => (Int16)x.parkID).FirstOrDefault();
                //List<int> buildingID = _db.Buildings.Where(a => a.parkID == parkID).Select(a => a.buildingID).ToList();
                IQueryable<string> roomCodes = _db.Rooms.Join(_db.Buildings, a => a.buildingID, d => d.buildingID, (a, d) => new { a.roomCode, d.parkID }).Where(a => a.parkID == parkID).Select(d => d.roomCode);

                local_return.roomCode = roomCodes.ToList();

            }

            if (building != "" && roomcode == "" || park != "" && building != "" && roomcode == "")
            {

                // This query is so awesome, it filters partial inputs, and gets rooms based on that. B-)

                IQueryable<string> roomCodes = _db.Rooms.Join(_db.Buildings, a => a.buildingID, d => d.buildingID, (a, d) => new { a.roomCode, d.buildingName }).Where(a => a.buildingName.Contains(building)).Select(d => d.roomCode);

                local_return.roomCode = roomCodes.ToList();

            }

            if (park != "" && building != "" && roomcode != "")
            {
                // This gets me all ov a do. It's beautiful. Bow to my awesome power!
                // After this implementation I noticed a change in performance, don't know if it's my end or this query. - mindful 
                
                IQueryable<string> roomCodes = _db.Rooms.Join(_db.Buildings, a => a.buildingID, d => d.buildingID, (a, d) => new { a.roomCode, d.buildingName, d.parkID })
                    .Where(a => a.buildingName.Contains(building)).Join(_db.Parks, a => a.parkID, b => b.parkID, (a, b) => new { a.roomCode, b.parkName }).Where(a => a.parkName.Contains(park)).Select(d => d.roomCode);

                local_return.roomCode = roomCodes.ToList();

            }

            return Json(local_return, JsonRequestBehavior.AllowGet);
        }


	}
}