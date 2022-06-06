using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using TMS.Models;
using TMS.Models.ViewModels;
using System.Web.Script.Serialization;
using System.Diagnostics;


namespace TMS.Controllers
{
    public class HomeController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static HomeController()
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                AllowAutoRedirect = false,
                UseCookies = false,
            };
            client = new HttpClient(handler);
            client.BaseAddress = new Uri("https://localhost:44327/api/");
        }
        // GET: Tenant

        public ActionResult Index()
        {
            TenantAndProperty TenantAndPropertyList = new TenantAndProperty();

            //url to get list of tenants
            string url = "tenantData/listtenants";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<Tenant> ListTenantData = response.Content.ReadAsAsync<IEnumerable<Tenant>>().Result;
            TenantAndPropertyList.ListTenantData = ListTenantData;


            //url to get list of properties
            url = "Propertydata/listProperties";
            response = client.GetAsync(url).Result;
            IEnumerable<Property> ListPropertyData = response.Content.ReadAsAsync<IEnumerable<Property>>().Result;
            TenantAndPropertyList.ListPropertyData = ListPropertyData;


            // pass the list of tenants and list of proporties to /index.cshtml
            return View(TenantAndPropertyList);
        }

        

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}