using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TimetableSys_T17.Controllers
{
    public class CreateRoomController : Controller
    {

        private int isLab(bool lab)
        {

            int labValue = 0;

            if (lab)
            {
                labValue = 1;
            }
            else
            {
                labValue = 0;
            }

            return labValue;
        }

        [HttpGet]
        public ActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Index(Models.CreateRoomModel room)
        {
            if (ModelState.IsValid)
            {
                //var db = new TimetableDbEntities();

                //db.Rooms.InsertOnSubmit(room);

                //TimetableDbEntities db = new TimetableDbEntities();

                using (TimetableDbEntities db = new TimetableDbEntities())
                {

                    Room room1 = new Room
                    {
                        roomCode = room.roomCode,
                        capacity = room.capacity,
                        lab = isLab(room.lab),
                        @private = 1
                    };




                    // Add the new object to the Rooms table.
                    db.Rooms.Add(room1);
                    

                    // Submit the change to the database. 

                    db.SaveChanges();
                }
                return View();
            }

            return View();
        }

	}
}

//TO DO: - Add building ID, get substring of roomName then get id from that
//       - Error check, regexp for roomName
//       - Check added room is in building tied to their department
//       - Prevent repeated rooms




//namespace Test1.Controllers
//{
//    public class MoviesController : Controller
//    {
//        private MovieDBContext db = new MovieDBContext();

//        // GET: Movies
//        public ActionResult Index()
//        {
//            return View(db.Movies.ToList());
//        }

//        // GET: Movies/Details/5
//        public ActionResult Details(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Movie movie = db.Movies.Find(id);
//            if (movie == null)
//            {
//                return HttpNotFound();
//            }
//            return View(movie);
//        }

//        // GET: Movies/Create
//        public ActionResult Create()
//        {
//            return View();
//        }

//        // POST: Movies/Create
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Create([Bind(Include = "ID,Title,ReleaseDate,Genre,Price")] Movie movie)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Movies.Add(movie);
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }

//            return View(movie);
//        }

//        // GET: Movies/Edit/5
//        public ActionResult Edit(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Movie movie = db.Movies.Find(id);
//            if (movie == null)
//            {
//                return HttpNotFound();
//            }
//            return View(movie);
//        }

//        // POST: Movies/Edit/5
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit([Bind(Include = "ID,Title,ReleaseDate,Genre,Price")] Movie movie)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Entry(movie).State = EntityState.Modified;
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }
//            return View(movie);
//        }

//        // GET: Movies/Delete/5
//        public ActionResult Delete(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Movie movie = db.Movies.Find(id);
//            if (movie == null)
//            {
//                return HttpNotFound();
//            }
//            return View(movie);
//        }

//        // POST: Movies/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public ActionResult DeleteConfirmed(int id)
//        {
//            Movie movie = db.Movies.Find(id);
//            db.Movies.Remove(movie);
//            db.SaveChanges();
//            return RedirectToAction("Index");
//        }

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                db.Dispose();
//            }
//            base.Dispose(disposing);
//        }
//    }
//}
