using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoMute.Models
{
    public class CarPool
    {
        public Guid CarPoolID{ get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ExpectedArrivalTime { get; set; }
        public virtual Location Origin { get; set; }
        public int DaysAvailable { get; set; }
        public virtual Location Destination { get; set; } 
        public int AvailableSeats { get; set; }
        public virtual User Owner { get; set; }
        public string Notes { get; set; }
    }
}
