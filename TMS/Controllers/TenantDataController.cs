using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TMS.Models;
using System.Web.Http.Description;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;


namespace TMS.Controllers
{
    public class TenantDataController : ApiController
    {
        ///this allows us to connect to the Mysql db 
        private TmDbContext Tms = new TmDbContext();

        ///this will access the tenants table
        /// <summary>
        /// returns a list of tenants
        /// </summary>
        /// <example> Get api/tenantdata/listtenants</example>
        /// <returns>
        /// a list of tenants
        /// </returns>
        [HttpGet]
        [ResponseType(typeof(Tenant))]
        public IHttpActionResult ListTenants()
        {
            List<Tenant> Tenants = Tms.Tenants.ToList();
            List<ListTenantData> TenantDataList = new List<ListTenantData>();
            Tenants.ForEach(t => TenantDataList.Add(new ListTenantData()
            {
                TenantId = t.TenantId,
                TenantName = t.TenantName,
                TenantRoom = t.TenantRoom

            }));
            return Ok(TenantDataList);

        }
       

        ///this will access the tenants table
        /// <summary>
        /// returns a single tenant's data
        /// </summary>
        /// <example> Get api/tenantdata/findtenant/23</example>
        /// <returns>
        /// a tenants
        /// </returns>
        [ResponseType(typeof(Tenant))]
        [HttpGet]
        [Route("api/tenantdata/findtenant/{id}")]
        public IHttpActionResult FindTenant(int id)
        {
            Tenant Tenant = Tms.Tenants.Find(id);
            if (Tenant == null)
            {
                return NotFound();
            }
            else
            {
                TenantData tenantData = new TenantData()
                {
                    TenantId = Tenant.TenantId,
                    TenantName = Tenant.TenantName,
                    TenantEmail = Tenant.TenantEmail,
                    TenantPhone = Tenant.TenantPhone,
                    TenantEmergencyContact = Tenant.TenantEmergencyContact,
                    TenantJoined = Tenant.TenantJoined,
                    TenantFloor = Tenant.TenantFloor,
                    TenantRoom = Tenant.TenantRoom,
                };

                return Ok(tenantData);
            }

        }

        ///this will access the tenants table
        /// <summary>
        /// update a single tenant's data
        /// </summary>
        /// <example> Post api/tenantdata/updatetenant/23</example>
        /// <returns>
        /// a tenants
        /// </returns>
        /// [Route("api/tenantdata/updatetenant/{id}")]
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateTenant(int id , Tenant tenant)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tenant.TenantId)
            {

                return BadRequest();
            }

            Tms.Entry(tenant).State = EntityState.Modified;

            try
            {
                Tms.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TenantExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return StatusCode(HttpStatusCode.OK);

        }

        ///this will access the tenants table
        /// <summary>
        /// add a single tenant's data
        /// </summary>
        /// <example> Post api/tenantdata/addtenant</example>
        /// <returns>
        /// a tenants
        /// </returns>
        ///[Route("api/tenantdata/addtenant")]

        [ResponseType(typeof(Tenant))]
        [HttpPost]
        public IHttpActionResult AddTenant( Tenant tenant)
        {
            //check if there is some errors
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Tms.Tenants.Add(tenant);
            Tms.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = tenant.TenantId }, tenant);
        }

        ///this will access the tenants table
        /// <summary>
        /// delete a single tenant's data
        /// </summary>
        /// <example> Get api/tenantdata/deletetenant/23</example>
        /// <returns>
        /// a tenants
        /// </returns>
        ///[Route("api/tenantdata/deletetenant/{id}")]

        [ResponseType(typeof(Tenant))]
        [HttpPost]
        public IHttpActionResult DeleteTenant(int id)
        {
            Debug.WriteLine("am here");
            Debug.WriteLine(id);
            Tenant tenant = Tms.Tenants.Find(id);

            if(tenant == null)
            {
                return NotFound();
            }
            //check if a tenant has a lease
            Lease lease = Tms.Leases.Where(l => l.TenantId == id).FirstOrDefault();
            if (lease != null)
            {
                Tms.Leases.Remove(lease);
            }

            Tms.Tenants.Remove(tenant);
            Tms.SaveChanges();
            return Ok();
        }

        /// <summary>
        /// method to check if a tenant exists
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool TenantExists(int id)
        {
            return Tms.Tenants.Count(t => t.TenantId == id) > 0;
        }
    }
}
