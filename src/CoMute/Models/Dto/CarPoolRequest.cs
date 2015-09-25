
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace CoMute.Web.Models.Dto
{
    public class CarPoolRequest
    {
        [Required(ErrorMessage = "Email address is required")]
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        public string emailAddress { get; set; }

        [Required]
        public int UserID { get; set; }

        [Required]
        [Display(Name = "Departure Time")]
        [DataType(DataType.Time)]
        public DateTime departureTime { get; set; }

        [Required]
        [Display(Name = "Expected Arrival Time")]
        [DataType(DataType.Time)]
        public DateTime expectedArrivalTime { get; set; }

        [Required]
        [Display(Name = "Leaving from")]
        public string origin { get; set; }

        [Required]
        [Display(Name = "Destination")]
        public string destination { get; set; }

        [Required]
        [Display(Name = "Available Seats")]
        public int seatsAvailable { get; set; }

        [Required]
        public List<DayOfWeek> daysAvaiable { get; set; }

        public string notes { get; set; }
    }
}
