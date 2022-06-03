using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Infrastructure;
using MySql.Data.MySqlClient; //MySql.Data should be installed if its missing

namespace TMS.Models
{
    public class TmDbContext : DbContext
    {
        public TmDbContext():base()
        {

        }
    
        public DbSet<Landlord> Landlords { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<Lease> Leases { get; set; }
    
    
     }
}