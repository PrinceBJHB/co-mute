using CoMute.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace CoMute.DAL
{
    public class UserInitializer : DropCreateDatabaseIfModelChanges<UserContext>
    {
        protected override void Seed(UserContext context)
        {
            var users = new List<User>() { new User { Name = "Richard", Surname = "Trevor", Email = "rktrevor@gmail.com", Password = "password", Phone = "0847364278", UserId = Guid.NewGuid() } };
            users.ForEach(u => context.Users.Add(u));
            context.SaveChanges();
        }
    }
}
