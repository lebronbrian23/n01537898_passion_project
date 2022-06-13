using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMS.Models.ViewModels
{
    public class PropertyDetails
    {
        public PropertyData Property { get; set; }
        public IEnumerable<Lease> Leases { get; set; }
    }
}