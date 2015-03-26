using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimetableSys_T17.Models
{
    public class RequestModel

    {
        // All commented code can be done backend
 

        //public int requestID { get; set; }
        //public Nullable<int> userID { get; set; }
        //public List<int> moduleID { get; set; } :- possibly get this from the id of the dropdown box?
        public int userID { get; set; }
        public List<String> moduleName { get; set; }
        public List<String> moduleCode { get; set; }
        public List<String> facilities { get; set; }
        public List<String> sessionType { get; set; }
        public List<String> day { get; set; }
        public Int16 period { get; set; } // At back end, the lists will be unpacked and submitted approriately
        public Int16 sessionLength { get; set; }
        public Int16 semester { get; set; }
        //public Nullable<int> year { get; set; } possibly remove this? Sem 2 != Sem 1 of same academic year
        public Nullable<int> priority { get; set; } // Ask ray?
        //public Nullable<int> adhoc { get; set; } :- Can determine this within the controller -- removed round from model. -- for illustrative purposes.
        public string specialRequirement { get; set; }
        //public Nullable<int> statusID { get; set; } will not need this, as all requests edited or otherwise will be marked as pending (unless no changes made)
        public List<int> week { get; set; }
        // public int[] building { get; set; } ****!! TO PASS TO VIEW? !!****
        public List<int> rooms { get; set; }
        public List<String> roomsDisplay { get; set; }



        //public Nullable<String> errorMessage { get; set; }




    }
}