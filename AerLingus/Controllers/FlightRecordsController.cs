using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.VisualBasic;
using AerLingus.Models;
using System.Data.Entity;
using System.IO;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using AerLingus.Controllers.Api;
using System.Web.Http;

namespace AerLingus.Controllers
{
    public class FlightRecordsController : Controller
    {
        private HttpClient client;
        private AerLingus_databaseEntities entities;


        public FlightRecordsController()
        {
            client = new HttpClient();
            entities = new AerLingus_databaseEntities();
        }

        public ActionResult Upload(HttpPostedFileBase file)
        {
            var fileName = Path.GetFileName(file.FileName);

            var path = Path.Combine(Server.MapPath("~/UploadedFiles"), fileName);

            file.SaveAs(path);

            FlightRecordsApiController flight = new FlightRecordsApiController()
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            var returnedStatusCode = flight.Upload(file).StatusCode;

            if (returnedStatusCode == System.Net.HttpStatusCode.OK)
                return RedirectToAction("Index", "Home");
            else
            {
                object errorMessage = null; ;

                if (returnedStatusCode == System.Net.HttpStatusCode.NotFound)
                    errorMessage = "ERROR 404: No file selected.";
                else if (returnedStatusCode == System.Net.HttpStatusCode.NoContent)
                    errorMessage = "ERROR 204: File is empty.";
                else if (returnedStatusCode == System.Net.HttpStatusCode.NotAcceptable)
                    errorMessage = "ERROR 406: File is missing header or footer or they are not prefixed with H or F.";
                else if (returnedStatusCode == System.Net.HttpStatusCode.PreconditionFailed)
                    errorMessage = "ERROR 412: No record added to database because number of footer records do not match with the number of actual records in the file.";
                else if (returnedStatusCode == System.Net.HttpStatusCode.Conflict)
                    errorMessage = "ERROR 409: File with requested header already exists in database.";
                else errorMessage = "ERROR 500: Internal Server Error";

                return View("Error", errorMessage);
            }
        }

        public ActionResult FlightRecordForm()
        {
            return View();
        }
    }
}
        