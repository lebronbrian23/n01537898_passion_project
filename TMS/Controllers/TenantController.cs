using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using TMS.Models;
using System.Web.Script.Serialization;
using TMS.Models.ViewModels;
using System.Diagnostics;

namespace TMS.Controllers
{
    public class TenantController : Controller
    {
        /*
         *ListPropertiesForTenant

            6:00 AM
            ListPropertiesForLandlord

            6:01 AM
            ListLeasesForTenant

            6:01 AM
            AddLease(int tenantid, int propertyid)

            6:01 AM
            RemoveLease(int tenantid, int propertyid) 
         */
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static TenantController()
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                AllowAutoRedirect = false,
                UseCookies = false,
            };
            client = new HttpClient(handler);
            client.BaseAddress = new Uri("https://localhost:44327/api/");
        }

        // GET: list Tenant

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


        
        // GET: single Tenant
        [Route("/tenant/view/{id}")]

        public ActionResult View(int id)
        {

            //connect to data access layer
            Tenant TenantData = new Tenant();

            //url to get a tenant
            string url = "tenantData/findtenant/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            TenantData = response.Content.ReadAsAsync<Tenant>().Result;

            // pass the  tenants to /view.cshtml
            return View(TenantData);
        }
        
        // GET: edit a Tenant
        [Route("/tenant/edit/{id}")]
        public ActionResult Edit(int id)
        {
            TenantDetails ViewModel = new TenantDetails();

            //the existing tenant information
            string url = "tenantData/findtenant/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            TenantData Tenant = response.Content.ReadAsAsync<TenantData>().Result;
            ViewModel.Tenant = Tenant;

            // all properties to choose from when updating this tenant

            //url to get list of properties
            url = "Propertydata/listProperties";
            response = client.GetAsync(url).Result;
            IEnumerable<Property> ListPropertyData = response.Content.ReadAsAsync<IEnumerable<Property>>().Result;
            ViewModel.ListPropertyData = ListPropertyData;


            return View(ViewModel);
        }

        // POST: Tenant/Update/2
        [HttpPost]
        public ActionResult Update(int id, Tenant tenant)
        {

            string url = "tenantdata/updatetenant/" + id;
            string jsonpayload = jss.Serialize(tenant);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("/view/" + id);
            }
            else
            {
                return RedirectToAction("/edit/" + id);

            }
        }

     
        // POST: tenant/add
        [HttpPost]
        public ActionResult Add(Tenant tenant)
        {
            //add a new teneat into our system using the API
            string url = "tenantdata/addtenant";


            string jsonpayload = jss.Serialize(tenant);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }


        }

    }
}