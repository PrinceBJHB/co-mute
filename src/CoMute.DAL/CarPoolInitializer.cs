using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoMute.DAL
{
    public class CarPoolInitializer : DropCreateDatabaseIfModelChanges<CarPoolContext>
    {
        protected override void Seed(CarPoolContext context)
        {
        }
    }
}
