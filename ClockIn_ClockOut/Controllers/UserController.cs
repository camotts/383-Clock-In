using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClockIn_ClockOut.Models;
using System.Web.Security;
using System.Web.Helpers;


namespace ClockIn_ClockOut.Controllers
{
    [AuthorizeUser(AccessLevel = "Admin")]
    public class UserController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Users
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Username,Password,FirstName,LastName,Role")] User user)
        {
            if (ModelState.IsValid)
            {
                if(db.Users.Any(u=> u.Username == user.Username)){
                    ViewBag.Duplicate="Username is not available";
                    ViewBag.RoleId = new SelectList(db.Roles, "RoleId", "RoleName");
                    return View(user);
                }
                user.Password = Crypto.HashPassword(user.Password);
                user.Timed = false;
                user.Role = 1;
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            
            }
            else
            {
                ModelState.AddModelError("", "Username already Exists");
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Username,Password,FirstName,LastName,Role")] User user)
        {
            if (ModelState.IsValid)
            {
                var entry = db.Entry(user);
                entry.Property(u => u.Password).IsModified = false;

                user.Password = Crypto.HashPassword(user.Password);
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        //Login Controller
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(User userLogingIn)
        {
           //This is where to put the Auth cookie and validate the user
            if (ModelState.IsValid)
            {
                User verification = db.Users.FirstOrDefault(u => u.Username == userLogingIn.Username);
                Boolean isPasswordVerified=false;
                if (verification != null){
                    if (userLogingIn.Password != null && userLogingIn.Password != "")
                    isPasswordVerified=(verification.Password!=null && Crypto.VerifyHashedPassword(verification.Password,userLogingIn.Password)==true);
                }
                if(isPasswordVerified==true){
                    FormsAuthentication.SetAuthCookie(userLogingIn.Username, false);
                    return RedirectToAction("PunchCard", "TimeEntry");
                }
                else
                {
                    ModelState.AddModelError("", "Login data is incorrect!");
                }
            }
            return View(userLogingIn);
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Logout()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "User");
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        
        [AllowAnonymous]
        public bool isAdmin()
        {
            if (User.Identity.IsAuthenticated)
            {
                string userForRoleCheck = User.Identity.Name;
                if (userForRoleCheck != "" && userForRoleCheck != null)
                {
                    var grabUser = db.Users.FirstOrDefault(x => userForRoleCheck == x.Username);

                    if (grabUser.Role == 2)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        [AllowAnonymous]
        public int getCount()
        {
            var count = db.Users.Where(u => u.ID > 0).Count();

            return count;
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
