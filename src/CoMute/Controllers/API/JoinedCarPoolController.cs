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
	[RoutePrefix("api/joined")]
	public class JoinedCarPoolController : ApiController
	{
		[Route("add")]
		public HttpResponseMessage JoinCarPool(int userId, JoinCarPoolRequest jcpr)
		{
			var result = DataQuery.JoinCarPool(userId, jcpr).Result;
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
		public HttpResponseMessage LeaveCarPool(int userId, int carPoolId)
		{
			var result = DataQuery.LeaveCarPool(userId, carPoolId).Result;
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
		public HttpResponseMessage GetJoinedCarPools(int userId)
		{
			var result = DataQuery.GetJoinedCarPools(userId).Result;
			if (result.IsSuccess)
			{
				List<JoinedCarPool> joinedCarPools = new List<JoinedCarPool>();
				foreach (GetJoinedCarPools_Result carPool in result.Result)
				{
					joinedCarPools.Add(new JoinedCarPool()
					{
						CDate = carPool.JoinedCDate,
						DaysAvailable = carPool.JoinedDaysAvailable,

                        CarPoolId = carPool.CarPoolId,
                        CarPoolDepart = carPool.Depart,
                        CarPoolArrive = carPool.Arrive,
                        CarPoolOrigin = carPool.Origin,
                        CarPoolOriginLat = carPool.OriginLat.Value,
                        CarPoolOriginLon = carPool.OriginLon.Value,
                        CarPoolDestination = carPool.Destination,
                        CarPoolDestinationLat = carPool.DestinationLat.Value,
                        CarPoolDestinationLon = carPool.DestinationLon.Value,
                            
                                
						CarPoolDaysAvailable = carPool.CarPoolDaysAvailable,
						CarPoolSeats = carPool.Seats,
						CarPoolNotes = carPool.Notes,
						CarPoolCDate = carPool.CarPoolCDate,

                        CarPoolHostUserId = carPool.HostUserId,
                        CarPoolHostUserName = carPool.Name,
                        CarPoolHostUserSurname = carPool.Surname,
                        CarPoolHostUserPhone = carPool.Phone,
                        CarPoolHostUserEmailAddress = carPool.Email

						
					});
				}
				HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
				response.Content = new ObjectContent(typeof(List<JoinedCarPool>), joinedCarPools, GlobalConfiguration.Configuration.Formatters.JsonFormatter);
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