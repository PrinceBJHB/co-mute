using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CustomAuth;

namespace CoMute.Web.Controllers.Web
{
    [BasicAuth]
    public class UserController : Controller
    {
        public ActionResult MyProfile()
        {
            Models.UserProfile model = AuthHelper<Models.UserProfile>.userProfile;
            return View(model);
        }

        public ActionResult EditProfile()
        {
            Models.UserProfile model = AuthHelper<Models.UserProfile>.userProfile;
            return View(model);
        }

        [HttpPost]
        public ActionResult EditProfile(Models.UserProfile model)
        {
            return View(model);
        }
    }
}