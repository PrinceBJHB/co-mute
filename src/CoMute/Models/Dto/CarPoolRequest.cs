
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace CoMute.Web.Models.Dto
{
    public class CarPoolRequest
    {
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
        public List<DayOfWeek> daysAvaiable { get; set; }

        public string notes { get; set; }

        public static implicit operator CarPoolRequest(DAL.CarPool model)
        {
            if (model == null)
                return null;
            
            CarPoolRequest result = new CarPoolRequest()
            {
                CarPoolID = model.CarPoolID,
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
