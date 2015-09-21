using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CoMute.Web.Models
{
    public class Car_pool
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public DateTime DepartureTime { get; set; }
        [Required]
        public DateTime ArrivalTime { get; set; }
        [Required]
        public string Origin { get; set; }
        [Required]
        public string DayAvailable { get; set; }
        [Required]
        public string Destination { get; set; }
        [Required]
        public string Owner { get; set; }
        public string Notes { get; set; }
        public User users { get; set; }

    }
}