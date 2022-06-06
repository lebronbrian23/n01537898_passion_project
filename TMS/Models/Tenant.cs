using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TMS.Models
{
    public class Tenant
    {
        [Key]
        public int TenantId { get; set; }
        public string TenantName { get; set; }
        public string TenantEmail { get; set; }
        public string TenantPhone { get; set; }
        public string TenantEmergencyContact { get; set; }
        public DateTime TenantJoined { get; set; }
        public int TenantRoom { get; set; }
        public int TenantFloor { get; set; }
        public virtual ICollection<Lease> Leases { get; set; }
    } 
    public class TenantData
    {
        public int TenantId { get; set; }
        public string TenantName { get; set; }
        public string TenantEmail { get; set; }
        public string TenantPhone { get; set; }
        public string TenantEmergencyContact { get; set; }
        public DateTime TenantJoined { get; set; }
        public int TenantFloor { get; set; }
        public int TenantRoom { get; set; }
        public virtual ICollection<Lease> Leases { get; set; }

    }
    public class ListTenantData
    {
        public int TenantId { get; set; }
        public string TenantName { get; set; }
        public int TenantRoom { get; set; }

    }
  
}