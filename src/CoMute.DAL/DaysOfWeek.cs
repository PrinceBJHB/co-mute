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
    
    public partial class DaysOfWeek
    {
        public DaysOfWeek()
        {
            this.CarPoolDays = new HashSet<CarPoolDay>();
        }
    
        public int DayOfWeekID { get; set; }
        public string nameOfDay { get; set; }
    
        public virtual ICollection<CarPoolDay> CarPoolDays { get; set; }
    }
}