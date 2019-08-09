using System;
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
                identifierNo=sfr.identifierNo,
                transactionType=sfr.transactionType,
                otherFFPNo=sfr.otherFFPNo,
                otherFFPScheme=sfr.otherFFPScheme,
                firstName=sfr.firstName,
                lastName=sfr.lastName,
                partnerTransactionNo=sfr.partnerTransactionNo,
                bookingDate=sfr.bookingDate,
                departureDate=sfr.departureDate,
                origin=sfr.origin,
                destination=sfr.destination,
                bookingClass=sfr.bookingClass,
                cabinClass=sfr.cabinClass,
                marketingFlightNo=sfr.marketingFlightNo,
                marketingAirline=sfr.marketingAirline,
                operatingFlightNo=sfr.operatingFlightNo,
                operatingAirline=sfr.operatingAirline,
                ticketNo=sfr.ticketNo,
                externalPaxID=sfr.externalPaxID,
                couponNo=sfr.couponNo,
                pnrNo=sfr.pnrNo,
                distance=sfr.distance,
                baseFare=sfr.baseFare,
                discountBase=sfr.discountBase,
                customerType=sfr.customerType,
                promotionCode=sfr.promotionCode,
                ticketCurrency=sfr.ticketCurrency,
                targetCurrency=sfr.targetCurrency,
                exchangeRate=sfr.exchangeRate,
                fareBasis=sfr.fareBasis

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
