using CoMute.Web.Models;
using CoMute.Web.Models.Dto;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Linq;
using CoMute.Web.Models.EntityFramework;
using System.Collections;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CoMute.Web.Controllers.API
{
    public class CarPoolController : ApiController
    {
        [AllowAnonymous]
        [Route("api/carpool/create")]
        public HttpResponseMessage CreateCarPool(CarPoolViewModel model)
        {
            using (CoMuteEntities db = new CoMuteEntities())
            {
                try
                {
                    if (model.CarPools.ID == 0)
                    {

                        //Add new
                        CarPools carpools = new CarPools();
                        CarPoolDays carpooldays = new CarPoolDays();

                        carpools.AvailableSeats = model.CarPools.AvailableSeats;
                        carpools.CarPoolDays = model.CarPools.CarPoolDays;
                        carpools.DateCreated = model.CarPools.DateCreated;
                        carpools.DepartureTime = model.CarPools.DepartureTime;
                        carpools.Destination = model.CarPools.Destination;
                        carpools.ETA = model.CarPools.ETA;
                        carpools.Notes = model.CarPools.Notes;
                        carpools.Origin = model.CarPools.Origin;
                        carpools.OwnerID = model.CarPools.OwnerID;

                        carpooldays.DayOfWeek = model.CarPoolDays.DayOfWeek;
                        carpooldays.CarPoolID = model.CarPools.ID;

                        db.CarPools.Add(carpools);
                        db.CarPoolDays.Add(carpooldays);

                        db.SaveChanges();

                    }
                    else
                    {
                        //Update
                        var carpools = (from n in db.CarPools
                                             where n.ID == model.CarPools.ID
                                             select n).SingleOrDefault();

                        var carpooldays = (from n in db.CarPoolDays
                                           where n.CarPoolID == model.CarPools.ID
                                        select n).SingleOrDefault();

                        carpools.AvailableSeats = model.CarPools.AvailableSeats;
                        carpools.CarPoolDays = model.CarPools.CarPoolDays;
                        carpools.DateCreated = model.CarPools.DateCreated;
                        carpools.DepartureTime = model.CarPools.DepartureTime;
                        carpools.Destination = model.CarPools.Destination;
                        carpools.ETA = model.CarPools.ETA;
                        carpools.Notes = model.CarPools.Notes;
                        carpools.Origin = model.CarPools.Origin;
                        carpools.OwnerID = model.CarPools.OwnerID;

                        carpooldays.DayOfWeek = model.CarPoolDays.DayOfWeek;

                        db.CarPools.Add(carpools);
                        db.CarPoolDays.Add(carpooldays);
                        db.SaveChanges();
                    }
                    return Request.CreateResponse(HttpStatusCode.Accepted);
                }
                catch (Exception ex)
                {
                    return Request.CreateResponse(ex);
                }
            };
        }

        [Route("api/carpool/get")]
        [HttpPost]
        public IEnumerable getCarPools()
        {

            using (CoMuteEntities db = new CoMuteEntities())
            {
                var n = from t in db.CarPools
                        select t;

                return n.ToList();
            }

        }

    }
}
