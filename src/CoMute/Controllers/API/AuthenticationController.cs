using CoMute.Web.Models.Dto;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Linq;
using CustomAuth;

namespace CoMute.Web.Controllers.API
{
    public class AuthenticationController : ApiController
    {
        /// <summary>
        /// Logs a user into the application.
        /// </summary>
        /// <param name="loginRequest">The user's login details</param>
        /// <returns></returns>
        [Route("api/auth/login")]
        public HttpResponseMessage Post(LoginRequest loginRequest)
        {
            if (AuthHelper<Models.UserProfile>.LoginUser(loginRequest.emailAddress, loginRequest.password, false))
            {
                return Request.CreateResponse(HttpStatusCode.OK, AuthHelper<Models.UserProfile>.userProfile);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden, "Invalid email address or password");
            }
        }

        [Route("api/auth/logout")]
        public HttpResponseMessage Get(LoginRequest loginRequest)
        {
            if (RequestContext.Principal.Identity.IsAuthenticated)
            {
                AuthHelper<Models.UserProfile>.LogoutUser();
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
