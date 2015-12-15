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
        long SaveUser(IUser user);
        IUser Login(string email, string password);
        IEnumerable<ICarPool> GetJoinedCarpools(string name);
        IEnumerable<ICarPool> GetOwnedCarpools(string name);
        long CreateCarpool(ICarPool carpool, string email);
    }
}
