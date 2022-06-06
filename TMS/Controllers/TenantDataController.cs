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


namespace TMS.Controllers
{
    public class TenantDataController : ApiController
    {
        //this allows us to connect to the Mysql db 
        private TmDbContext Tms = new TmDbContext();

        //this will access the tenants table
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
        /*[HttpGet]
        [Route("api/tenantdata/listtenants")]
        public IEnumerable<ListTenantData> ListTenants()
        {
            List<Tenant> Tenants = Tms.Tenants.ToList();
            List<ListTenantData> TenantDataList = new List<ListTenantData>();
            Tenants.ForEach(t => TenantDataList.Add(new ListTenantData()
            {
                TenantId = t.TenantId,
                TenantName = t.TenantName,
                TenantRoom = t.TenantRoom
               
            }));
            return TenantDataList;
           
        }*/

        //this will access the tenants table
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
       
        //this will access the tenants table
        /// <summary>
        /// update a single tenant's data
        /// </summary>
        /// <example> Post api/tenantdata/updatetenant/23</example>
        /// <returns>
        /// a tenants
        /// </returns>
        [ResponseType(typeof(void))]
        [HttpGet]
        [Route("api/tenantdata/updatetenant/{id}")]
        public IHttpActionResult UpdateTenant(int id , Tenant tenant)
        {
            return StatusCode(HttpStatusCode.OK);
        }

        //this will access the tenants table
        /// <summary>
        /// add a single tenant's data
        /// </summary>
        /// <example> Post api/tenantdata/addtenant</example>
        /// <returns>
        /// a tenants
        /// </returns>
        [ResponseType(typeof(Tenant))]
        [HttpGet]
        [Route("api/tenantdata/addtenant")]
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

        //this will access the tenants table
        /// <summary>
        /// delete a single tenant's data
        /// </summary>
        /// <example> Get api/tenantdata/deletetenant/23</example>
        /// <returns>
        /// a tenants
        /// </returns>
        [ResponseType(typeof(Tenant))]
        [HttpGet]
        [Route("api/tenantdata/deletetenant/{id}")]
        public IHttpActionResult DeleteTenant(int id)
        {
            Tenant tenant = Tms.Tenants.Find(id);
            if(tenant == null)
            {
                return NotFound();
            }

            Lease lease = Tms.Leases.Where(l => l.TenantId == id).FirstOrDefault();
            if (lease == null)
            {
                return NotFound();
            }
            Tms.Leases.Remove(lease);
            
            Tms.Tenants.Remove(tenant);
            Tms.SaveChanges();
            return Ok();
        }
    }
}
