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
        //[ResponseType(typeof(Tenant))]
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
                  
                };


                return Ok(tenantData);
            }

        }
        /// <summary>
        /// Gathers information about animals related to a particular keeper
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all animals in the database, including their associated species that match to a particular keeper id
        /// </returns>
        /// <param name="id">Keeper ID.</param>
        /// <example>
        /// GET: api/TenantData/ListPropertiesForTenant/1
        /// </example>
        [HttpGet]
        [ResponseType(typeof(LeaseDto))]
        public IHttpActionResult ListPropertiesForTenant(int id)
        {
            
            //all tenants that have properties which match with our ID
            //can't directly access the ticket's bookings
            //access the lease bridging table,
            //joining the tickets in
            List<Lease> TenantAndProperty = Tms.Leases.Where(l => l.TenantId == id).Include(l => l.Property).ToList();

            List<LeaseDto> LeaseDtos = new List<LeaseDto>();

            TenantAndProperty.ForEach(d => LeaseDtos.Add(new LeaseDto()
            {
                LeaseId = d.LeaseId,
                TenantId = d.Tenant.TenantId,
                PropertyId = d.Property.PropertyId,
                PropertyName = d.Property.PropertyName,
                PropertyAddress = d.Property.PropertyAddress,
                Floor = d.Floor,
                Room = d.Room,
                LeaseEndDate = d.LeaseEndDate
,
            }));


            return Ok(LeaseDtos);
        }

        ///this will access the leases table
        /// <summary>
        /// add a single lease
        /// </summary>
        /// <example> Post api/tenantdata/addlease</example>
        /// <returns>
        /// a lease
        /// </returns>
       // [Route("api/tenantdata/addlease")]

        [ResponseType(typeof(Lease))]
        [HttpPost]
        public IHttpActionResult AddLease( Lease lease)
        {

            //check if there is some errors
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Tms.Leases.Add(lease);
            Tms.SaveChanges();

           
            return CreatedAtRoute("DefaultApi", new { id = lease.LeaseId }, lease);
        }
        ///this will access the lease table
        /// <summary>
        /// delete a single tenant's lease
        /// </summary>
        /// <example> Get api/tenantdata/deletelease/23</example>
        /// <returns>
        /// a tenants
        /// </returns>
        ///[Route("api/tenantdata/deletelease/{id}")]

        [ResponseType(typeof(Lease))]
        [HttpPost]
        public IHttpActionResult DeleteLease(int id)
        {

            Lease lease = Tms.Leases.Find(id);

            if (lease == null)
            {
                return NotFound();
            }

            Tms.Leases.Remove(lease);
            Tms.SaveChanges();
            return Ok();
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
        public IHttpActionResult AddTenant(Tenant tenant)
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
            
            Tenant tenant = Tms.Tenants.Find(id);

            if(tenant == null)
            {
                return NotFound();
            }
            //check if a tenant has any lease
            var lease = Tms.Leases.Where(row => row.TenantId == id ).ToList();

            if (lease != null)
            {
               // Delete all the leases that belong to the tenant
                Tms.Leases.RemoveRange(lease);
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
