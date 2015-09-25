using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomAuth
{
    public interface IProfileModel
    {
        string firstName { get; set; }
        string lastName { get; set; }
        string emailAddress { get; set; }
        string userType { get; set; }
        bool rememberMe { get; set; }
        bool Login(string username, string password);
        bool IsInRole(string role);
    }
}
