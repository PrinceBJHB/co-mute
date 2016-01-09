using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoMute.Web.Models
{
    public class SearchFilter// : IValidatableObject
	{
		[Required(ErrorMessage = "Departure time is required.")]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh\\:mm}")]
		[RegularExpression(@"((([0-1][0-9])|(2[0-3]))(:[0-5][0-9])(:[0-5][0-9])?)", ErrorMessage = "Time must be between 00:00 and 23:59.")]
		[Display(Name = "Departure time")]
		public TimeSpan Depart { get; set; }
		[Required(ErrorMessage = "Arrival time is required.")]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh\\:mm}")]
		[RegularExpression(@"((([0-1][0-9])|(2[0-3]))(:[0-5][0-9])(:[0-5][0-9])?)", ErrorMessage = "Time must be between 00:00 and 23:59.")]
		[Display(Name = "Arrival time")]
		public TimeSpan Arrive { get; set; }
		[Display(Name = "Origin")]
        [Required(ErrorMessage = "Origin is required.")]
		public string Origin { get; set; }
        [Required(ErrorMessage = "Origin is required.")]
		public double OriginLat { get; set; }
		[Required(ErrorMessage = "Origin is required.")]
		public double OriginLon { get; set; }
		[Display(Name = "Destination")]
        [Required(ErrorMessage = "Destination is required.")]
		public string Destination { get; set; }
		[Required(ErrorMessage = "Destination is required.")]
		public double DestinationLat { get; set; }
		[Required(ErrorMessage = "Destination is required.")]
		public double DestinationLon { get; set; }
		[Required(ErrorMessage = "At least one day must be selected.")]
		[Range(1, 127)]
		[Display(Name = "Active days")]
		public int DaysAvailable { get; set; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext context)
        //{
        //    // User might start a car pool at 23:30 and arrive at 00:30
        //    //if (Depart.Hours > Arrive.Hours || (Depart.Hours == Arrive.Hours && Depart.Minutes >= Arrive.Minutes))
        //    //{
        //    //    yield return new ValidationResult("Departure time must be before arrival time.", new string[] { "Depart", "Arrive" });
        //    //}
 
        //}
	}
}
