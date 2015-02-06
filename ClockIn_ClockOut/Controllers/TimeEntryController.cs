using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClockIn_ClockOut.Models;
using System.Data.Entity.Migrations;

namespace ClockIn_ClockOut.Controllers
{
    [AuthorizeUser(AccessLevel = "Admin")]
    public class TimeEntryController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: TimeEntries
        public ActionResult Index()
        {
            return View(db.TimeEntries.ToList());
        }

        // GET: TimeEntries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TimeEntry timeEntry = db.TimeEntries.Find(id);

            if (timeEntry == null)
            {
                return HttpNotFound();
            }

            return View(timeEntry);
        }

        // GET: TimeEntries/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TimeEntries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,UserId,TimeIn,TimeOut")] TimeEntry timeEntry)
        {
            if (ModelState.IsValid)
            {
                db.TimeEntries.Add(timeEntry);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(timeEntry);
        }

        // GET: TimeEntries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeEntry timeEntry = db.TimeEntries.Find(id);
            if (timeEntry == null)
            {
                return HttpNotFound();
            }
            return View(timeEntry);
        }

        // POST: TimeEntries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserId,TimeIn,TimeOut")] TimeEntry timeEntry)
        {
            if (ModelState.IsValid)
            {
                db.Entry(timeEntry).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(timeEntry);
        }

        // GET: TimeEntries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeEntry timeEntry = db.TimeEntries.Find(id);
            if (timeEntry == null)
            {
                return HttpNotFound();
            }
            return View(timeEntry);
        }

        // POST: TimeEntries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TimeEntry timeEntry = db.TimeEntries.Find(id);
            db.TimeEntries.Remove(timeEntry);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpGet]
        public ActionResult PunchCard()
        {
            //Grab the user
            var user = db.Users.FirstOrDefault(u => u.Username == User.Identity.Name);
            ViewBag.user = user;

            //grab all the time entries for the user
            List<TimeEntry> TimeEntries = db.TimeEntries.Where(x => x.UserId == user.ID).OrderByDescending(o => o.TimeIn).ToList();
            ViewBag.TimeEntries = TimeEntries;

            //for the display name, put the full name together
            string cFullName = user.FirstName + " " + user.LastName;
            ViewBag.fullName = cFullName;

            return View(TimeEntries);
        }

        [Authorize]
        [HttpPost]
        public ActionResult PunchCard(TimeEntry time)
        {

            var user = db.Users.FirstOrDefault(u => u.Username == User.Identity.Name);

            if (user.Timed)
            {
                List<TimeEntry> list = db.TimeEntries.Where(l => l.UserId == user.ID).ToList();

                var times = list.Last();

                if (times.UserId == user.ID)
                {
                    time.ID = times.ID;
                    time.TimeIn = times.TimeIn;
                    time.UserId = times.UserId;
                    time.TimeOut = DateTime.Now;
                    user.Timed = false;
                    db.TimeEntries.AddOrUpdate(time);
                }


            }
            else
            {
                time.UserId = user.ID;
                time.TimeIn = DateTime.Now;
                user.Timed = true;
                db.TimeEntries.Add(time);
            }

            db.SaveChanges();
            return RedirectToAction("PunchCard", "TimeEntry");
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
