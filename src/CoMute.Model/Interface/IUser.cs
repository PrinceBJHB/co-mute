using CoMute.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoMute.Model.Interface
{
    public interface IUser
    {
        long Id { get; set; }
        string Name { get; set; }
        string Surname { get; set; }
        string EmailAddress { get; set; }
        string PhoneNumber { get; set; }
        string Password { get; set; }

        List<CarPool> JoinedCarpools { get; set; }
    }
}
