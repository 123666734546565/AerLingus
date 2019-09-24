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

        //[Route("api/JourneysApi/Search")]
        //public List<Journey> GetSearchedJourneys(SearchJourney search)
        //{

        //}
    }
}
