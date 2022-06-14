using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMS.Models.ViewModels
{
    public class PropertyDetails
    {
        public PropertyData Property { get; set; }
        public IEnumerable<LeaseDto> PropertyLease { get; set; }
        public IEnumerable<Landlord> ListLandlordData { get; set; }


    }
}