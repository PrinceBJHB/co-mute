//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CoMute.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class CarPool
    {
        public CarPool()
        {
            this.DayOfWeeks = new HashSet<DayOfWeek>();
        }
    
        public int CarPoolID { get; set; }
        public int UserID { get; set; }
        public string departureTime { get; set; }
        public string expectedArrivalTime { get; set; }
        public string origin { get; set; }
        public string destination { get; set; }
        public int seatsAvailable { get; set; }
        public string notes { get; set; }
        public System.DateTime created { get; set; }
    
        public virtual User User { get; set; }
        public virtual ICollection<DayOfWeek> DayOfWeeks { get; set; }
    }
}
