using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoMute.Web.Models.Dto
{
    public class JoinCarpoolRequest
    {
        public long CarpoolId { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ExpectedArrivalTime { get; set; }
        public DayOfWeek DaysAvailable{ get; set; }
    }
}