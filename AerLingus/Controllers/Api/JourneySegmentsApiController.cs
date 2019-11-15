using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
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


        [HttpPost]
        [Route("api/JourneySegmentsApi/AddJourneySegment")]
        public async Task<HttpResponseMessage> AddJourneySegmentAsync([FromBody] JourneySegment j)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (entities.JourneySegments.Any(b => b.TicketNo == j.TicketNo && b.couponNo ==j.couponNo))
                        return Request.CreateResponse(HttpStatusCode.Conflict);
                    else
                    {
                        entities.JourneySegments.Add(j);
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


    }
}
