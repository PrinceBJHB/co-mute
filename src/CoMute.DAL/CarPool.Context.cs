﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class CoMuteEntities : DbContext
    {
        public CoMuteEntities()
            : base("name=CoMuteEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<CarPoolDay> CarPoolDays { get; set; }
        public virtual DbSet<CarPool> CarPools { get; set; }
        public virtual DbSet<DaysOfWeek> DaysOfWeeks { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<UserCarPool> UserCarPools { get; set; }
        public virtual DbSet<User> Users { get; set; }
    
        public virtual ObjectResult<usp_CarPool_Create_Result> usp_CarPool_Create(Nullable<int> userID, Nullable<System.TimeSpan> departureTime, Nullable<System.TimeSpan> expectedArrivalTime, string origin, string destination, Nullable<int> seatsAvailable, string notes, string days)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            var departureTimeParameter = departureTime.HasValue ?
                new ObjectParameter("departureTime", departureTime) :
                new ObjectParameter("departureTime", typeof(System.TimeSpan));
    
            var expectedArrivalTimeParameter = expectedArrivalTime.HasValue ?
                new ObjectParameter("expectedArrivalTime", expectedArrivalTime) :
                new ObjectParameter("expectedArrivalTime", typeof(System.TimeSpan));
    
            var originParameter = origin != null ?
                new ObjectParameter("origin", origin) :
                new ObjectParameter("origin", typeof(string));
    
            var destinationParameter = destination != null ?
                new ObjectParameter("destination", destination) :
                new ObjectParameter("destination", typeof(string));
    
            var seatsAvailableParameter = seatsAvailable.HasValue ?
                new ObjectParameter("seatsAvailable", seatsAvailable) :
                new ObjectParameter("seatsAvailable", typeof(int));
    
            var notesParameter = notes != null ?
                new ObjectParameter("notes", notes) :
                new ObjectParameter("notes", typeof(string));
    
            var daysParameter = days != null ?
                new ObjectParameter("days", days) :
                new ObjectParameter("days", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<usp_CarPool_Create_Result>("usp_CarPool_Create", userIDParameter, departureTimeParameter, expectedArrivalTimeParameter, originParameter, destinationParameter, seatsAvailableParameter, notesParameter, daysParameter);
        }
    
        public virtual ObjectResult<usp_User_Login_Result> usp_User_Login(string emailAddress, string password)
        {
            var emailAddressParameter = emailAddress != null ?
                new ObjectParameter("emailAddress", emailAddress) :
                new ObjectParameter("emailAddress", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("password", password) :
                new ObjectParameter("password", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<usp_User_Login_Result>("usp_User_Login", emailAddressParameter, passwordParameter);
        }
    
        public virtual ObjectResult<usp_User_Register_Result> usp_User_Register(string firstName, string lastName, string emailAddress, string phoneNumber, string password)
        {
            var firstNameParameter = firstName != null ?
                new ObjectParameter("firstName", firstName) :
                new ObjectParameter("firstName", typeof(string));
    
            var lastNameParameter = lastName != null ?
                new ObjectParameter("lastName", lastName) :
                new ObjectParameter("lastName", typeof(string));
    
            var emailAddressParameter = emailAddress != null ?
                new ObjectParameter("emailAddress", emailAddress) :
                new ObjectParameter("emailAddress", typeof(string));
    
            var phoneNumberParameter = phoneNumber != null ?
                new ObjectParameter("phoneNumber", phoneNumber) :
                new ObjectParameter("phoneNumber", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("password", password) :
                new ObjectParameter("password", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<usp_User_Register_Result>("usp_User_Register", firstNameParameter, lastNameParameter, emailAddressParameter, phoneNumberParameter, passwordParameter);
        }
    }
}