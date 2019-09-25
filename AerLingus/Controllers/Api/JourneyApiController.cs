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

        [HttpGet]
        public IEnumerable<Journey> GetJourneys()
        {
            if (!entities.Journeys.Any())
                return default(IEnumerable<Journey>);

            return entities.Journeys;
        }

        [HttpGet]
        public IHttpActionResult GetJourney(int id)
        {
            var journey = entities.Journeys.SingleOrDefault(j => j.ID == id);

            if (journey == null)
                return NotFound();

            return Ok(journey);
        }

        [Route("api/JourneysApi/Search")]
        public List<Journey> GetSearchedJourneys(SearchJourney search)
        {
            var searchedJourneys = entities.Journeys.Where(j =>
                                                        (search.identifierNo != null ? j.IdentifierNo.StartsWith(search.identifierNo) : j.IdentifierNo == j.IdentifierNo) &&
                                                        (search.firstName != null ? j.FirstName.StartsWith(search.firstName) : j.FirstName == j.FirstName) &&
                                                        (search.lastName != null ? j.LastName.StartsWith(search.lastName) : j.LastName == j.LastName) &&
                                                        (search.ticketNo != null ? j.TicketNo.StartsWith(search.ticketNo) : search.ticketNo == search.ticketNo)).ToList();
            return searchedJourneys;
        }

            return searchedJourneys;

        }
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
            try
            {
                if (j.TicketNo != string.Empty)
                {
                    entities.Journeys.Add(j);
                    await entities.SaveChangesAsync();

                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else return Request.CreateResponse(HttpStatusCode.Conflict);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut]
        public HttpResponseMessage EditJourney(int id, Journey journey)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Request.CreateResponse(HttpStatusCode.BadRequest);

                var journeyInDatabase = entities.Journeys.SingleOrDefault(j => j.ID == id);

                if (journeyInDatabase == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound);

                journeyInDatabase.IdentifierNo = journey.IdentifierNo;
                journeyInDatabase.FirstName = journey.FirstName;
                journeyInDatabase.LastName = journey.LastName;
                journeyInDatabase.TicketNo = journey.TicketNo;

                if (!ModelState.IsValid)
                    return Request.CreateResponse(HttpStatusCode.BadRequest);

                entities.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch(Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        public HttpResponseMessage DeleteJourney(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Request.CreateResponse(HttpStatusCode.BadRequest);

                var journeyInDatabase = entities.Journeys.SingleOrDefault(j => j.ID == id);

                if (journeyInDatabase == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound);

                entities.Journeys.Remove(journeyInDatabase);
                entities.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
            else return Request.CreateResponse(HttpStatusCode.Conflict);
        }
          
    }
}
