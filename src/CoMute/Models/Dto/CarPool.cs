using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoMute.Web.Models.Dto
{
    public class CarPool
    {
        [Required]
        public int UserCarPoolID { get; set; }

        [Required]
        public int CarPoolID { get; set; }

        [Required]
        public int UserID { get; set; }

        [Required]
        public TimeSpan departureTime { get; set; }

        [Required]
        public TimeSpan expectedArrivalTime { get; set; }

        [Required]
        public string origin { get; set; }

        [Required]
        public string destination { get; set; }

        [Required]
        public int seatsAvailable { get; set; }

        [Required]
        public IEnumerable<string> daysAvaiable { get; set; }
        public string notes { get; set; }
    }
}
