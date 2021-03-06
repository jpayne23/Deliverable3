﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TimetableSys_T17;
using System.Text.RegularExpressions;

//---Clean up code
//---Autofill fields for edit
//--Validation on edit, look at facilityController

namespace TimetableSys_T17.Controllers
{
    public class RoomsController : Controller
    {
        private TimetableDbEntities db = new TimetableDbEntities();

        private string roomCodeToUpper(string roomCode)
        {
            string buildingCode = roomCode.Substring(0, roomCode.IndexOf("."));
            buildingCode = buildingCode.ToUpper();

            string result = buildingCode + roomCode.Substring(1);

            return result;
        }

        private bool checkRoomCode(string roomCode)
        {
            Regex reg = new Regex(@"^[A-Za-z]{1,3}(\.)[0-9](\.)[0-9]{1,3}[a-z]?$");

            return reg.IsMatch(roomCode);
        }//Regular expression check for room code

        private bool validate(string roomCode) //Checks that room with same name hasn't been created before
        {

            bool returnVal = false;

            using (var db = new TimetableDbEntities())
            {
                //Get all roomID where the roomName is the same as the one the user inputted
                var room = from roomDB in db.Rooms where roomDB.roomCode == roomCode select roomDB.roomCode;

                if (room.Count() == 0)
                {
                    //Allows room to be created if there are no existing rooms with same name
                    returnVal = true;
                }
            }

            return returnVal;
        }

        // GET: Rooms
        public ActionResult Index()
        {
            //Gets all rooms that arent private and orders them by building id
            var rooms = db.Rooms.OrderBy(a => a.buildingID).Where(a => a.@private == 0).Include(r => r.Building);
            //var result = rooms.OrderBy(a => a.buildingID);
            return View(rooms.ToList());
        }

        // GET: Rooms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }

            var bID = room.buildingID;

            var facilities = db.Rooms.Where(a => a.roomID == id).Select(a => a.Facilities.Select(c => c.facilityName).ToList()).ToList();

            var name = db.Buildings.Where(s => s.buildingID == bID).Select(s => s.buildingName);

            ViewBag.Fac = facilities;
            ViewBag.count = facilities.First().Count();
            ViewBag.building = name.First();
            ViewBag.Lab = room.lab;

            return View(room);
        }

        // GET: Rooms/Create
        public ActionResult Create()
        {
            var options = db.Buildings.AsEnumerable().Select(s => new
            {
                buildingID = s.buildingID,
                Info = string.Format("{0} - {1}", s.buildingCode, s.buildingName)
            });

            var facilityNames = db.Facilities.ToList();

            ViewBag.facilities = facilityNames;
            ViewBag.buildingID = new SelectList(options, "buildingID", "Info");
            return View();
        }

        // POST: Rooms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "roomCode,buildingID,capacity,Facilities")] Room room, bool Labe, IEnumerable<int> fac)
        {

            room.roomCode = roomCodeToUpper(room.roomCode);

            var bID = room.buildingID;
            var bCode = room.roomCode;
            bCode =  bCode.Substring(0, bCode.IndexOf("."));

            var result = db.Buildings.Where(s => s.buildingCode.Contains(bCode)).Select(s => s.buildingID);

            if (result.First() == bID && checkRoomCode(room.roomCode) && validate(room.roomCode) && ModelState.IsValid)
            {

                room.@private = 0;

                if (Labe)
                {
                    room.lab = 1;
                }
                else
                {
                    room.lab = 0;
                }
                if (fac != null)
                {
                    foreach (var i in fac)
                    {
                        //Gets facility object from db for correct id and adds it to room
                        room.Facilities.Add(db.Facilities.Where(a => a.facilityID == i).First());
                    }
                }
                db.Rooms.Add(room);
                db.SaveChanges();
                return RedirectToAction("Index");
                
            }

            var options = db.Buildings.AsEnumerable().Select(s => new
            {
                buildingID = s.buildingID,
                Info = string.Format("{0} - {1}", s.buildingCode, s.buildingName)
            });
            var facilityNames = db.Facilities.ToList();

            ViewBag.facilities = facilityNames;
            ViewBag.buildingID = new SelectList(options, "buildingID", "Info");

            return View(room);
        }

        // GET: Rooms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }

