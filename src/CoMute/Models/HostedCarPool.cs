using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoMute.Web.Models
{
	public class HostedCarPool// : IValidatableObject
	{
        [Display(Name = "Car pool ID")]
		public int Id { get; set; }
        [Required (ErrorMessage = "Departure time is required.")]
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
		public string Origin { get; set; }
		[Required(ErrorMessage = "Origin is required.")]
		public double OriginLat { get; set; }
		[Required(ErrorMessage = "Origin is required.")]
		public double OriginLon { get; set; }
        [Display(Name = "Destination")]
		public string Destination { get; set; }
		[Required(ErrorMessage = "Destination is required.")]
		public double DestinationLat { get; set; }
		[Required(ErrorMessage = "Destination is required.")]
		public double DestinationLon { get; set; }
		[Required(ErrorMessage = "At least one day must be selected.")]
		[Range(1,127, ErrorMessage = "At least one day must be selected.")]
        [Display(Name = "Active days")]
		public int DaysAvailable { get; set; }
		[Required(ErrorMessage = "Number of seats available is required.")]	
        [Display(Name = "Seats available")]
		[RegularExpression(@"([1-9])|(10)", ErrorMessage = "Please enter a number between 1 and 10.")]
		public int Seats { get; set; }
        [Display(Name = "Notes")]
		public string Notes { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd MMM yyyy}")]
        [Display(Name = "Creation date")]
		public DateTime CDate { get; set; }



        public int HostUserId { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        [Display(Name = "Name")]
        public string HostUserName { get; set; }
        [Required(ErrorMessage = "Surname is required.")]
        [Display(Name = "Surname")]
        public string HostUserSurname { get; set; }
        [Display(Name = "Phone number")]
        [Phone(ErrorMessage = "This is not a valid phone number.")]
        public string HostUserPhone { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "This is not a valid email address.")]
        [Display(Name = "Email address")]
        public string HostUserEmailAddress { get; set; }

        // User might start a car pool at 23:30 and arrive at 00:30
        //public IEnumerable<ValidationResult> Validate(ValidationContext context)
        //{
        //    if (Depart.Hours > Arrive.Hours || (Depart.Hours == Arrive.Hours && Depart.Minutes >= Arrive.Minutes))
        //    {
        //        yield return new ValidationResult("Departure time must be before arrival time.", new string[] { "Depart", "Arrive" });
        //    }
        //}
	}
}
