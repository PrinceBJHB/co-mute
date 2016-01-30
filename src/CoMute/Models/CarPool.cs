using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoMute.Web.Models
{
    public class CarPool
    {
        public string CarPoolId { get; set; }
        public DateTime Departure  {get;set;}
        public DateTime Arrival {get;set;}
        public string Origin {get;set;}
        public int Days {get;set;}
        public string Destination {get;set;}
        public int Seats {get;set;}
        public string Leader {get;set;}
        public string Notes {get;set;}
        private List<string> Members;
    }
}
