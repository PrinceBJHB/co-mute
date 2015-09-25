using CoMute.Web.Models;
using CoMute.Web.Models.Dto;
using CustomAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CoMute.Web.Controllers.API
{
    public class UserController : ApiController
    {
        [Route("api/user/add")]
        public HttpResponseMessage Post(RegistrationRequest registrationRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (DAL.CoMuteEntities db = new DAL.CoMuteEntities())
                    {
                        DAL.usp_User_Register_Result result = db.usp_User_Register(
                            registrationRequest.firstName.FormalFormat(),
                            registrationRequest.lastName.FormalFormat(),
                            registrationRequest.emailAddress.ToLower(),
                            registrationRequest.phoneNumber,
                            registrationRequest.password).FirstOrDefault();

                        if (result != null && result.UserID > 0)
                        {
                            return Request.CreateResponse(HttpStatusCode.Created, "Successfully registered");
                        }
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Ambiguous, "Invalid details proivided");
                }
            }
            catch
            {
                // Nlog
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest, "Failed to register new user...");
        }
    }
}
