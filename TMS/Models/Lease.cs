using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TMS.Models
{
    public class Lease
    {
        [Key]
        public int LeaseId { get; set; }
       
        [ForeignKey("Tenant")]
        public int TenantId { get; set; }
        public virtual Tenant Tenant { get; set; }
        //public Tenant Tenant { get; set; }

        [ForeignKey("Property")]
        public int PropertyId { get; set; }
        public virtual Property Property { get; set; }

        public int Room { get; set; }

        public int Floor { get; set; }

        public DateTime LeaseEndDate { get; set; }
    }

    public class LeaseDto
    {
        public int LeaseId { get; set; }
        public int TenantId { get; set; }
        public int PropertyId { get; set; }
        public string PropertyName { get; set; }
        public string PropertyAddress { get; set; }
        public DateTime LeaseEndDate { get; set; }
        public int Room { get; set; }
        public int Floor { get; set; }

    }
}