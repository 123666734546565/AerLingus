using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AerLingus.Helpers;
using AerLingus.Models;

namespace AerLingus.Controllers.Api
{
    public class JourneyApiController : ApiController
    {
        AerLingus_databaseEntities entities;

        public JourneyApiController()
        {
            entities = new AerLingus_databaseEntities();
        }

        //[Route("api/JourneysApi/Search")]
        //public List<Journey> GetSearchedJourneys(SearchJourney search)
        //{

        //}

        [HttpPost]
        [Route("api/JourneyApi/AddJourney")]
        public async Task<HttpResponseMessage> AddJourneyAsync([FromBody] Journey j)
        {
            if (j.TicketNo != string.Empty)
            {
                if (entities.Journeys.Any(b => b.TicketNo == j.TicketNo))
                    return Request.CreateResponse(HttpStatusCode.Conflict);
                else
                {
                    entities.Journeys.Add(j);
                    await entities.SaveChangesAsync();

                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
            else return Request.CreateResponse(HttpStatusCode.Conflict);
        }
    }
}
