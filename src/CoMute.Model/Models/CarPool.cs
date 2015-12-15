﻿using CoMute.Model.Interface;
using System;
using System.Collections.Generic;

namespace CoMute.Model.Models
{
    public class CarPool : ICarPool
    {
        public long Id { get; set; }
        public int AvailableSeats { get; set; }
        public List<DayOfWeek> DaysAvailable { get; set; }
        public DateTime DepartureTime { get; set; }
        public string Destination { get; set; }
        public DateTime ExpectedArrivalTime { get; set; }
        public string Notes { get; set; }
        public string Origin { get; set; }
        public IUser Owner { get; set; }
    }
}
