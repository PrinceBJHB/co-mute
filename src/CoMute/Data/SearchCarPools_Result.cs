//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CoMute.Web.Data
{
    using System;
    
    public partial class SearchCarPools_Result
    {
        public int Id { get; set; }
        public System.TimeSpan Depart { get; set; }
        public System.TimeSpan Arrive { get; set; }
        public string Origin { get; set; }
        public Nullable<double> OriginLat { get; set; }
        public Nullable<double> OriginLon { get; set; }
        public string Destination { get; set; }
        public Nullable<double> DestinationLat { get; set; }
        public Nullable<double> DestinationLon { get; set; }
        public int DaysAvailable { get; set; }
        public int Seats { get; set; }
        public string Notes { get; set; }
        public System.DateTime CDate { get; set; }
        public int HostUserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int DaysWithSeats { get; set; }
    }
}
