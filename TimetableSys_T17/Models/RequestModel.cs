using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimetableSys_T17.Models
{

    public class temp
    {

         //y.dayID, y.periodID, y.sessionLength, y.semester, y.week}).ToList()).ToList();

        public int? dayID;
        public int? periodID;
        public int? sessionLength;
        public int? semester;
        public string week;

    }

    public class RequestModel
    {

        public List<string> parkName { get; set; }
        public List<string> buildingName { get; set; }
        public List<string> roomCode { get; set; }
        public List<string> facilities { get; set; }
        public List<string> moduleCode { get; set; }
        public List<string> moduleTitle { get; set; }
        public List<string> sessionType { get; set; }
        public List<List<temp>> test { get; set; }

    }
    
    
}