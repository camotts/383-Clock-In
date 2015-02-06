using ClockIn_ClockOut.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ClockIn_ClockOut.Controllers
{
    class AuthorizeUserAttribute: AuthorizeAttribute
    {
        private DatabaseContext db = new DatabaseContext();

        // Custom property
        public string AccessLevel { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var isAuthorized = base.AuthorizeCore(httpContext);
            if (!isAuthorized)
            {                
                return false;
            }

            string privilegeLevels = string.Join("", GetUserRights(httpContext.User.Identity.Name.ToString())); 

            if (privilegeLevels.Contains(this.AccessLevel))
            {
                return true;
            }
            else
            {
                return false;
            }            
        }

        /// <summary>
        /// Get the Role for the user
        /// </summary>
        /// <param name="cUser"></param>
        /// <returns></returns>
        public string GetUserRights(string cUser)
        {
            var user = db.Users.FirstOrDefault(u => cUser == u.Username);

            var role = db.Roles.FirstOrDefault(r => user.Role == r.ID);

            return role.Name;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(
                            new
                            {
                                controller = "User",
                                action = "Login"
                            })
                        );
        }
    }

}
