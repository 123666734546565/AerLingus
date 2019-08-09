﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AerLingus.Models;

namespace AerLingus.Controllers
{
    public class FlightRecordsAPIController : ApiController
    {
        AerLingusDatabaseEntities entities = new AerLingusDatabaseEntities();

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
        public HttpResponseMessage AddFlightRecord([FromBody] Flight_Records sfr)
        {
            entities.Flight_Records.Add(new Flight_Records()
            {
                //PutnikID = putnik.PutnikID,
                //Ime = putnik.Ime,
                //LetID = putnik.LetID

            });
            entities.SaveChanges();
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
