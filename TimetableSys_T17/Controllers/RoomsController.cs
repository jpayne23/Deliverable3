using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TimetableSys_T17;

//---Clean up code
//---Autofill fields for edit
//--Validation on edit, look at facilityController

namespace TimetableSys_T17.Controllers
{
    public class RoomsController : Controller
    {
        private TimetableDbEntities db = new TimetableDbEntities();

        // GET: Rooms
        public ActionResult Index()
        {
            var rooms = db.Rooms.Where(a => a.@private == 0).Include(r => r.Building);
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

            //var fac = db.Rooms.Where(a => a.roomID == id).Select(a => a.Facilities.Select(c => c.facilityID).ToList()).ToList();

            //foreach (var i in fac[0])
            //{
            //    Debug.WriteLine(i);
            //}

            //Debug.WriteLine(facilities[0].Count());

            var name = db.Buildings.Where(s => s.buildingID == bID).Select(s => s.buildingName);

            ViewBag.Fac = facilities;
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
            //Change to multiselect
            //var facility = db.Facilities.AsEnumerable().Select(s => new
            //{
            //    facilityID = s.facilityID,
            //    Fac = string.Format("{0}", s.facilityName)
            //});

            var facilityNames = db.Facilities.Select(x => x.facilityName).ToList();

            ViewBag.facilities = facilityNames;
            ViewBag.buildingID = new SelectList(options, "buildingID", "Info");
            return View();
        }

        // POST: Rooms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "roomCode,buildingID,capacity")] Room room, bool Labe, List<int> facilities)
        {

            var bID = room.buildingID;
            var bCode = room.roomCode;
            bCode =  bCode.Substring(0, bCode.IndexOf("."));

            //Debug.WriteLine("Joe, we're here: " + bCode + " ... ");

            var result = db.Buildings.Where(s => s.buildingCode.Contains(bCode)).Select(s => s.buildingID);

            foreach (var i in facilities)
            {
                //var fa = db.Facilities.Where(a => a.facilityID == i).First();
                Debug.WriteLine(i);
            }

            if (result.First() == bID)
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

                //Gets facility object from db for correct id
                //Will be a loop when working
                var facility = db.Facilities.Where(a => a.facilityID == 2).First();
                //Adds facility to room
                room.Facilities.Add(facility);
                //Debug.WriteLine(facility.facilityName + " - " + facility.facilityID);

                if (ModelState.IsValid)
                {
                    db.Rooms.Add(room);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            var options = db.Buildings.AsEnumerable().Select(s => new
            {
                buildingID = s.buildingID,
                Info = string.Format("{0} - {1}", s.buildingCode, s.buildingName)
            });

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

            //Change to a multiselect, probably do client side
            var facility = db.Facilities.AsEnumerable().Select(s => new
            {
                facilityID = s.facilityID,
                Fac = string.Format("{0}", s.facilityName)
            });

            ViewBag.facilityID = new SelectList(facility, "facilityID", "Fac");
            ViewBag.buildingID = new SelectList(options, "buildingID", "Info");

            ViewBag.Lab = room.lab;
            ViewBag.@Private = room.@private;

            return View(room);
        }

        // POST: Rooms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "roomID,roomCode,buildingID,capacity")] Room room, bool Labe, bool Priv)
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
                db.Entry(room).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var options = db.Buildings.AsEnumerable().Select(s => new
            {
                buildingID = s.buildingID,
                Info = string.Format("{0} - {1}", s.buildingCode, s.buildingName)
            });

            ViewBag.buildingID = new SelectList(options, "buildingID", "Info");

            return View(room);
        }

        // GET: Rooms/Delete/5
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
            ViewBag.building = name.First();
            ViewBag.Lab = room.lab;

            return View(room);
        }

        // POST: Rooms/Delete/5 //Deleting facilities works
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
                Debug.WriteLine(fa.facilityName);
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
