using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimetableSys_T17.Models
{
    public class CreateRoomModel
    {
        public string roomName { get; set; }
        public int capacity { get; set; }
        public bool lab { get; set; }

    }
}