using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMS.Models.ViewModels
{
    public class TenantAndProperty
    {
        public int TenantId { get; set; }
        public IEnumerable<Property> ListPropertyData { get;set;}
    }
}