using CoMute.Model.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoMute.Domain.Inteface
{
    public interface IUserRepo
    {
        IEnumerable<IUser> GetAllUsers();
        IUser GetUserByID(long id);
        IUser GetUserByEmail(string email);
        IUser SaveUser(IUser user);
        bool DeleteUser(long id);
    }
}
