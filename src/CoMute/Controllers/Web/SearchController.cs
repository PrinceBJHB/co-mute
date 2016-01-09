using CoMute.Web.Controllers.API;
using CoMute.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CoMute.Web.Controllers.Web
{
    public class SearchController : Controller
    {
        public ActionResult Search()
        {
            if (StaticValues.StaticValues.CurrentUser == null) return RedirectToAction("Login", "Home");
            return View();
        }

		public async Task<ActionResult> SearchCarPools(SearchFilter sf)
		{
			if (!ModelState.IsValid)
			{
				return PartialView("Search", sf);
			}

            var result = await Task.Run(() => { return new ApiSearchController().SearchCarPools(sf); }).ConfigureAwait(false);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                ObjectContent objContent = result.Content as ObjectContent;
				List<SearchResult> sr = objContent.Value as List<SearchResult>;
                return PartialView("_SearchResults", sr);
            }
            else
            {
                ObjectContent objContent = result.Content as ObjectContent;
                ModelState.AddModelError("", objContent.Value.ToString());
				return PartialView("Search", sf);
            }
		}

        public ActionResult SearchResultDetails(SearchResult sr)
        {
            JoinCarPoolRequest jcpr = new JoinCarPoolRequest()
            {
                SelectedDays = 0,

                DaysWithSeats = sr.DaysWithSeats,
                CarPoolId = sr.CarPoolId,
                CarPoolDepart = sr.CarPoolDepart,
                CarPoolArrive = sr.CarPoolArrive,
                CarPoolOrigin = sr.CarPoolOrigin,
                CarPoolOriginLat = sr.CarPoolOriginLat,
                CarPoolOriginLon = sr.CarPoolOriginLon,
                CarPoolDestination = sr.CarPoolDestination,
                CarPoolDestinationLat = sr.CarPoolDestinationLat,
                CarPoolDestinationLon = sr.CarPoolDestinationLon,
                CarPoolDaysAvailable = sr.CarPoolDaysAvailable,
                CarPoolSeats = sr.CarPoolSeats,
                CarPoolNotes = sr.CarPoolNotes,
                CarPoolCDate = sr.CarPoolCDate,

                CarPoolHostUserId = sr.CarPoolHostUserId,
                CarPoolHostUserName = sr.CarPoolHostUserName,
                CarPoolHostUserSurname = sr.CarPoolHostUserSurname,
                CarPoolHostUserPhone = sr.CarPoolHostUserPhone,
                CarPoolHostUserEmailAddress = sr.CarPoolHostUserEmailAddress,

            };
            return View(jcpr);
        }

        public ActionResult JoinCarPool(JoinCarPoolRequest jcpr)
        {
			if (!ModelState.IsValid)
			{
				return PartialView("Search", jcpr);
			}

			var result = new JoinedCarPoolController().JoinCarPool(StaticValues.StaticValues.CurrentUser.Id, jcpr);
			if (result.StatusCode == HttpStatusCode.OK)
			{
				return RedirectToAction("Profile", "Profile");
			}
			else
			{
				ObjectContent objContent = result.Content as ObjectContent;
				ModelState.AddModelError("", objContent.Value.ToString());
				return View("SearchResultDetails", jcpr);
			}
            
        }
    }
}