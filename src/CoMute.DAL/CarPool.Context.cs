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
    
        public virtual DbSet<CarPool> CarPools { get; set; }
        public virtual DbSet<DayOfWeek> DayOfWeeks { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}
