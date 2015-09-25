using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoMute.Web.Models
{
    public class CarPool
    {
        [Required]
        public int UserID { get; set; }

        [Required]
        public DateTime departureTime { get; set; }

        [Required]
        public DateTime expectedArrivalTime { get; set; }

        [Required]
        public string origin { get; set; }

        [Required]
        public string destination { get; set; }

        [Required]
        public int seatsAvailable { get; set; }

        public List<DayOfWeek> daysAvaiable { get; set; }

        public string notes { get; set; }
    }
}
