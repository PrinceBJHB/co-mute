using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoMute.Model.Interface
{
    public interface ICarPool
    {
        DateTime DepartureTime { get; set; }
        DateTime ExpectedArrivalTime { get; set; }
        string Origin { get; set; }
        DateTime DaysAvailable { get; set; }
        string Destination { get; set; }
        int AvailableSeats { get; set; }
        string Notes { get; set; }
        IUser Owner { get; set; }
    }
}
