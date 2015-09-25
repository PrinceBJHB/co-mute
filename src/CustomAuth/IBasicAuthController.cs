using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CustomAuth
{
    /// <summary>
    /// Web.Config - > loginUrl="~/Controller/Login"
    /// </summary>
    public interface IBasicAuthController<profileModel, loginModel> where profileModel : class, IProfileModel, new() where loginModel : class, IBasicUserDetails, new()
    {
        ActionResult Login(string ReturnUrl);
        [HttpPost]
        ActionResult Login(loginModel model, string ReturnUrl);
        ActionResult Logout();
    }
}
