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
    public class PropertyDataController : ApiController
    {
        //this allows us to connect to the Mysql db 
        private TmDbContext Tms = new TmDbContext();

        //this will access the Properties table
        /// <summary>
        /// returns a list of Properties
        /// </summary>
        /// <example> Get api/Propertydata/listProperties</example>
        /// <returns>
        /// a list of Properties
        /// </returns>
        [HttpGet]
        [Route("api/Propertydata/listProperties")]
        public IEnumerable<ListPropertyData> ListProperties()
        {
            List<Property> Properties = Tms.Properties.ToList();
            List<ListPropertyData> PropertyDataList = new List<ListPropertyData>();
            Properties.ForEach(p => PropertyDataList.Add(new ListPropertyData()
            {
                PropertyId = p.PropertyId,
                PropertyName = p.PropertyName,
                
            }));
            return PropertyDataList;
           
        }

        //this will access the Properties table
        /// <summary>
        /// returns a single Property's data
        /// </summary>
        /// <example> Get api/Propertydata/findProperty/23</example>
        /// <returns>
        /// a Properties
        /// </returns>
        [ResponseType(typeof(Property))]
        [HttpGet]
        [Route("api/Propertydata/findProperty/{id}")]
        public IHttpActionResult FindProperty(int id)
        {
            Property Property = Tms.Properties.Find(id);
            if (Property == null)
            {
                return NotFound();
            }
            else
            {
                
                PropertyData PropertyData = new PropertyData()
                {
                    PropertyId = Property.PropertyId,
                    PropertyName = Property.PropertyName,
                    PropertyAddress = Property.PropertyAddress,
                    PropertyFloors = Property.PropertyFloors,
                    PropertyConstructed = Property.PropertyConstructed,
                    LandlordId = Property.LandlordId,
                    LandlordName = Property.Landlord.LandlordName,
                    LandlordPhone = Property.Landlord.LandlordPhone,
                    TenantsCount = Property.Leases.Count(),


                };

                return Ok(PropertyData);
            }

        }
        ///<summary>
        /// add a new property to the system
        /// </summary>
        /// <param name="property">json data of property</param>
        /// <returns>
        /// HEADER: 201 (Created)
        /// OR
        /// HEADER: 400 (Bad Request)
        /// </returns>
        [ResponseType(typeof(Property))]
        [HttpPost]
        [Route("api/propertydata/addproperty")]
        public IHttpActionResult AddProperty(Property property)
        {
            //check if there is some errors
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Tms.Properties.Add(property);
            Tms.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = property.PropertyId }, property);
        }

        ///<summary>
        /// update a property to the system
        /// </summary>
        /// <param name="peoperty">json data of property</param>
        /// <returns>
        /// HEADER: 201 (Created)
        /// OR
        /// HEADER: 400 (Bad Request)
        /// </returns>
        [ResponseType(typeof(Property))]
        [HttpPost]
        [Route("api/propertydata/updateproperty")]
        public IHttpActionResult UpdateProperty(int id, Property property)
        {
            //check if there is some errors and id matches the propertyId
            if (!ModelState.IsValid || property.PropertyId != id)
            {
                return BadRequest();
            }

            Tms.Entry(property).State = EntityState.Modified;
            try
            {
                Tms.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PropertyExits(id))
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
        /// method to check if a property exists
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool PropertyExits(int id)
        {
            return Tms.Properties.Count(p => p.PropertyId == id) > 0;
        }
    }
}
