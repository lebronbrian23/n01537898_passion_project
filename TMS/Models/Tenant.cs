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
       
        public virtual ICollection<Lease> Properties { get; set; }
    }  
    //Data transfer Object or DTO for the tenant
    public class TenantData
    {
        public int TenantId { get; set; }
        public string TenantName { get; set; }
        public string TenantEmail { get; set; }
        public string TenantPhone { get; set; }
        public string TenantEmergencyContact { get; set; }
        public DateTime TenantJoined { get; set; }

      
    }
    //Data transfer Object or DTO for the tenant list
    public class ListTenantData
    {
        public int TenantId { get; set; }
        public string TenantName { get; set; }

    }
  
}