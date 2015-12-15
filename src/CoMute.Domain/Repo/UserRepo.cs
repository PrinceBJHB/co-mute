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

        public long SaveUser(IUser user)
        {
            try
            {
                if (GetUserByEmail(user.EmailAddress) != null)
                    throw new Exception("emailalreadyexistsexception");//email already exists

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

                return user.Id;
            }
            catch (Exception ex)
            {
                //log exception
                throw new Exception("An error has occurred. Contact someone for support.");
            }
        }

        public IEnumerable<ICarPool> GetJoinedCarpools(string name)
        {
            var user = GetUserByEmail(name);
            if (user == null)
                return null;

            var cps = user.JoinedCarpools.Select(c => c.Id);
            return context.CarPool.Where(c => cps.Contains(c.Id));
        }

        public IEnumerable<ICarPool> GetOwnedCarpools(string name)
        {
            return context.CarPool.Where(c => c.Owner.EmailAddress == name);
        }

        public long CreateCarpool(ICarPool carpool, string email)
        {
            try
            {
                //get carpool opportunities created by user.
                var carpools = GetOwnedCarpools(email);

                //Check time frames
                if (carpools.Any(cp => cp.DaysAvailable.Any(d => carpool.DaysAvailable.Equals(d))
                    && carpool.DepartureTime < cp.ExpectedArrivalTime && carpool.ExpectedArrivalTime > cp.DepartureTime))
                    throw new Exception("timeframeoverlappingexception");//you already joined a carpool within tis time slot.

                if (carpool.Id < 1)
                {
                    context.CarPool.Add((CarPool)carpool);

                    context.Entry(carpool).State = EntityState.Added;
                }

                context.SaveChanges();

                return carpool.Id;
            }
            catch (Exception ex)
            {
                //log exception
                throw new Exception("An error has occurred. Contact someone for support.");
            }
        }

        public bool JoinCarpool(long userId, long carpoolId)
        {
            try
            {
                //Check if user has not joined overlapping carpool event.
                //Check if there are seats available.
                //join if both checks passes.   

                return false;
            }
            catch (Exception ex)
            {
                //log exception
                throw new Exception("An error has occurred. Contact someone for support.");
            }
        }


        public bool JoinCarpool(long carpoolId, string email)
        {
            //Join the carpool
            return true;
        }


        public IEnumerable<ICarPool> SearchCarpool(string search)
        {
            //search on origin and destination and departure time and expected arrival time.
            return new List<CarPool>();
        }
    }
}
