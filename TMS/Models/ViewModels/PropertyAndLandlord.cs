using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMS.Models.ViewModels
{
    public class PropertyAndLandlord
    {
        public IEnumerable<Property> Property { get;set;}
        public IEnumerable<Landlord> ListLandlordData { get;set;}
    }
}