            var options = db.Buildings.AsEnumerable().Select(s => new
            {
                buildingID = s.buildingID,
                Info = string.Format("{0} - {1}", s.buildingCode, s.buildingName)
            });

            var selected = db.Rooms.Where(a => a.roomID == id).Select(a => a.Facilities.Select(c => c.facilityID)).ToList();
            ViewBag.selectedFac = selected[0];

            var facilityNames = db.Facilities.ToList();
            ViewBag.facilities = facilityNames;

            ViewBag.buildingID = new SelectList(options, "buildingID", "Info",db.Buildings.Where(a => a.buildingID == room.buildingID).Select(a => a.buildingID).First());

            ViewBag.Lab = room.lab;
            ViewBag.@Private = room.@private;

            return View(room);
        }

        // POST: Rooms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken] //Editing everything works, not complete with error checking
        public ActionResult Edit([Bind(Include = "roomID,roomCode,buildingID,capacity")] Room room, bool Labe, bool Priv, IEnumerable<int> fac)
        {
            room.roomCode = roomCodeToUpper(room.roomCode);
            var oldRoomCode = db.Rooms.Where(x => x.roomID == room.roomID).Select(x => x.roomCode).First();
            var newRoomCode = room.roomCode;
            var bCode = newRoomCode.Substring(0, newRoomCode.IndexOf("."));
            var result = db.Buildings.Where(s => s.buildingCode.Contains(bCode)).Select(s => s.buildingID);

            //Get all roomID where the roomName is the same as the one the user inputted
            var roomings = from roomDB in db.Rooms where roomDB.roomCode == newRoomCode select roomDB.roomCode;

            if (result.First() == room.buildingID && (roomings.Count() == 0 || oldRoomCode == newRoomCode) && checkRoomCode(room.roomCode))
            {

                if (Labe)
                {
                    room.lab = 1;
                }
                else
                {
                    room.lab = 0;
                }
                if (Priv)
                {
                    room.@private = 1;
                }
                else
                {
                    room.@private = 0;
                }

                if (ModelState.IsValid)
                {
                    //Updates the room with the correct set of facilities, either remove or add is allowed
                    Room postAttached = db.Rooms.Where(x => x.roomID == room.roomID).First();
                    room.Facilities = postAttached.Facilities;
                    room.Facilities.Clear();
                    if (fac != null)
                    {
                        foreach (int f in fac)
                        {
                            room.Facilities.Add(db.Facilities.Where(x => x.facilityID == f).First());
                        }
                    }
                    //Updates old version of room with edited values, then saves to database
                    db.Entry(postAttached).CurrentValues.SetValues(room);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }

            var selected = db.Rooms.Where(a => a.roomID == room.roomID).Select(a => a.Facilities.Select(c => c.facilityID)).ToList();
            ViewBag.selectedFac = selected[0];

            var facilityNames = db.Facilities.ToList();

            ViewBag.facilities = facilityNames;

            var options = db.Buildings.AsEnumerable().Select(s => new
            {
                buildingID = s.buildingID,
                Info = string.Format("{0} - {1}", s.buildingCode, s.buildingName)
            });

            ViewBag.buildingID = new SelectList(options, "buildingID", "Info");

            return View(room);
        }

        // GET: Rooms/Delete/5
        //DONE
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }

            var bID = room.buildingID;

            var facilities = db.Rooms.Where(a => a.roomID == id).Select(a => a.Facilities.Select(c => c.facilityName).ToList()).ToList();

            var name = db.Buildings.Where(s => s.buildingID == bID).Select(s => s.buildingName);

            ViewBag.Fac = facilities;
            ViewBag.count = facilities.First().Count();
            ViewBag.building = name.First();
            ViewBag.Lab = room.lab;

            return View(room);
        }

        // POST: Rooms/Delete/5 //Deleting facilities works
        //DONE
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Room room = db.Rooms.Find(id);

            //Gets all facilities ID for room
            var fac = db.Rooms.Where(a => a.roomID == id).Select(a => a.Facilities.Select(c => c.facilityID).ToList()).ToList();

            //Loops through every facility a room has and deletes it
            foreach (var i in fac[0])
            {
                var fa = db.Facilities.Where(a => a.facilityID == i).First();
                room.Facilities.Remove(fa);
            }

            db.Rooms.Remove(room);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
