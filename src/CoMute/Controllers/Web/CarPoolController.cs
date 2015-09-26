using CustomAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoMute.Web.Controllers.Web
{
    [BasicAuth]
    public class CarPoolController : Controller
    {
        // GET: CarPool
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _GetCarpool(Models.Dto.CarPoolRequest model)
        {
            int UserID = AuthHelper<Models.UserProfile>.userProfile.UserID;
            string view = model.UserID == UserID ? "_CarPoolEdit" : "_CarPoolView";
            return PartialView(view, (Models.CarPool) model);
        }

        public ActionResult _MyCarPools(List<Models.Dto.CarPool> model)
        {
            return PartialView(model);
        }
    }
}