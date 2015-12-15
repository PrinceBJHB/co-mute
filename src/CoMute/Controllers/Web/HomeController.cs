using CoMute.Domain.Inteface;
using CoMute.Domain.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoMute.Web.Controllers.Web
{
    [Authorize]
    public class HomeController : Controller
    {
        IUserRepo UserRepo;

        public HomeController()
        {
            UserRepo = new UserRepo();
        }

        public ActionResult Index()
        {
            UserRepo.GetJoinedCarpools(User.Identity.Name);    
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult RegisterCarpool()
        {
            return View();
        }
    }
}