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

            //url to get list of tenants
            string url = "tenantData/listtenants";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<ListTenantData> ListTenantData = response.Content.ReadAsAsync<IEnumerable<ListTenantData>>().Result;

            // pass the list of tenants and list of proporties to /index.cshtml
            return View(ListTenantData);
        }


        
        // GET: single Tenant
        [Route("/tenant/view/{id}")]

        public ActionResult View(int id)
        {
            TenantDetails ViewModel = new TenantDetails();

            //url to get a tenant  information
            string url = "tenantData/findtenant/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            TenantData Tenant= response.Content.ReadAsAsync<TenantData>().Result;
            ViewModel.Tenant = Tenant;

            // all leases for this tenant

            //url to get list of leases
            url = "tenantdata/ListPropertiesForTenant/" + id;
            response = client.GetAsync(url).Result;
            IEnumerable<LeaseDto> Leases= response.Content.ReadAsAsync<IEnumerable<LeaseDto>>().Result;
            ViewModel.Leases = Leases;

            // pass the  tenants to /view.cshtml
            return View(ViewModel);
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
        // GET: Tenant/Delete/5
        [Route("/tenant/delete/{id}")]
        public ActionResult DeleteConfirm(int id)
        {
            string url = "tenantdata/findtenant/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            Tenant tenant = response.Content.ReadAsAsync<Tenant>().Result;
            return View(tenant);
        }

        // POST: tenant/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "tenantdata/deletetenant/" + id;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("/view/" + id);
            }
        }

        // GET: tenant/newlease/5

        public ActionResult NewLease(int id)
        {
            TenantAndProperty TenantAndPropertyList = new TenantAndProperty();

            TenantAndPropertyList.TenantId = id;

          
            //url to get list of properties
            string url = "Propertydata/listProperties";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<Property> ListPropertyData = response.Content.ReadAsAsync<IEnumerable<Property>>().Result;
            TenantAndPropertyList.ListPropertyData = ListPropertyData;

            // pass the list of tenants and list of proporties to /index.cshtml
            return View(TenantAndPropertyList);
        }

        // POST: tenant/addlease/5
        [HttpPost]
        public ActionResult AddLease(int id ,Lease lease)
        {
            string url = "tenantdata/addlease";
            string jsonpayload = jss.Serialize(lease);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("/view/" + id);
            }
            else
            {
                return RedirectToAction("/newlease/" + id);
            }
        }

        // GET: tenant/Deletelease/5
        [HttpGet]
        //[Route("/tenant/deletelease/{id}?TenantInd={tenantId}")]

        public ActionResult DeleteLease(int id , int tenantId)
        {
            string url = "tenantdata/deletelease/" + id ;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            
            return RedirectToAction("/view/" + tenantId);
        }

    }
}