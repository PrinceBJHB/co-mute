using CoMute.Model.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoMute.Web.Models
{
    public class CarPoolIndexViewModel
    {
        public long Id { get; set; }
        public DateTime DateJoined { get; set; }
        public DateTime DepartureTime { get; set; }
        public string Destination { get; set; }
        public DateTime ExpectedArrivalTime { get; set; }
        public string Origin { get; set; }
        public IUser Owner { get; set; }
    }
}