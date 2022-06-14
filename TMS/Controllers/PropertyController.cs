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
    public class PropertyController : Controller
    {

        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static PropertyController()
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                AllowAutoRedirect = false,
                UseCookies = false,
            };
            client = new HttpClient(handler);
            client.BaseAddress = new Uri("https://localhost:44327/api/");
        }

        // GET: list Property
        public ActionResult Index()
        {

            PropertyAndLandlord ViewModel = new PropertyAndLandlord();

            //url to get a tenant  information
            string url = "propertyData/listProperties";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<Property> Properties = response.Content.ReadAsAsync<IEnumerable<Property>>().Result;
            ViewModel.Property = Properties;

            //url to get list of leases
            url = "Landlorddata/listLandlords";
            response = client.GetAsync(url).Result;
            IEnumerable<Landlord> Landlords = response.Content.ReadAsAsync<IEnumerable<Landlord>>().Result;
            ViewModel.ListLandlordData = Landlords;

            // pass the  tenants to /view.cshtml
            return View(ViewModel);
        }
     

        // POST: property/add
        [HttpPost]
        [Route("/property/add")]
        public ActionResult Add(Property property)
        {
            
            //add a new property into our system using the API
            string url = "propertydata/addproperty";

            string jsonpayload = jss.Serialize(property);

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
        // GET: single property
        [Route("/property/view/{id}")]

        public ActionResult View(int id)
        {
            PropertyDetails ViewModel = new PropertyDetails();

            //url to get a tenant  information
            string url = "propertyData/findproperty/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            PropertyData Property = response.Content.ReadAsAsync<PropertyData>().Result;
            ViewModel.Property = Property;

            // all leases for this tenant

            //url to get list of leases
            url = "propertydata/ListTenantsForProperty/" + id;
            response = client.GetAsync(url).Result;
            IEnumerable<LeaseDto> PropertyLeases = response.Content.ReadAsAsync<IEnumerable<LeaseDto>>().Result;
            ViewModel.PropertyLease = PropertyLeases;

            // pass the  tenants to /view.cshtml
            return View(ViewModel);
        }

        // GET: edit a property
        [Route("/property/edit/{id}")]
        public ActionResult Edit(int id)
        {
            PropertyDetails ViewModel = new PropertyDetails();

            //the existing tenant information
            string url = "propertyData/findproperty/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            PropertyData Property = response.Content.ReadAsAsync<PropertyData>().Result;
            ViewModel.Property = Property;

            //url to get list of leases
            url = "Landlorddata/listLandlords";
            response = client.GetAsync(url).Result;
            IEnumerable<Landlord> Landlords = response.Content.ReadAsAsync<IEnumerable<Landlord>>().Result;
            ViewModel.ListLandlordData = Landlords;

            return View(ViewModel);
        }

        // POST: Property/Update/2
        [HttpPost]
        public ActionResult Update(int id, Property property)
        {

            string url = "propertydata/updateproperty/" + id;
            string jsonpayload = jss.Serialize(property);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            return RedirectToAction("/view/" + id);
            
        }
        // GET: Property/Delete/5
        [Route("/property/delete/{id}")]
        public ActionResult DeleteConfirm(int id)
        {
            string url = "propertydata/findproperty/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            Property property = response.Content.ReadAsAsync<Property>().Result;
            return View(property);
        }

        // POST: property/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "propertydata/deleteproperty/" + id;
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

    }
}