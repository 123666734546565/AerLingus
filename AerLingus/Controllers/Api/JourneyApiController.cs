using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
    }
}
