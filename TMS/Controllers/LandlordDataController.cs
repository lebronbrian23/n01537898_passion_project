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
    public class LandlordDataController : ApiController
    {
        //this allows us to connect to the Mysql db 
        private TmDbContext Tms = new TmDbContext();

        //this will access the Landlords table
        /// <summary>
        /// returns a list of Landlords
        /// </summary>
        /// <example> Get api/Landlorddata/listLandlords</example>
        /// <returns>
        /// a list of Landlords
        /// </returns>
        [HttpGet]
        [Route("api/Landlorddata/listLandlords")]
        public IEnumerable<LandlordData> ListLandlords()
        {
            List<Landlord> Landlords = Tms.Landlords.ToList();
            List<LandlordData> LandlordDataList = new List<LandlordData>();
            Landlords.ForEach(l => LandlordDataList.Add(new LandlordData()
            {
                LandlordId = l.LandlordId,
                LandlordName = l.LandlordName,
                LandlordEmail = l.LandlordEmail,
                LandlordPhone = l.LandlordPhone,
                LandlordDOB = l.LandlordDOB,

            })); ;
            return LandlordDataList;
           
        }

        //this will access the Landlords table
        /// <summary>
        /// returns a single Landlord's data
        /// </summary>
        /// <example> Get api/Landlorddata/findLandlord/23</example>
        /// <returns>
        /// a Landlords
        /// </returns>
        [ResponseType(typeof(Landlord))]
        [HttpGet]
        [Route("api/Landlorddata/findLandlord/{id}")]
        public IHttpActionResult FindLandlord(int id)
        {
            Landlord Landlord = Tms.Landlords.Find(id);
            if (Landlord == null)
            {
                return NotFound();
            }
            else
            {
                Landlord LandlordData = new Landlord()
                {
                    LandlordId = Landlord.LandlordId,
                    LandlordName = Landlord.LandlordName,
                    LandlordEmail = Landlord.LandlordEmail,
                    LandlordPhone = Landlord.LandlordPhone,
                    LandlordDOB = Landlord.LandlordDOB,
            };

                return Ok(LandlordData);
            }

        }

        ///<summary>
        /// add a new landlord to the system
        /// </summary>
        /// <param name="landlord">json data of landlord</param>
        /// <returns>
        /// HEADER: 201 (Created)
        /// OR
        /// HEADER: 400 (Bad Request)
        /// </returns>
        [ResponseType(typeof(Landlord))]
        [HttpPost]
        [Route("api/landlorddata/addlandlord")]
        public IHttpActionResult AddLandlord(Landlord landlord)
        {
            //check if there is some errors
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Tms.Landlords.Add(landlord);
            Tms.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = landlord.LandlordId }, landlord);
        }

        ///<summary>
        /// update a landlord to the system
        /// </summary>
        /// <param name="landlord">json data of landlord</param>
        /// <returns>
        /// HEADER: 201 (Created)
        /// OR
        /// HEADER: 400 (Bad Request)
        /// </returns>
        [ResponseType(typeof(Landlord))]
        [HttpPost]
        //[Route("api/landlorddata/updatelandlord")]
        public IHttpActionResult UpdateLandlord(int id,Landlord landlord)
        {
            //check if there is some errors and id matches the landlordId
            if (!ModelState.IsValid || landlord.LandlordId != id)
            {
                return BadRequest();
            }

            Tms.Entry(landlord).State = EntityState.Modified;
            try
            {
                Tms.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LandlordExits(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return StatusCode(HttpStatusCode.NotFound);
        }

        /// <summary>
        /// method to check if a landlord exists
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool LandlordExits(int id)
        {
            return Tms.Landlords.Count(l => l.LandlordId == id)  > 0;
        }

    }
}
