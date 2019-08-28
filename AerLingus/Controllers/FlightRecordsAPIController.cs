using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AerLingus.Models;

namespace AerLingus.Controllers
{
    public class FlightRecordsAPIController : ApiController
    {
        AerLingus_databaseEntities entities = new AerLingus_databaseEntities();

        // GET: api/FlightRecordsAPI
        public HttpResponseMessage Get()
        {
            if (entities.Flight_Records.ToList() != null)
            {
                return Request.CreateResponse<IEnumerable<Flight_Records>>(HttpStatusCode.OK, entities.Flight_Records.ToList());
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
            }
        }

        // GET: api/FlightRecordsAPI/5
        public string Get(int id)
        {
            return "value";
        }
        
        [System.Web.Http.HttpPost]
        [Route("api/FlightRecordsAPI/AddFlightRecord")]
        // POST: api/FlightRecordsAPI/AddFlightRecord
        public async Task<HttpResponseMessage> AddFlightRecord([FromBody] Flight_Records sfr)
        {
            entities.Flight_Records.Add(sfr);
            await entities.SaveChangesAsync();
            return Request.CreateResponse(HttpStatusCode.OK);

        }

        // PUT: api/FlightRecordsAPI/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/FlightRecordsAPI/5
        public void Delete(int id)
        {
        }
    }
}
