using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CoMute.Web.Models
{
    public class CoMuteWebContext : DbContext
    {
            
        public CoMuteWebContext() : base("name=CoMuteWebContext")
        {
        }

        public System.Data.Entity.DbSet<CoMute.Web.Models.User> Users { get; set; }

        public System.Data.Entity.DbSet<CoMute.Web.Models.Car_pool> Car_pool { get; set; }
    
    }
}
