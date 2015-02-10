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

    //[AuthorizeUser(AccessLevel = "Admin")]

    public class TimeEntryController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        [Authorize]
        // GET: TimeEntries
        public ActionResult Index()
        {
            var user = db.Users.FirstOrDefault(u => u.Username == User.Identity.Name);
            if (user.Role == 2)
            {
                return View(db.Users.ToList());
            }
            else
            {
                return RedirectToAction("PunchCard");
            }
        }


        // GET: TimeEntries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TimeEntry timeEntry = db.TimeEntries.Find(id);

            //for the display name, put the full name together
            var user = db.Users.FirstOrDefault(u => u.Username == User.Identity.Name);

            string cFullName = user.FirstName + " " + user.LastName;
            ViewBag.fullName = cFullName;

            if (timeEntry == null)
            {
                return HttpNotFound();
            }

            return View(timeEntry);
        }


        [AuthorizeUser(AccessLevel = "Admin")]
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

        [AuthorizeUser(AccessLevel = "Admin")]
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

        [AuthorizeUser(AccessLevel = "Admin")]
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

        [AuthorizeUser(AccessLevel = "Admin")]
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
        public ActionResult PunchCard(TimeEntry time, String timeDate)
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
                    time.TimeOut = Convert.ToDateTime(timeDate);
                    user.Timed = false;
                    time.timeMinutes = time.TimeOut-time.TimeIn;
                    var test = (time.timeMinutes.TotalMinutes < 01.01);
                    if (time.timeMinutes.TotalMinutes < 01.01)
                    {
                        var toBeDeleted = db.TimeEntries.FirstOrDefault(t => t.ID == time.ID);
                        db.TimeEntries.Remove(toBeDeleted);
                        
                    }
                    else
                    {
                        db.TimeEntries.AddOrUpdate(time);                        
                    }
                }


            }
            else
            {
                time.UserId = user.ID;
                time.TimeIn = Convert.ToDateTime(timeDate);
                user.Timed = true;
                db.TimeEntries.Add(time);
            }

            db.SaveChanges();
            return getPartial();
        }

        
        [AllowAnonymous]
        [HttpGet]
        public ActionResult getPartial()
        {
            var user = db.Users.FirstOrDefault(u => u.Username == User.Identity.Name);
            ViewBag.user = user;

            //grab all the time entries for the user
            List<TimeEntry> TimeEntries = db.TimeEntries.Where(x => x.UserId == user.ID).OrderByDescending(o => o.TimeIn).ToList();

            foreach (var item in TimeEntries)
            {
                item.TimeIn = item.TimeIn.ToLocalTime();
                item.TimeOut = item.TimeOut.ToLocalTime();
            }



            //for the display name, put the full name together
            string cFullName = user.FirstName + " " + user.LastName;
            ViewBag.fullName = cFullName;

            //Is the user an admin??
            bool isAdmin = false;
            if (user.Role == 2)
            {
                isAdmin = true;
            }

            ViewBag.isAdmin = isAdmin;


            return PartialView("TimesTable", TimeEntries);
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult getPartialId(String id)
        {
            int realId = Convert.ToInt32(id);
            var user = db.Users.FirstOrDefault(u => u.ID == realId);
            ViewBag.user = user;

            //grab all the time entries for the user
            List<TimeEntry> TimeEntries = db.TimeEntries.Where(x => x.UserId == user.ID).OrderByDescending(o => o.TimeIn).ToList();

            foreach (var item in TimeEntries)
            {
                item.TimeIn = item.TimeIn.ToLocalTime();
                item.TimeOut = item.TimeOut.ToLocalTime();
            }


            //Is the user an admin??
            ViewBag.isAdmin = true;

            //for the display name, put the full name together
            string cFullName = user.FirstName + " " + user.LastName;
            ViewBag.fullName = cFullName;

            return PartialView("TimesTable", TimeEntries);
        }

        [AllowAnonymous]
        [HttpGet]
        public string getName(String id)
        {
            int realId = Convert.ToInt32(id);
            var user = db.Users.FirstOrDefault(u => u.ID == realId);

            return (user.FirstName + " " + user.LastName);
        }

        [Authorize]
        public String getHours()
        {


            TimeSpan total = DateTime.Now - DateTime.Now;
            var user = db.Users.FirstOrDefault(u => u.Username == User.Identity.Name);
            foreach (var item in db.TimeEntries.Where(x => x.UserId == user.ID).OrderBy(x => x.ID > 0))
            {
                total += item.timeMinutes;
            }

            return total.ToString(@"hh\:mm");
        }

        [Authorize]
        public String getHoursId(String id)
        {
            int realId = Convert.ToInt32(id);
            TimeSpan total = DateTime.Now - DateTime.Now;

            var user = db.Users.FirstOrDefault(u => u.Username == User.Identity.Name);
            foreach (var item in db.TimeEntries.Where(x => x.UserId == user.ID).OrderBy(x => x.ID > 0))
            {
                total += item.timeMinutes;
            }

            return total.ToString(@"hh\:mm");
        }

        [Authorize]
        [HttpGet]
        public bool getClockBool()
        {
            var user = db.Users.FirstOrDefault(u => u.Username == User.Identity.Name);
            return user.Timed;
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
