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

            //url to get list of properties
            string url = "propertyData/listProperties";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<ListPropertyData> ListPropertyData = response.Content.ReadAsAsync<IEnumerable<ListPropertyData>>().Result;

            // pass the list of proporties to /index.cshtml
            return View(ListPropertyData);
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
            ///url = "tenantdata/ListTenantsForProperty/" + id;
            //response = client.GetAsync(url).Result;
            //IEnumerable<LeaseDto> Leases = response.Content.ReadAsAsync<IEnumerable<LeaseDto>>().Result;
            //ViewModel.Leases = Leases;

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