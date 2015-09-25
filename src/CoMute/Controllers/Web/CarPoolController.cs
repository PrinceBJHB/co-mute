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

        public ActionResult _CarPool(Models.Dto.CarPoolRequest model)
        {
            return PartialView(model);
        }
    }
}