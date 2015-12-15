using System;
using System.Collections.Generic;

namespace CoMute.Model.Interface
{
    public interface ICarPool
    {
        long Id { get; set; }
        DateTime DepartureTime { get; set; }
        DateTime ExpectedArrivalTime { get; set; }
        string Origin { get; set; }
        List<DayOfWeek> DaysAvailable { get; set; }
        string Destination { get; set; }
        int AvailableSeats { get; set; }
        string Notes { get; set; }
        IUser Owner { get; set; }
    }
}
