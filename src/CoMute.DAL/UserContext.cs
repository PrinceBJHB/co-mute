using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using CoMute.Models;

namespace CoMute.DAL
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; } 

        public UserContext() : base("dbCoMute")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }


    }
}
