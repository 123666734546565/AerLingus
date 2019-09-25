using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
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


        [HttpPost]
        [Route("api/JourneyApi/AddJourney")]
        public async Task<HttpResponseMessage> AddJourneyAsync([FromBody] Journey j)
        {
            try
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
            
                
            
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
            
        
          
        [Route("api/JourneyApi/SearchApi")]
        public List<Journey> GetSearchedJourneysApi()
        {
            string identifierNo = HttpContext.Current.Request.QueryString["IdentifierNo"] != null ? HttpContext.Current.Request.QueryString["IdentifierNo"] : null;
            string firstName = HttpContext.Current.Request.QueryString["FirstName"] != null ? HttpContext.Current.Request.QueryString["FirstName"] : null;
            string lastName = HttpContext.Current.Request.QueryString["LastName"] != null ? HttpContext.Current.Request.QueryString["LastName"] : null;
            string ticketNo = HttpContext.Current.Request.QueryString["TicketNo"] != null ? HttpContext.Current.Request.QueryString["TicketNo"] : null;

            if (identifierNo == null && firstName == null &&
                lastName == null && ticketNo == null)
                return default(List<Journey>);

            var searchedJourneys = entities.Journeys.Where(j =>
                                                (identifierNo != null ? j.IdentifierNo.StartsWith(identifierNo) : j.IdentifierNo == j.IdentifierNo) &&
                                                (firstName != null ? j.FirstName.StartsWith(firstName) : j.FirstName == j.FirstName) &&
                                                (lastName != null ? j.LastName.StartsWith(lastName) : j.LastName == j.LastName) &&
                                                (ticketNo != null ? j.TicketNo.StartsWith(ticketNo) : j.TicketNo == j.TicketNo)).ToList();

            return searchedJourneys;
        }

        //[HttpPost]
        //[Route("api/JourneyApi/AddJourney")]
        //public async Task<HttpResponseMessage> AddJourneyAsync([FromBody] Journey j)
        //{
        //    if (j.TicketNo != null)
        //    {
        //        if (entities.Journeys.Any(b => b.TicketNo == j.TicketNo))
        //            return Request.CreateResponse(HttpStatusCode.Conflict);
        //        else
        //        {
        //            entities.Journeys.Add(j);
        //            await entities.SaveChangesAsync();

        //            return Request.CreateResponse(HttpStatusCode.OK);
        //        }
        //    }
        //    else return Request.CreateResponse(HttpStatusCode.NotAcceptable);
        //}

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
            catch (Exception)
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


    }
}
