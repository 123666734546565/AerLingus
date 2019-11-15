using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AerLingus.Models;

namespace AerLingus.Controllers.Api
{
    public class JourneySegmentsApiController : ApiController
    {
        private AerLingus_databaseEntities entities;

        public JourneySegmentsApiController()
        {
            entities = new AerLingus_databaseEntities();
        }

        [HttpGet]
        public IEnumerable<JourneySegment> GetJourneySegments(string ticketNo) //ovo ID je ticketNo
        {
            if (!entities.JourneySegments.Any())
                return default(IEnumerable<JourneySegment>);

            return entities.JourneySegments.Where(js => js.TicketNo == ticketNo).AsEnumerable();
        }
    }
}
