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
    public class LandlordController : Controller
    {

        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static LandlordController()
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                AllowAutoRedirect = false,
                UseCookies = false,
            };
            client = new HttpClient(handler);
            client.BaseAddress = new Uri("https://localhost:44327/api/");
        }

        
        // GET: edit a landlord data
        public ActionResult Index()
        {
            int id = 1; // there is only one landlord with id 1
            LandlordData LandlordData = new LandlordData();

            //the existing tenant information
            string url = "landlordData/findlandlord/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            LandlordData landlord = response.Content.ReadAsAsync<LandlordData>().Result;

            return View(landlord);
        }

        // POST: landlord/Update/1
        [HttpPost]
        [Route("/landlored/Update/{id}")]
        public ActionResult Update(int id, Landlord landlord)
        {

            string url = "landlorddata/updatelandlord/" + id;
            string jsonpayload = jss.Serialize(landlord);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            return RedirectToAction("/index");

        }


    }
}