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
	public class ApiSearchController : ApiController
    {
		[Route("get")]
		public HttpResponseMessage SearchCarPools(SearchFilter sf)
        {
			var result = DataQuery.SearchCarPools(sf).Result;
			if (result.IsSuccess)
			{
				List<SearchResult> hostedCarPools = new List<SearchResult>();
				foreach (SearchCarPools_Result carPool in result.Result)
				{
					hostedCarPools.Add(new SearchResult()
					{
                        DaysWithSeats = carPool.DaysWithSeats,

                        CarPoolId = carPool.Id,
                        CarPoolDepart = carPool.Depart,
                        CarPoolArrive = carPool.Arrive,
                        CarPoolOrigin = carPool.Origin,
                        CarPoolOriginLat = carPool.OriginLat.Value,
                        CarPoolOriginLon = carPool.OriginLon.Value,
                        CarPoolDestination = carPool.Destination,
                        CarPoolDestinationLat = carPool.DestinationLat.Value,
                        CarPoolDestinationLon = carPool.DestinationLon.Value,
                        CarPoolDaysAvailable = carPool.DaysAvailable,
                        CarPoolSeats = carPool.Seats,
                        CarPoolNotes = carPool.Notes,
                        CarPoolCDate = carPool.CDate,

                        CarPoolHostUserId = carPool.HostUserId,
                        CarPoolHostUserName = carPool.Name,
                        CarPoolHostUserSurname = carPool.Surname,
                        CarPoolHostUserPhone = carPool.Phone,
                        CarPoolHostUserEmailAddress = carPool.Email
											
					});
				}
				HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
				response.Content = new ObjectContent(typeof(List<SearchResult>), hostedCarPools, GlobalConfiguration.Configuration.Formatters.JsonFormatter);
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