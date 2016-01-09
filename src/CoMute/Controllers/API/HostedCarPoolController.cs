using System;
using CoMute.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CoMute.Web.Data;

namespace CoMute.Web.Controllers.API
{
	[RoutePrefix("api/hosted")]
    public class HostedCarPoolController : ApiController
    {
		[Route("add")]
		public HttpResponseMessage CreateCarPool(HostedCarPool hcp)
		{
			var result = DataQuery.CreateCarPool(hcp).Result;
			if (result.IsSuccess)
			{
				return new HttpResponseMessage(HttpStatusCode.OK);
			}
			else
			{
				HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.BadRequest);
				response.Content = new ObjectContent(typeof(string), result.Error, GlobalConfiguration.Configuration.Formatters.JsonFormatter);
				return response;
			}	
		}

		[Route("disable")]
		public HttpResponseMessage CancelCarPool(int carPoolId)
		{
			var result = DataQuery.CancelCarPool(carPoolId).Result;
			if (result.IsSuccess)
			{
				return new HttpResponseMessage(HttpStatusCode.OK);
			}
			else
			{
				HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.BadRequest);
				response.Content = new ObjectContent(typeof(string), result.Error, GlobalConfiguration.Configuration.Formatters.JsonFormatter);
				return response;
			}	
		}

		[Route("get")]
		public HttpResponseMessage GetHostedCarPools(int userId)
		{
			var result = DataQuery.GetHostedCarPools(userId).Result;
			if (result.IsSuccess)
			{
				List<HostedCarPool> hostedCarPools = new List<HostedCarPool>();
				foreach (GetHostedCarPools_Result carPool in result.Result)
				{
					hostedCarPools.Add(new HostedCarPool()
					{
						Id = carPool.Id,
						Depart = carPool.Depart,
						Arrive = carPool.Arrive,
						Origin = carPool.Origin,
						OriginLat = carPool.OriginLat.Value,
						OriginLon = carPool.OriginLon.Value,
						Destination = carPool.Destination,
						DestinationLat = carPool.DestinationLat.Value,
						DestinationLon = carPool.DestinationLon.Value,    
						DaysAvailable = carPool.DaysAvailable,
						Seats = carPool.Seats,
						Notes = carPool.Notes,
						CDate = carPool.CDate,

                        HostUserId = carPool.HostUserId,
                        HostUserName = carPool.Name,
                        HostUserSurname = carPool.Surname,
                        HostUserPhone = carPool.Phone,
                        HostUserEmailAddress = carPool.Email
					});
				}
				HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
				response.Content = new ObjectContent(typeof(List<HostedCarPool>), hostedCarPools, GlobalConfiguration.Configuration.Formatters.JsonFormatter);
				return response;
			}
			else
			{
				HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.BadRequest);
				response.Content = new ObjectContent(typeof(string), result.Error, GlobalConfiguration.Configuration.Formatters.JsonFormatter);
				return response;
			}	
		}
    }
}