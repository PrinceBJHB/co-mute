using CoMute.Web.Models;
using CoMute.Web.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CoMute.Web.Controllers.API
{
    public class CarPoolController : ApiController
    {
        [Route("api/carpool/add")]
        public HttpResponseMessage Post(CarPoolRequest carpoolRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (DAL.CoMuteEntities db = new DAL.CoMuteEntities())
                    {
                        //if (result != null && result.UserID > 0)
                        //{
                        //    return Request.CreateResponse(HttpStatusCode.Created, "Successfully registered");
                        //}
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
