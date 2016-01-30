using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CoMute.Web.Models.Dto;

namespace CoMute.Web.Controllers.API
{
    public class CarPoolController : ApiController
    {
        /// <summary>
        /// Joins a user into a car-pool
        /// </summary>
        /// <param name="joinCarPoolRequest">The user's email address and id of car-pool to joine</param>
        /// <returns></returns>
        public HttpResponseMessage Post(JoinCarPoolRequest joinCarPoolRequest)
        {
            HttpResponseMessage response;

            if (canUserJoinCarPool(joinCarPoolRequest))
            {
                response = Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.NotAcceptable);
            }
            return response;
        }

        private Boolean canUserJoinCarPool(JoinCarPoolRequest request)
        {
            return request.CarPoolId.Equals("nopeville");
        }
    }
}
