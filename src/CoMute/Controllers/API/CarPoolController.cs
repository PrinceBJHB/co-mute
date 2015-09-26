using CoMute.Web.Models;
using CoMute.Web.Models.Dto;
using CustomAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Linq.Dynamic;

namespace CoMute.Web.Controllers.API
{
    [BasicAuth]
    public class CarPoolController : ApiController
    {
        private static int UserID
        {
            get { return AuthHelper<Models.UserProfile>.userProfile.UserID; }
        }

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
        [HttpPost]
        public HttpResponseMessage Create(Models.Dto.CarPoolRequest carPoolRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (DAL.CoMuteEntities db = new DAL.CoMuteEntities())
                    {
                        DAL.usp_CarPool_Create_Result result = db.usp_CarPool_Create(
                            carPoolRequest.UserID,
                            carPoolRequest.departureTime,
                            carPoolRequest.expectedArrivalTime,
                            carPoolRequest.origin,
                            carPoolRequest.destination,
                            carPoolRequest.seatsAvailable,
                            carPoolRequest.notes,
                            string.Join(",", carPoolRequest.daysAvaiable.Select(a => (int)a))).FirstOrDefault();

                        if (result != null && result.Success == true)
                        {
                            return Request.CreateResponse(HttpStatusCode.Created, "Successfully created carpool");
                        }
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Ambiguous, "Invalid details provided");
                }
            }
            catch
            {
                // Nlog
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest, "Failed to create carpool...");
        }

        [Route("api/carpool/getmine")]
        public HttpResponseMessage Get()
        {
            if (ModelState.IsValid)
            {
                using (DAL.CoMuteEntities db = new DAL.CoMuteEntities())
                {
                    var model = db.UserCarPools
                        .Where(a => a.UserID == UserID)
                        .Select(a => new Models.Dto.CarPool()
                        {
                            CarPoolID = a.CarPool.CarPoolID,
                            UserID = a.CarPool.UserID,
                            departureTime = a.CarPool.departureTime,
                            expectedArrivalTime = a.CarPool.expectedArrivalTime,
                            origin = a.CarPool.origin,
                            destination = a.CarPool.destination,
                            seatsAvailable = a.CarPool.seatsAvailable - a.CarPool.UserCarPools.Count,
                            daysAvaiable = a.CarPool.CarPoolDays.Select(b => b.DaysOfWeek.nameOfDay),
                            UserCarPoolID = a.UserCarPoolID
                        }).ToList();

                    return Request.CreateResponse(HttpStatusCode.OK, model);
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid request parameters");
            }
        }

        [Route("api/carpool/get")]
        public HttpResponseMessage Get([FromUri] Models.Dto.CarPoolListRequest request)
        {
            if (ModelState.IsValid)
            {
                using (DAL.CoMuteEntities db = new DAL.CoMuteEntities())
                {
                    string search = request.search ?? "";

                    var model = db.CarPools
                        .Where(a => search != "" ? a.origin.Contains(request.search) : 1 == 1)
                        .Where(a => !a.UserCarPools.Select(b => b.UserID).Contains(UserID))
                        .OrderBy(request.sidx + " " + request.sord)
                        .Take(request.rows)
                        .Skip(request.rows * (request.page - 1))
                        .Select(a => new Models.Dto.CarPool()
                    {
                        CarPoolID = a.CarPoolID,
                        UserID = a.UserID,
                        departureTime = a.departureTime,
                        expectedArrivalTime = a.expectedArrivalTime,
                        origin = a.origin,
                        destination = a.destination,
                        seatsAvailable = a.seatsAvailable - a.UserCarPools.Count,
                        daysAvaiable = a.CarPoolDays.Select(b => b.DaysOfWeek.nameOfDay)
                    }).ToList();

                    Models.Dto.CarPoolListResponse data = new CarPoolListResponse()
                    {
                        data = model,
                        total = 1,
                        page = request.page,
                        records = 1
                    };

                    return Request.CreateResponse(HttpStatusCode.OK, data);
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid request parameters");
            }
        }

        [Route("api/carpool/delete/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            return Request.CreateResponse(HttpStatusCode.BadRequest, "Not yet implemented");
        }

        [Route("api/carpool/update")]
        [HttpPost]
        public HttpResponseMessage Update(Models.Dto.CarPoolRequest carPoolRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (DAL.CoMuteEntities db = new DAL.CoMuteEntities())
                    {
                        DAL.CarPool carpool = db.CarPools.Where(a => a.CarPoolID == carPoolRequest.CarPoolID).FirstOrDefault();

                        if (carpool != null && carpool.UserID == AuthHelper<Models.UserProfile>.userProfile.UserID)
                        {
                            carpool.departureTime = carPoolRequest.departureTime;
                            carpool.expectedArrivalTime = carPoolRequest.expectedArrivalTime;
                            carpool.origin = carPoolRequest.origin;
                            carpool.destination = carPoolRequest.destination;
                            carpool.seatsAvailable = carPoolRequest.seatsAvailable;
                            carpool.notes = carPoolRequest.notes;

                            var remove = carpool.CarPoolDays.Where(a => !carPoolRequest.daysAvaiable.Contains((DayOfWeek)a.DayOfWeekID));
                            db.CarPoolDays.RemoveRange(remove);

                            var add = carPoolRequest.daysAvaiable.Where(a => !carpool.CarPoolDays.Select(b => (DayOfWeek)b.DayOfWeekID).Contains(a)).ToList();
                            foreach (var item in add)
                            {
                                carpool.CarPoolDays.Add(new DAL.CarPoolDay()
                                {
                                    CarPoolID = carpool.CarPoolID,
                                    DayOfWeekID = (int)item
                                });
                            }

                            db.SaveChanges();

                            return Request.CreateResponse(HttpStatusCode.Accepted, "Successfully updated carpool");
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.Unauthorized, "Cannot edit other users carpools");
                        }
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Ambiguous, "Invalid details provided");
                }
            }
            catch
            {
                // Nlog
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest, "Failed to update carpool...");
        }

        [Route("api/carpool/join/{id}")]
        [HttpGet]
        public HttpResponseMessage Join(int id)
        {
            try
            {
                using (DAL.CoMuteEntities db = new DAL.CoMuteEntities())
                {
                    DAL.UserCarPool pool = new DAL.UserCarPool()
                    {
                        CarPoolID = id,
                        UserID = UserID,
                        joinDate = DateTime.Now
                    };

                    db.UserCarPools.Add(pool);
                    db.SaveChanges();

                    if (pool.UserCarPoolID > 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Successfully joined carpool");
                    }
                }
            }
            catch
            {
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest, "Failed to join carpool...");
        }

        [Route("api/carpool/leave/{id}")]
        [HttpGet]
        public HttpResponseMessage Leave(int id)
        {
            try
            {
                using (DAL.CoMuteEntities db = new DAL.CoMuteEntities())
                {
                    DAL.UserCarPool pool = db.UserCarPools.SingleOrDefault(a => a.UserCarPoolID == id);

                    db.UserCarPools.Remove(pool);
                    db.SaveChanges();

                    if (pool.UserCarPoolID > 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Successfully left carpool");
                    }
                }
            }
            catch
            {
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest, "Failed to leave carpool...");
        }
    }
}
