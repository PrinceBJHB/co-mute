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
    [BasicAuth]
    public class CarPoolController : ApiController
    {
        [Route("api/carpool/add/{id}")]
        public HttpResponseMessage Get(int? id)
        {
            try
            {
                if (id == null)
                {
                    Models.Dto.CarPoolRequest model = new Models.Dto.CarPoolRequest();
                    model.UserID = AuthHelper<Models.UserProfile>.userProfile.UserID;
                    return Request.CreateResponse(HttpStatusCode.OK, model);
                } 
                else
                {
                    using (DAL.CoMuteEntities db = new DAL.CoMuteEntities())
                    {
                        Models.Dto.CarPoolRequest model = db.CarPools.Where(a => a.CarPoolID == id).FirstOrDefault();
                        return Request.CreateResponse(HttpStatusCode.OK, model);
                    }
                }
            }
            catch
            {
                // Nlog
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest, "Failed to register new user...");
        }

        [Route("api/carpool/create")]
        public HttpResponseMessage Post(Models.Dto.CarPoolRequest carPoolRequest)
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
