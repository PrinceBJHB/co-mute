using CoMute.Web.Controllers.API;
using CoMute.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace CoMute.Web.Controllers.Web
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (StaticValues.StaticValues.CurrentUser == null) return RedirectToAction("Login", "Home"); 
            else return RedirectToAction("Profile", "Profile");
        }

		[AllowAnonymous]
		public ActionResult Login()
		{
			LoginRequest model = new LoginRequest
			{
				Email = "",
				Password = ""
			};
			return View(model);
		}

		[AllowAnonymous]
		[HttpPost]
		public ActionResult Login(LoginRequest model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var result = new UserController().Login(model.Email, model.Password);
			if (result.StatusCode == HttpStatusCode.OK)
			{
				ObjectContent objContent = result.Content as ObjectContent;
				User user = objContent.Value as User;
                StaticValues.StaticValues.CurrentUser = user;
				return RedirectToAction("Profile", "Profile");
			}
			else
			{
				ObjectContent objContent = result.Content as ObjectContent;
				ModelState.AddModelError("", objContent.Value.ToString());
				return View(model);
			}
		}

		[AllowAnonymous]
        public ActionResult About()
        {
            return View();
        }


		[AllowAnonymous]
		public ActionResult Register()
		{
			User model = new User
			{
				Name = "",
				Surname = "",
				Phone = "",
				EmailAddress = "",
				Password = "",
				ConfirmPassword = ""
			};

			return View(model);
		}

		[AllowAnonymous]
		[HttpPost]
		public ActionResult Register(User model)
        {
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var result = new UserController().Register(model);
			if (result.StatusCode == HttpStatusCode.OK)
			{
				return RedirectToAction("Login", "Home");
			}
			else
			{
				ObjectContent objContent = result.Content as ObjectContent;
				ModelState.AddModelError("EmailAddress", objContent.Value.ToString());
				return View(model);
			}
        }

		public ActionResult Logout()
		{
			StaticValues.StaticValues.CurrentUser = null;
			return RedirectToAction("Login", "Home");
		}
    }
}