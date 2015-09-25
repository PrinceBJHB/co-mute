
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace CoMute.Web.Models.Dto
{
    public class CarPoolRequest
    {
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

        public static implicit operator CarPoolRequest(DAL.CarPool model)
        {
            if (model == null)
                return null;

            CarPoolRequest result = new CarPoolRequest()
            {
                UserID = model.UserID,
                seatsAvailable = model.seatsAvailable,
                origin = model.origin,
                notes = model.notes,
                expectedArrivalTime = model.expectedArrivalTime,
                destination = model.destination,
                departureTime = model.departureTime
            };

            result.daysAvaiable = new List<DayOfWeek>();

            foreach(var item in model.CarPoolDays) {
                result.daysAvaiable.Add((DayOfWeek)item.DayOfWeekID);
            }

            return result;
        }
    }
}
