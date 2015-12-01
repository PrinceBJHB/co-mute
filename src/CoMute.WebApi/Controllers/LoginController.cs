using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using CoMute.DAL;
using CoMute.Models;

namespace CoMute.WebApi.Controllers
{
    public class LoginController : ApiController
    {
        private UserContext db = new UserContext();

        

        // GET: api/Login/5
        [ResponseType(typeof(bool))]
        public IHttpActionResult PostUser(User user)
        {
            bool isAuthenticated = false;

            User userInfo = db.Users.Where(x => x.Email == user.Email && x.Password == user.Password).SingleOrDefault();

            if (userInfo != null)
            {
               isAuthenticated = true;
            }
            else
            {
                isAuthenticated = false;
            }

            return Ok(isAuthenticated);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(Guid id)
        {
            return db.Users.Count(e => e.UserId == id) > 0;
        }
    }
}