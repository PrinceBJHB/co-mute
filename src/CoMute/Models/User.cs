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
        public string Createdcarpools { get; set; }
        public string Joinedcarpools { get; set; }

    }

    public class CarPool
    {
        //[Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Departure { get; set; }
        public string Arrival { get; set; }
        public string Origin { get; set; }
        public string Days { get; set; }
        public string Destination { get; set; }
        public string Seats { get; set; }
        public string Owner { get; set; }
        public string Notes { get; set; }

    }

    public class CarPoolDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<CarPool> CarPools { get; set; }
    }
}
