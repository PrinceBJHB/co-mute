using CoMute.Domain.Inteface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoMute.Model.Interface;
using CoMute.Model.Models;
using CoMute.Domain.Context;
using System.Data.Entity;

namespace CoMute.Domain.Repo
{
    public class UserRepo : IUserRepo
    {
        MySqlContext context;

        public UserRepo()
        {
            context = new MySqlContext();
        }

        public bool DeleteUser(long id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IUser> GetAllUsers()
        {
            try
            {
                IEnumerable<IUser> users = context.User;

                return users;
            }
            catch (Exception)
            {
                //log exception
                throw new Exception("An error has occurred. Contact someone for support.");
            }
        }

        public IUser GetUserByEmail(string email)
        {
            try
            {
                string lowerEmail = email.ToLower();

                return context.User.FirstOrDefault(u => u.EmailAddress == lowerEmail);
            }
            catch (Exception)
            {
                //log exception
                throw new Exception("An error has occurred. Contact someone for support.");
            }
        }

        public IUser GetUserByID(long id)
        {
            try
            {
                return context.User.FirstOrDefault(u => u.Id == id);
            }
            catch (Exception)
            {
                //log exception
                throw new Exception("An error has occurred. Contact someone for support.");
            }
        }

        public IUser Login(string email, string password)
        {
            try
            {
                var user = GetUserByEmail(email);

                if (user != null && user.Password == password)
                    return user;

                return null;
            }
            catch (Exception)
            {
                //log exception
                throw new Exception("An error has occurred. Contact someone for support.");
            }
        }

        public IUser SaveUser(IUser user)
        {
            try
            {
                if (user.Id < 1)
                {
                    context.User.Add((User)user);

                    context.Entry(user).State = EntityState.Added;
                }
                else
                {
                    var userToUpdate = (User)GetUserByID(user.Id);

                    userToUpdate.Name = user.Name;
                    userToUpdate.Surname = user.Surname;
                    userToUpdate.EmailAddress = user.EmailAddress;
                    userToUpdate.Password = user.Password;

                    context.Entry(userToUpdate).State = EntityState.Modified;
                }

                context.SaveChanges();

                return GetUserByID(user.Id);
            }
            catch (Exception ex)
            {
                //log exception
                throw new Exception("An error has occurred. Contact someone for support.");
            }
        }
    }
}
