using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoMute.Web.Models
{
	public class SearchResult
	{
		public int DaysWithSeats { get; set; }

        [Display(Name = "Car pool ID")]
        public int CarPoolId { get; set; }
        [Required (ErrorMessage = "Departure time is required.")]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh\\:mm}")]
		[RegularExpression(@"((([0-1][0-9])|(2[0-3]))(:[0-5][0-9])(:[0-5][0-9])?)", ErrorMessage = "Time must be between 00:00 and 23:59.")]
        [Display(Name = "Departure time")]
        public TimeSpan CarPoolDepart { get; set; }
		[Required(ErrorMessage = "Arrival time is required.")]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh\\:mm}")]
		[RegularExpression(@"((([0-1][0-9])|(2[0-3]))(:[0-5][0-9])(:[0-5][0-9])?)", ErrorMessage = "Time must be between 00:00 and 23:59.")]
        [Display(Name = "Arrival time")]
        public TimeSpan CarPoolArrive { get; set; }
        [Display(Name = "Origin")]
        public string CarPoolOrigin { get; set; }
		[Required(ErrorMessage = "Origin is required.")]
        public double CarPoolOriginLat { get; set; }
		[Required(ErrorMessage = "Origin is required.")]
        public double CarPoolOriginLon { get; set; }
        [Display(Name = "Destination")]
        public string CarPoolDestination { get; set; }
		[Required(ErrorMessage = "Destination is required.")]
        public double CarPoolDestinationLat { get; set; }
		[Required(ErrorMessage = "Destination is required.")]
        public double CarPoolDestinationLon { get; set; }
		[Required(ErrorMessage = "At least one day must be selected.")]
		[Range(1,127, ErrorMessage = "At least one day must be selected.")]
        [Display(Name = "Active days")]
        public int CarPoolDaysAvailable { get; set; }
		[Required(ErrorMessage = "Number of seats available is required.")]	
        [Display(Name = "Seats available")]
		[RegularExpression(@"([1-9])|(10)", ErrorMessage = "Please enter a number between 1 and 10.")]
        public int CarPoolSeats { get; set; }
        [Display(Name = "Notes")]
        public string CarPoolNotes { get; set; }
        [Display(Name = "Creation Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime CarPoolCDate { get; set; }

        public int CarPoolHostUserId { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        [Display(Name = "Name")]
        public string CarPoolHostUserName { get; set; }
        [Required(ErrorMessage = "Surname is required.")]
        [Display(Name = "Surname")]
        public string CarPoolHostUserSurname { get; set; }
        [Display(Name = "Phone number")]
        [Phone(ErrorMessage = "This is not a valid phone number.")]
        public string CarPoolHostUserPhone { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "This is not a valid email address.")]
        [Display(Name = "Email address")]
        public string CarPoolHostUserEmailAddress { get; set; }
	}
}
