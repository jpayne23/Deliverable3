using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TimetableSys_T17;

namespace TimetableSys_T17.Controllers
{
    public class RoomsController : Controller
    {
        private TimetableDbEntities db = new TimetableDbEntities();

        // GET: Rooms
        public ActionResult Index()
        {
            var rooms = db.Rooms.Include(r => r.Building);
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

            ViewBag.Lab = room.lab;

            return View(room);
        }

        // GET: Rooms/Create
        public ActionResult Create()
        {
            ViewBag.buildingID = new SelectList(db.Buildings, "buildingID", "buildingName");
            return View();
        }

        // POST: Rooms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "roomCode,buildingID,capacity")] Room room, bool Labe)
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

            if (ModelState.IsValid)
            {
                db.Rooms.Add(room);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var info = db.Buildings.Select(s => new 
                {
                    BuildingID = s.buildingID,
                    BuildingInfo = string.Format("{0} -- {1}", s.buildingName, s.buildingCode)
                });

                
                

            ViewBag.BuildingInfo = new SelectList(info, "BuildingID", "BuildingInfo");


            ViewBag.buildingID = new SelectList(db.Buildings, "buildingID", "buildingName", room.buildingID);
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
            ViewBag.buildingID = new SelectList(db.Buildings, "buildingID", "buildingName", room.buildingID);
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
            ViewBag.buildingID = new SelectList(db.Buildings, "buildingID", "buildingName", room.buildingID);
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
            return View(room);
        }

        // POST: Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Room room = db.Rooms.Find(id);
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
