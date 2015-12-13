using CoMute.Model.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoMute.Model.Models
{
    class CarPool : ICarPool
    {
        public int AvailableSeats { get; set; }
        public DateTime DaysAvailable { get; set; }
        public DateTime DepartureTime { get; set; }
        public string Destination { get; set; }
        public DateTime ExpectedArrivalTime { get; set; }
        public string Notes { get; set; }
        public string Origin { get; set; }
        public IUser Owner { get; set; }


    }
}
