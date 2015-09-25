using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace CustomAuth
{
    public class Principal <ProfileModel> : System.Security.Principal.IPrincipal //where ProfileModel : IProfileModel, new()
    {
        public IIdentity Identity { get; private set; }

        public Principal()
        {
            this.Identity = null;
        }

        public Principal(string username)
        {
            this.Identity = new GenericIdentity(username);
        }

        public bool IsInRole(string role)
        {
            if (Identity != null && Identity.IsAuthenticated && !string.IsNullOrWhiteSpace(role))
            {
                return ((IProfileModel)userProfile).IsInRole(role);
            }
            else
            {
                return false;
            }
        }

        public ProfileModel userProfile;
    }
}
