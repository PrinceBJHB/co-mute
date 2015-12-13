using CoMute.Domain.Inteface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoMute.Model.Interface;
using CoMute.Model.Models;
using CoMute.Domain.Context;

namespace CoMute.Domain.Repo
{
    public class UserRepo : IUserRepo
    {
        public bool DeleteUser(long id)
        {
            throw new NotImplementedException();
        }

        public List<IUser> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public IUser GetUserByEmail(string email)
        {
            MySqlContext context = new MySqlContext();
            string lowerEmail = email.ToLower();
            User user = (User)context.User.FirstOrDefault(u => u.EmailAddress == lowerEmail);

            return user;
        }

        public IUser GetUserByID(long id)
        {
            throw new NotImplementedException();
        }

        public IUser SaveUser(IUser user)
        {
            throw new NotImplementedException();
        }
    }
}
