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

            return View();
        }



        private List<string> UniqFacilities(List<List<string>> input) {

            List<string> placeholder = new List<string>();

            foreach(var i in input)
            {
                foreach(var y in i)
                {

                    if (!(placeholder.Contains(y))) {

                        placeholder.Add(y);

                    }
                }
            }

            return placeholder;


        }

        
        [HttpGet]
        public JsonResult RequestModelUpdaterOptional(string park, string building, string roomcode, List<string> facilities, string additional_requirements)
        {
            // data-in sent as an array - therefore iterate to replace '--' (default) with "" == idea :-)
            // Facilities:- drop down box or list view of all available facilities based on their search. 
            // Or, if they start with facilities, i.e.  facilities.count != 0, then we will filter rooms, buildings and park.
            
            RequestModel local_return = new RequestModel();


            /*
             * 
             * MY THOUGHT PROCESS FOR NOT DOING AN ELSE STATEMENT AND RETURNING ALL DATA IF NO FIELDS HAVE INPUT
             * IS BECAUSE THERE'LL BE A HUGE AMOUNT OF DATA SENT TO CLIENT, THEREFORE, AFFECTING PERFORMANCE,
             * THEREFORE, I HAVE CATERED FOR ALL EVENTUALITIES IN THE HOPE THAT THE RESPONSE TIME IS SNAPPY.
             * 
             */
            
            if (park != "" && building == "" && roomcode == "")
            {

                Int16 parkID = (Int16)(_db.Parks.Where(x => x.parkName.Contains(park)).Select(x => x.parkID).FirstOrDefault());
                IQueryable<string> park_names = _db.Parks.Where(x => x.parkName.Contains(park)).Select(x => x.parkName);
                IQueryable<string> building_names = _db.Buildings.Where(x => (Int16)x.parkID == parkID).Select(x => x.buildingName);
                IQueryable<string> roomCodes = _db.Rooms.Join(_db.Buildings, a => a.buildingID, d => d.buildingID, (a, d) => new { a.roomCode, d.parkID }).Where(a => a.parkID == parkID).Select(d => d.roomCode);
                List<List<string>> available_facilities = _db.Parks.Join(_db.Buildings, a => a.parkID, d => d.parkID, (a, d) => new { a.parkName, d.buildingID }).Where(a => a.parkName.Contains(park))
                    .Join(_db.Rooms, a => a.buildingID, d => d.buildingID, (a, d) => new { d.Facilities }).Select(a => a.Facilities.Select(c => c.facilityName).ToList()).ToList();

                local_return.parkName = park_names.ToList();
                local_return.buildingName = building_names.ToList();
                local_return.roomCode = roomCodes.ToList();
                local_return.facilities = UniqFacilities(available_facilities);

            }
            else if (building != "" && roomcode == "" || park == "" && building != "")
            {

                List<string> placeholder = new List<string>();
                string park_name = _db.Parks.Join(_db.Buildings, a => a.parkID, d => d.parkID, (a, d) => new { a.parkName, d.buildingName }).Where(a => a.buildingName.Contains(building)).Select(d => d.parkName).FirstOrDefault();
                IQueryable<string> roomCodes = _db.Rooms.Join(_db.Buildings, a => a.buildingID, d => d.buildingID, (a, d) => new { a.roomCode, d.buildingName }).Where(a => a.buildingName.Contains(building)).Select(d => d.roomCode);
                List<List<string>> available_facilities = _db.Rooms.Join(_db.Buildings, a => a.buildingID, d => d.buildingID, (a, d) => new { a.Facilities, d.buildingName })
                    .Where(a => a.buildingName.Contains(building)).Select(d => d.Facilities.Select(a => a.facilityName).ToList()).ToList();
                
                placeholder.Add(park_name);

                local_return.parkName = placeholder;
                local_return.roomCode = roomCodes.ToList();
                local_return.facilities = UniqFacilities(available_facilities);

            }
            else if (park != "" && building != "" && roomcode != "")
            {
                // This gets me all ov a do. It's beautiful. Bow to my awesome power!
                // After this implementation I noticed a change in performance, don't know if it's my end or this query. - mindful 
                
                IQueryable<string> roomCodes = _db.Rooms.Join(_db.Buildings, a => a.buildingID, d => d.buildingID, (a, d) => new { a.roomCode, d.buildingName, d.parkID })
                    .Where(a => a.buildingName.Contains(building)).Join(_db.Parks, a => a.parkID, b => b.parkID, (a, b) => new { a.roomCode, b.parkName }).Where(a => a.parkName.Contains(park)).Select(d => d.roomCode);

                local_return.roomCode = roomCodes.ToList();

            }
            else if (park == "" && building == "" && roomcode != "" || park != "" && building == "" && roomcode != "")
            {

                List<string> placeholder = new List<string>();
                List<string> placeholder_ii = new List<string>();
                var return_data = _db.Parks.Join(_db.Buildings, a => a.parkID, d => d.parkID, (a, d) => new { a.parkName, d.buildingName, d.buildingID })
                    .Join(_db.Rooms, a => a.buildingID, d => d.buildingID, (a, d) => new { a.parkName, a.buildingName, d.roomCode }).Where(a => a.roomCode == roomcode).Select(a => new { a.parkName, a.buildingName }).FirstOrDefault();
                List<List<string>> available_facilities = _db.Rooms.Where(a => a.roomCode.Contains(roomcode)).Select(a => a.Facilities.Select(d => d.facilityName).ToList()).ToList();


                placeholder.Add(return_data.parkName) ;
                placeholder_ii.Add(return_data.buildingName);

                local_return.parkName = placeholder;
                local_return.buildingName = placeholder_ii;
                local_return.facilities = UniqFacilities(available_facilities);

            }
            else
            {
                
                IQueryable<string> park_names = _db.Parks.Select(x => x.parkName);
                IQueryable<string> building_names = _db.Buildings.Select(x => x.buildingName);
                IQueryable<string> room_codes = _db.Rooms.Select(x => x.roomCode);
                IQueryable<string> available_facilities = _db.Facilities.Select(x => x.facilityName);

                local_return.buildingName = building_names.ToList();
                local_return.parkName = park_names.ToList();
                local_return.roomCode = room_codes.ToList();
                local_return.facilities = available_facilities.ToList();
   
            }

            return Json(local_return, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult RequestModelUpdaterCompulsory(string module_code, string module_title, string room_type)
        {

            RequestModel local_return = new RequestModel();

            if (module_code == "" && module_title == "")
            {

                IQueryable<string> module_codes = _db.Modules.Select(x => x.modCode);
                IQueryable<string> module_titles = _db.Modules.Select(x => x.modTitle);
                local_return.moduleCode = module_codes.ToList();
                local_return.moduleTitle = module_titles.ToList();

            }
            else if (module_code != "" && module_title == "")
            {

                IQueryable<string> module_titles = _db.Modules.Where(x => x.modCode == module_code).Select(x => x.modTitle);
                             
                local_return.moduleTitle = module_titles.ToList();

            }
            else if (module_code == "" && module_title != "")
            {

                IQueryable<string> module_codes = _db.Modules.Where(x => x.modTitle == module_title).Select(x => x.modCode);

                local_return.moduleCode = module_codes.ToList();
            }

            return Json(local_return, JsonRequestBehavior.AllowGet);
        }


	}
}