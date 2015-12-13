using CoMute.Model.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoMute.Domain.Context
{
    public class MySqlContext : DbContext
    {
        public MySqlContext() : base(Properties.Settings.Default.MySQLConnection)
        {
        }

        public virtual DbSet<IUser> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<IUser>().HasKey(u => u.Id);
            modelBuilder.Entity<IUser>().HasRequired(u => u.EmailAddress);
            modelBuilder.Entity<IUser>().HasRequired(u => u.Name);
            modelBuilder.Entity<IUser>().HasRequired(u => u.Surname);
            modelBuilder.Entity<IUser>().HasRequired(u => u.Password);
            modelBuilder.Entity<IUser>().HasOptional(u => u.PhoneNumber);


        }
    }
}
