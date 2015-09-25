using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomAuth
{
    public interface IBasicUserDetails
    {
        string emailAddress { get; set; }
        string password { get; set; }
    }
}
