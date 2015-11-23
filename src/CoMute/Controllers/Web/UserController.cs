using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoMute.Web.Models;

namespace CoMute.Web.Controllers.Web
{
    public class UserController : Controller
    {
        private CarPoolDBContext db = new CarPoolDBContext();

        public ActionResult Profile()
        {
            return View();
        }

        public ActionResult CreateCarPool()
        {
            return View();
        }

        public ActionResult Update()
        {

            ViewBag.Submitted = false;

            var result = new { Success = "0", Message = "Update Failed" };

            // Create the Client
            if (HttpContext.Request.RequestType == "POST")
            {
                ViewBag.Submitted = true;
                // If the request is POST, get the values from the form
                var name = Request.Form["name"];
                var surname = Request.Form["surname"];
                var oldpassword = Request.Form["oldpassword"];
                var newpassword = Request.Form["newpassword"];
                var phone = Request.Form["phone"];
                var email = Request.Form["email"];

                var currentEmail = Session["email"].ToString();
                //get the user with the session variable
                var user = db.Users.Where(b => b.EmailAddress ==currentEmail).FirstOrDefault();

                if (user != null)
                {
                    var password = user.Password;
                    
                    if (password == oldpassword)
                    {
                        user.Password = newpassword;
                        user.Surname = surname;
                        user.Name = name;
                        user.Phone = phone;

                        using (var newdb = new CarPoolDBContext() ){
                            newdb.Entry(user).State = System.Data.Entity.EntityState.Modified;
                            newdb.SaveChanges();
                        }

                        Session["username"] = user.Name;
                        Session["email"] = email;
                        Session["phone"] = user.Phone;
                        Session["surname"] = user.Surname;

                        result = new { Success = "1", Message = "Changes successfully updated" };
                    }
                    else
                    {
                        result = new { Success = "0", Message = "Old password does not match current Password" };
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

        public ActionResult RegisterCarPool()
        {

            ViewBag.Submitted = false;

            var result = new { Success = "0", Message = "Registration Failed" };

            // Create the Client
            if (HttpContext.Request.RequestType == "POST")
            {

                ViewBag.Submitted = true;
                // If the request is POST, get the values from the form
                var cname = Request.Form["cname"];
                var dtime = Request.Form["dtime"];
                var atime = Request.Form["atime"];
                var destination = Request.Form["destination"];
                var origin = Request.Form["origin"];
                var availabled = Request.Form["availabled"];
                var availables = Request.Form["availables"];
                var owner = Request.Form["owner"];
                var notes = Request.Form["notes"];

                //check if the email is unique
                var existingCarPool = db.CarPools.Where(b => b.Name == cname).FirstOrDefault();

                if (existingCarPool == null)
                {

                    var carpool = new CarPool
                    {
                        Name = cname,
                        Departure = dtime,
                        Arrival = atime,
                        Destination = destination,
                        Origin = origin,
                        Days = availabled,
                        Seats = availables,
                        Owner = owner,
                        Notes = notes
                    };
                    
                    db.CarPools.Add(carpool);
                    db.SaveChanges();

                    //put the created carpool in the user created column
                    var email = Session["email"].ToString();

                    var user = db.Users.Where(b => b.EmailAddress == email).FirstOrDefault();

                    var createdCarPools = user.Createdcarpools +","+cname;
                    user.Createdcarpools = createdCarPools;

                    using (var newdb = new CarPoolDBContext())
                    {
                        newdb.Entry(user).State = System.Data.Entity.EntityState.Modified;
                        newdb.SaveChanges();
                    }

                    result = new { Success = "1", Message = "CarPool Registation Successful!" };
                   
                }
                else
                {
                    result = new { Success = "0", Message = "CarPool with the same Name exists. Please enter another name" };
                }
                

                // Create a new Client for these details.
            }

            return Json(result, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Logout()
        {
            Session.Remove("logged");
            Session.Remove("username");
            Session.Remove("email");
            Session.Remove("phone");
            Session.Remove("surname");

            return View("Profile");
        }

    }
}