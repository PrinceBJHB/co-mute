using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoMute.Web.Models
{
	public class JoinedCarPool
	{
        [Display(Name = "Days joined")]
		public int DaysAvailable { get; set; }
        [Display(Name = "Join date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd:MMM:yyyy}")]
		public DateTime CDate { get; set; }

        [Display(Name = "Car pool ID")]
		public int CarPoolId { get; set; }
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh\\:mm}")]
        [Display(Name = "Departure time")]
        public TimeSpan CarPoolDepart { get; set; }
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh\\:mm}")]
        [Display(Name = "Arrival time")]
        public TimeSpan CarPoolArrive { get; set; }
        [Display(Name = "Origin")]
        public string CarPoolOrigin { get; set; }
        public double CarPoolOriginLat { get; set; }
        public double CarPoolOriginLon { get; set; }
        [Display(Name = "Destination")]
        public string CarPoolDestination { get; set; }
        public double CarPoolDestinationLat { get; set; }
        public double CarPoolDestinationLon { get; set; }
        [Display(Name = "Active days")]
        public int CarPoolDaysAvailable { get; set; }
        [Display(Name = "Seats available")]
        public int CarPoolSeats { get; set; }
        [Display(Name = "Notes")]
        public string CarPoolNotes { get; set; }
        [Display(Name = "Creation date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime CarPoolCDate { get; set; }



        public int CarPoolHostUserId { get; set; }
        [Display(Name = "Host's name")]
        public string CarPoolHostUserName { get; set; }
        [Display(Name = "Host's surname")]
        public string CarPoolHostUserSurname { get; set; }
        [Display(Name = "Host's phone number")]
        public string CarPoolHostUserPhone { get; set; }
        [Display(Name = "Host's email address")]
        public string CarPoolHostUserEmailAddress { get; set; }
	}
}
