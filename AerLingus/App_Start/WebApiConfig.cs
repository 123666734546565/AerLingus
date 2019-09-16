using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace AerLingus
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            //config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

            // Web API routes
            config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "FlightRecordsApi",
            //    routeTemplate: "api/FlightRecordsApi/Search/{identifierNo}/{otherFFPNo}/{firstName}/{lastName}/{departureDate}/{Origin}/{destination}/{bookingClass}/{operatingAirline}/{ticketNo}/{externalPaxID}/{pnrNo}",
            //    defaults: new
            //    {
            //        identifierNo = RouteParameter.Optional,
            //        otherFFPNo = RouteParameter.Optional,
            //        firstName = RouteParameter.Optional,
            //        lastName = RouteParameter.Optional,
            //        departureDate = RouteParameter.Optional,
            //        Origin = RouteParameter.Optional,
            //        destination = RouteParameter.Optional,
            //        bookingClass = RouteParameter.Optional,
            //        operatingAirline = RouteParameter.Optional,
            //        ticketNo = RouteParameter.Optional,
            //        externalPaxID = RouteParameter.Optional,
            //        pnrNo = RouteParameter.Optional
            //    }
            //);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
