using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoMute.Web.Models
{
    public class CarPool// this class contains the CarPool Functionality properties
    {

        public string Email { get; set; }// inique identifier for owner 
        public string DepartTime { get; set; }
        public string ArriveTime { get; set; }
        public string Origin { get; set; }
        public string DaysAvailable { get; set; }
        public string Destination { get; set; }
        public string  SeatsAvailable { get; set; }
        public string Notes { get; set; }
        public string DateCreated { get; set; }//date pool record  was created
     

       
    }
}
