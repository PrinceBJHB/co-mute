using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CustomAuth;

namespace CoMute.Web.Controllers.Web
{
    public class AuthController : Controller, CustomAuth.IBasicAuthController<Models.User, Models.Dto.LoginRequest>
    {
        public ActionResult Login(string ReturnUrl)
        {
            ViewBag.redirect = ReturnUrl;
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult About()
        {
            return View("About", masterName:"~/Views/Shared/_LoginLayout.cshtml" );
        }

        [HttpPost]
        public ActionResult Login(Models.Dto.LoginRequest model, string ReturnUrl)
        {
            if (AuthHelper<Models.User>.LoginUser(model.emailAddress, model.password, false))
            {
                if (!string.IsNullOrEmpty(ReturnUrl) && ReturnUrl.Length > 1)
                {
                    return Redirect(ReturnUrl);
                }
                else
                {
                    return RedirectToAction("/Home/Index");
                }
            }
            else
            {
                ViewBag.error = "Login Failed, Invalid username or password";
                return View();
            }
        }

        public ActionResult Logout()
        {
            if (ControllerContext.HttpContext.User.Identity.IsAuthenticated)
            {
                AuthHelper<Models.User>.LogoutUser();
                ViewBag.error = "You have been Logged out";
            }
            return View("Login");
        }
    }
}