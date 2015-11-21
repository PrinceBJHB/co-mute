using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoMute.Web.Models;

namespace CoMute.Web.Controllers.Web
{
    public class HomeController : Controller
    {
        private CarPoolDBContext db = new CarPoolDBContext();

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

        public ActionResult RegisterUser()
        {
      
            ViewBag.Submitted = false;

            var result = new { Success = "0", Message = "Registration Failed" };

            // Create the Client
            if (HttpContext.Request.RequestType == "POST")
            {
                ViewBag.Submitted = true;
                // If the request is POST, get the values from the form
                var name = Request.Form["name"];
                var surname = Request.Form["surname"];
                var password = Request.Form["password"];
                var phone = Request.Form["phone"];
                var email = Request.Form["email"];

                //check if the email is unique
                var existingUser = db.Users.Where(b => b.EmailAddress == email).FirstOrDefault();

                if (existingUser == null)
                {
                    var user = new User { Name = name, Surname = surname, Password = password, Phone = phone, EmailAddress = email };
                    try
                    {
                        db.Users.Add(user);
                        db.SaveChanges();

                        result = new { Success = "1", Message = "Registation Successful. Welcome " + name + " to CarPool!!" };
                    }
                    catch (Exception ex)
                    {

                    }
                }
                else
                {
                    result = new { Success = "0", Message = "User with the same Email exists. Please enter another email" };
                }
                // Create a new Client for these details.
            }

            return Json(result, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Login()
        {

            ViewBag.Submitted = false;

            var result = new { Success = "0", Message = "Login Failed" };

            // Create the Client
            if (HttpContext.Request.RequestType == "POST")
            {
                ViewBag.Submitted = true;
                // If the request is POST, get the values from the form
               
                var password = Request.Form["password"];
                var email = Request.Form["email"];

                
                var user = db.Users.Where(b => b.EmailAddress == email).FirstOrDefault();
                var pwd = user.Password;

                if (user != null && pwd == password)
                {
                    result = new { Success = "1", Message = "Login Successful "};

                    var surnames = user.Surname;

                    Session["logged"] = true;
                    Session["username"] = user.Name;
                    Session["email"] = email;
                    Session["phone"] = user.Phone;
                    Session["surname"] = user.Surname;

                }
                else if (user == null)
                {
                    result = new { Success = "0", Message = "Username does not exist" };
                }
                else
                {
                    result = new { Success = "0", Message = "Please enter the correct password and username" };
                }
  
                // Create a new Client for these details.
            }

            return Json(result, JsonRequestBehavior.AllowGet);

        }
    }
}