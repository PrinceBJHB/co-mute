using CoMute.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoMute.DAL
{
    public class CarPoolContext : DbContext
    {
        public DbSet<CarPool> CarPools { get; set; }

        public CarPoolContext() : base("dbCoMute")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
