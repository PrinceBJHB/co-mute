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
    public class ProfileController : Controller
    {
        public ActionResult Profile()
        {
            if (StaticValues.StaticValues.CurrentUser == null) return RedirectToAction("Login", "Home"); 
            return View();
        }

		#region Personal details

		public ActionResult ViewPersonalDetails()
        {
            var result = new UserController().GetUser(StaticValues.StaticValues.CurrentUser.Id);

            if (result.StatusCode == HttpStatusCode.OK)
            {
                ObjectContent objContent = result.Content as ObjectContent;
                User user = objContent.Value as User;
                StaticValues.StaticValues.CurrentUser = user;
                return PartialView("_PersonalDetailsView", user);
            }
            else
            {
                ObjectContent objContent = result.Content as ObjectContent;
                ModelState.AddModelError("", objContent.Value.ToString());
                return PartialView("_PersonalDetailsView", new User());
            }
        }

        public ActionResult EditPersonalDetails()
        {
            return PartialView("_PersonalDetailsEdit", StaticValues.StaticValues.CurrentUser);
        }

        public ActionResult UpdatePersonalDetails(User user)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_PersonalDetailsEdit", user);
            }
            var result = new UserController().UpdateProfile(user);

            if (result.StatusCode == HttpStatusCode.OK)
            {
                StaticValues.StaticValues.CurrentUser = user;
                return PartialView("_PersonalDetailsView", user);
            }
            else
            {
                ObjectContent objContent = result.Content as ObjectContent;
                ModelState.AddModelError("EmailAddress", objContent.Value.ToString());
                return PartialView("_PersonalDetailsEdit", user);
            }
        }

		public ActionResult UpdatePassword()
		{
			return PartialView("_UpdatePassword");
		}

		public ActionResult SendUpdatePassword(UpdatePasswordRequest upr)
		{
			UserController Uc = new UserController();
			if (Uc.CalculateMD5Hash(upr.Password) != StaticValues.StaticValues.CurrentUser.Password)
			{
				ModelState.AddModelError("Password", "The password entered is incorrect.");
			}
			if (!ModelState.IsValid)
			{
				return PartialView("_UpdatePassword", upr);
			}
			var result = Uc.UpdatePassword(StaticValues.StaticValues.CurrentUser.Id, upr.NewPassword);

			if (result.StatusCode == HttpStatusCode.OK)
			{
				StaticValues.StaticValues.CurrentUser.Password = Uc.CalculateMD5Hash(upr.NewPassword);
				ModelState.AddModelError("", "Your password has been changed.");
				return PartialView("_UpdatePassword", upr);
			}
			else
			{
				ObjectContent objContent = result.Content as ObjectContent;
				return PartialView("_UpdatePassword", upr);
			}
		}

		#endregion Personal details

		#region Hosted car pools

		public ActionResult ViewHostedCarPools()
        {
            var result = new HostedCarPoolController().GetHostedCarPools(StaticValues.StaticValues.CurrentUser.Id);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                ObjectContent objContent = result.Content as ObjectContent;
                List<HostedCarPool> hcps = objContent.Value as List<HostedCarPool>;
                return PartialView("_HostedCarPools", hcps);
            }
            else
            {
                ObjectContent objContent = result.Content as ObjectContent;
                ModelState.AddModelError("", objContent.Value.ToString());
                return PartialView("_HostedCarPools", new List<HostedCarPool>());
            }

        }

		public ActionResult AddHostedCarPool()
		{
			return PartialView("_HostedCarPoolAdd");
		}

		[HttpPost]
        public ActionResult AddHostedCarPool(HostedCarPool hcp)
        {
			hcp.HostUserId = StaticValues.StaticValues.CurrentUser.Id;
            hcp.HostUserName = StaticValues.StaticValues.CurrentUser.Name;
            hcp.HostUserSurname = StaticValues.StaticValues.CurrentUser.Surname;
            hcp.HostUserPhone = StaticValues.StaticValues.CurrentUser.Phone;
            hcp.HostUserEmailAddress = StaticValues.StaticValues.CurrentUser.EmailAddress;

			hcp.CDate = DateTime.Now;
			hcp.Id = 0;
			ModelState.Clear();
			var b = TryValidateModel(hcp);
			if (!ModelState.IsValid)
			{
				return PartialView("_HostedCarPoolAdd", hcp);
			}
			var result = new HostedCarPoolController().CreateCarPool(hcp);

			if (result.StatusCode == HttpStatusCode.OK)
			{
				return PartialView("_HostedCarPoolAdd");
			}
			else
			{
				ObjectContent objContent = result.Content as ObjectContent;
				ModelState.AddModelError("", objContent.Value.ToString());
				return PartialView("_HostedCarPoolAdd", hcp);
			}
        }

		public ActionResult HostedCarPoolDetails(HostedCarPool hcp)
		{
			return PartialView("_HostedCarPoolDetails", hcp);
		}

		public ActionResult CancelHostedCarPool(HostedCarPool hcp)
		{
			var result = new HostedCarPoolController().CancelCarPool(hcp.Id);

			if (result.StatusCode == HttpStatusCode.OK)
			{
				return PartialView("_HostedCarPoolDetails", hcp);
			}
			else
			{
				ObjectContent objContent = result.Content as ObjectContent;
				ModelState.AddModelError("", objContent.Value.ToString());
				return PartialView("_HostedCarPoolDetails", hcp);
			}
		}

		#endregion Hosted car pools

		#region Joined car pools

		public ActionResult ViewJoinedCarPools()
		{
			var result = new JoinedCarPoolController().GetJoinedCarPools(StaticValues.StaticValues.CurrentUser.Id);
			if (result.StatusCode == HttpStatusCode.OK)
			{
				ObjectContent objContent = result.Content as ObjectContent;
				List<JoinedCarPool> jcps = objContent.Value as List<JoinedCarPool>;
				return PartialView("_JoinedCarPools", jcps);
			}
			else
			{
				ObjectContent objContent = result.Content as ObjectContent;
				ModelState.AddModelError("", objContent.Value.ToString());
				return PartialView("_JoinedCarPools", new List<JoinedCarPool>());
			}
		}

		public ActionResult JoinedCarPoolDetails(JoinedCarPool jcp)
		{
			return PartialView("_JoinedCarPoolDetails", jcp);
		}

		public ActionResult LeaveJoinedCarPool(JoinedCarPool jcp)
		{
			var result = new JoinedCarPoolController().LeaveCarPool(jcp.CarPoolId, StaticValues.StaticValues.CurrentUser.Id);

			if (result.StatusCode == HttpStatusCode.OK)
			{
				return PartialView("_JoinedCarPoolDetails", jcp);
			}
			else
			{
				ObjectContent objContent = result.Content as ObjectContent;
				ModelState.AddModelError("", objContent.Value.ToString());
				return PartialView("_JoinedCarPoolDetails", jcp);
			}
		}

		#endregion Joined car pools
	}
}