using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using TMS.Models;
using System.Web.Script.Serialization;


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
        // GET: Tenant
        [Route("/home/view/{id}")]

        public ActionResult View(int id)
        {

            //connect to data access layer
            Tenant TenantData = new Tenant();

            //url to get a tenant
            string url = "tenantData/findtenant/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            TenantData = response.Content.ReadAsAsync<Tenant>().Result;

            // pass the list of tenants to /index.cshtml
            return View(TenantData);
        }

    }
}