using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CoMute.Web.Extensions;
using System.Web.Mvc;

namespace CoMute.Web.Models
{
    public class CarPool
    {
        [Required]
        [HiddenInput(DisplayValue = false)]
        public int CarPoolID { get; set; }

        [Required]
        [HiddenInput(DisplayValue = false)]
        public int UserID { get; set; }

        [Required]
        [Display(Name = "Departure Time")]
        [DataType(DataType.Time)] //, DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public TimeSpan departureTime { get; set; }

        [Required]
        [Display(Name = "Expected Arrival Time")]
        [DataType(DataType.Time)]
        public TimeSpan expectedArrivalTime { get; set; }

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
        [Display(Name = "Days Available")]
        [UIHint("DayOfWeek")]
        [EnsureOneElement(ErrorMessage = "Must select at least one day for the carpool")]
        public List<DayOfWeek> daysAvaiable { get; set; }

        [Display(Name = "Travel Notes")]
        [DataType(DataType.MultilineText)]
        public string notes { get; set; }

        public static implicit operator CarPool(Dto.CarPoolRequest model)
        {
            if (model == null)
                return null;

            CarPool result = new CarPool()
            {
                CarPoolID = model.CarPoolID,
                UserID = model.UserID,
                seatsAvailable = model.seatsAvailable,
                origin = model.origin,
                notes = model.notes,
                expectedArrivalTime = model.expectedArrivalTime,
                destination = model.destination,
                departureTime = model.departureTime,
                daysAvaiable = model.daysAvaiable
            };

            return result;
        }
    }
}
