using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TMS.Models
{
    public class Property
    {
        [Key]
        public int PropertyId { get; set; }
        public string PropertyName { get; set; }

        public string PropertyAddress { get; set; }

        public string PropertyFloors { get; set; }
        public DateTime? PropertyConstructed { get; set; }
        //property belongs to one landlord
        // landlord can have many properties
        [ForeignKey("Landlord")]
        public int LandlordId { get; set; }
        public virtual Landlord Landlord { get; set; }
       
        public virtual ICollection<Lease> Leases { get; set; }
    }
    
    public class PropertyData
    {
        public int PropertyId { get; set; }

        public string PropertyName { get; set; }
        public string PropertyAddress { get; set; }
        public string PropertyFloors { get; set; }
        public DateTime? PropertyConstructed { get; set; }
        public int LandlordId { get; set; }
        public virtual Landlord Landlord { get; set; }

        public string LandlordName{ get; set; }
        public string LandlordPhone { get; set; }

        public virtual ICollection<Lease> Leases { get; set; }

        public int TenantsCount { get; set; }


    } 
    //single property data
    public class ListPropertyData
    {
        public int PropertyId { get; set; }

        public string PropertyName { get; set; }
        

    }
}