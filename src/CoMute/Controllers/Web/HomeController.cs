using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoMute.Web.Controllers.Web
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }


        public ActionResult NewCarPool()  // method for car pool
        {
            return View();
        }

        public ActionResult CarPool()  // method for  car pool lists
        {
            return View();
        }
    }
}