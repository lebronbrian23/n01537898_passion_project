using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace TMS.Models
{
  
    public class Landlord
    {
        [Key]
        public int LandlordId { get; set; }
        public string LandlordName { get; set; }
        public DateTime? LandlordDOB { get; set; }
        public string LandlordPhone { get; set; }
        public string LandlordEmail { get; set; }

        public ICollection<Property> Properties { get; set; }
    }
    
    public class LandlordData
    {
        public int LandlordId { get; set; }
        public string LandlordName { get; set; }
        public DateTime? LandlordDOB { get; set; }
        public string LandlordPhone { get; set; }
        public string LandlordEmail { get; set; }

    }
}