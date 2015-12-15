using CoMute.Model.Models;
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
        public MySqlContext() : base("name=ComuteConnection")
        {
        }

        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<CarPool> CarPool { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<User>().ToTable("User");

            modelBuilder.Entity<CarPool>().HasKey(u => u.Id);
            modelBuilder.Entity<CarPool>().ToTable("CarPool");

            modelBuilder.Entity<User>().HasMany(u => u.JoinedCarpools).WithMany().Map(m =>
            {
                m.MapLeftKey("UserId")
                .MapRightKey("CarPoolId")
                .ToTable("User_Carpool");
            });
        }
    }
}
