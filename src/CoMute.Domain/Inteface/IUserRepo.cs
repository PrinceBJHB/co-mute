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
        IEnumerable<ICarPool> GetJoinedCarpools(string email);
        IEnumerable<ICarPool> GetOwnedCarpools(string email);
        long CreateCarpool(ICarPool carpool, string email);
        bool JoinCarpool(long carpoolId, string email);
        IEnumerable<ICarPool> SearchCarpool(string search);
    }
}
