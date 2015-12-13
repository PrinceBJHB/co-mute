using CoMute.Domain.Inteface;
using CoMute.Domain.Repo;
using CoMute.Web.Models.Dto;
using System;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CoMute.Web.Controllers.API
{
    public class AuthenticationController : ApiController
    {
        IUserRepo UserRepo;

        public AuthenticationController()
        {
            UserRepo = new UserRepo();
        }

        /// <summary>
        /// Logs a user into the application.
        /// </summary>
        /// <param name="loginRequest">The user's login details</param>
        /// <returns></returns>
        public HttpResponseMessage Post(LoginRequest loginRequest)
        {
            try
            {
                var user = UserRepo.Login(loginRequest.Email, loginRequest.Password);

                if (user != null)
                {
                    HttpContext.Current.Session["CoMute_User"] = user.Id;
                    HttpContext.Current.Session["CoMute_LogedIn"] = true;

                    return Request.CreateResponse(HttpStatusCode.Accepted);
                }

                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception)
            {
                //log exception
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Error occurred. Please contact someone.");
            }
        }
    }
}
