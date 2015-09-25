using CoMute.Web.Models.Dto;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Linq;

namespace CoMute.Web.Controllers.API
{
    public class AuthenticationController : ApiController
    {
        /// <summary>
        /// Logs a user into the application.
        /// </summary>
        /// <param name="loginRequest">The user's login details</param>
        /// <returns></returns>
        public HttpResponseMessage Post(LoginRequest loginRequest)
        {
            using (DAL.CoMuteEntities db = new DAL.CoMuteEntities())
            {
                DAL.usp_User_Login_Result result = db.usp_User_Login(loginRequest.emailAddress, loginRequest.password).FirstOrDefault();
                if (result != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Forbidden);
                }
            }
        }
    }
}
