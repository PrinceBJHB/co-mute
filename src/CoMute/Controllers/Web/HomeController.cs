﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CustomAuth;

namespace CoMute.Web.Controllers.Web
{
    [BasicAuth]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search()
        {
            return View();
        }

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

        public ActionResult About()
        {
            return View("About", masterName: "~/Views/Shared/_Layout.cshtml");
        }
    }
}