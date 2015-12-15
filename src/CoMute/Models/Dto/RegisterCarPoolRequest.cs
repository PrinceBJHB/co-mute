using CoMute.Model.Interface;
using System;
using System.Collections.Generic;

namespace CoMute.Web.Models.Dto
{
    public class RegisterCarPoolRequest
    {
        public int AvailableSeats { get; set; }
        public DayOfWeek DaysAvailable { get; set; }
        public DateTime DepartureTime { get; set; }
        public string Destination { get; set; }
        public DateTime ExpectedArrivalTime { get; set; }
        public string Notes { get; set; }
        public string Origin { get; set; }
        public IUser Owner { get; set; }
    }
}