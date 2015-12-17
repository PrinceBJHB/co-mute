using CoMute.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoMute.DAL
{
    public class LocationInitializer : DropCreateDatabaseIfModelChanges<LocationContext>
    {
        protected override void Seed(LocationContext context)
        {
            var locations = new List<Location>() { new Location { LocationName = "Cape Town" }, new Location { LocationName = "Bellville" }, new Location { LocationName = "Somerset West" } };
            locations.ForEach(l => context.Locations.Add(l));
            context.SaveChanges();
        }
    }
}
