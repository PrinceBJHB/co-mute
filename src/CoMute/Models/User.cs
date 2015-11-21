using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CoMute.Web.Models
{
    //[Table("Users")]
    public class User
    {
        //[Key]
        public int Id { get;set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EmailAddress { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }

        public static Boolean RegisterUser()
        {

            return true;
        }
    }

    public class CarPoolDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}
