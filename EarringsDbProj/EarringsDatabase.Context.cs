﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EarringsDbProj
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class EarringsDatabaseEntities : DbContext
    {
        public EarringsDatabaseEntities()
            : base("name=EarringsDatabaseEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<ConfigurationParameters> ConfigurationParameters { get; set; }
        public DbSet<UsersCredentials> UsersCredentials { get; set; }
    }
}
