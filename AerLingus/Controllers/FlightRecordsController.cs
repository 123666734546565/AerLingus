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

        public FlightRecordsController()
        {
            client = new HttpClient();
        }

        public ActionResult Upload(HttpPostedFileBase file)
        {
            if (file == null)
                return Content("No file selected.");

            if (file.ContentLength == 0)
                return Content("File is empty.");

            var fileName = Path.GetFileName(file.FileName);

            var path = Path.Combine(Server.MapPath("~/UploadedFiles"), fileName);

            file.SaveAs(path);

            FlightRecordsApiController api = new FlightRecordsApiController()
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            var returnedStatusCode = api.Upload(file).StatusCode;

            if (returnedStatusCode == System.Net.HttpStatusCode.OK)
                return View("UploadSuccessful");
            else
            {
                object errorMessage = null;

                if (returnedStatusCode == System.Net.HttpStatusCode.NotFound)
                    errorMessage = "ERROR 404: No file selected";
                else if (returnedStatusCode == System.Net.HttpStatusCode.NoContent)
                    errorMessage = "ERROR 204: File is empty";
                else if (returnedStatusCode == System.Net.HttpStatusCode.NotAcceptable)
                    errorMessage = "ERROR 406: File is missing header or footer or they are not prefixed with H or F";
                else if (returnedStatusCode == System.Net.HttpStatusCode.BadRequest)
                    errorMessage = "ERROR 400: Footer does not have the number of potential records";
                else if (returnedStatusCode == System.Net.HttpStatusCode.PreconditionFailed)
                    errorMessage = "ERROR 412: No record added to database because the number of footer records does not match with the number of existing records in the file";
                else if (returnedStatusCode == System.Net.HttpStatusCode.Conflict)
                    errorMessage = "ERROR 409: File with that header already exists in database";
                else errorMessage = "ERROR 500: Internal Server Error";
                return View("Error", errorMessage);
            }
        }

        public ActionResult FlightRecordForm()
        {
            return View();
        }

        [System.Web.Http.HttpPost]
        public ActionResult UploadFiles()
        {
            string poruka = string.Empty;
            if (Request.Files?.Count <= 0)
                return Content("There is no file selected");

            else
            {
                var filesCount = Request.Files.Count;
                for (int i = 0; i < filesCount; i++)
                {
                    var file = Request.Files[i];


                    if (file == null)
                        poruka = poruka + "No file selected.\n";

                    if (file.ContentLength == 0)
                        poruka = poruka + ("File is empty.\n");


                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/UploadedFiles"), fileName);
                    if (fileName != string.Empty)
                        return Content(fileName);
                    else
                        return Content("PRAZNO");
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
                        if (returnedStatusCode == System.Net.HttpStatusCode.NotFound)
                            poruka = poruka + "404: No file selected\n";
                        else if (returnedStatusCode == System.Net.HttpStatusCode.NoContent)
                            poruka = poruka + "204: File is empty\n";
                        else if (returnedStatusCode == System.Net.HttpStatusCode.NotAcceptable)
                            poruka = poruka + "406: File is missing header or footer or they are not prefixed with H or F\n";
                        else if (returnedStatusCode == System.Net.HttpStatusCode.PreconditionFailed)
                            poruka = poruka + "412: No record added to database because number of footer records do not match\n";
                        else if (returnedStatusCode == System.Net.HttpStatusCode.Conflict)
                            poruka = poruka + "409: File with that header already exists in database\n";
                        poruka = poruka + "500: Internal Server Error\n";
                    }
                }
                if (poruka != string.Empty)
                {
                    return Content(poruka);
                }
                else
                    return RedirectToAction("Index", "Home");
            }
        }

    }
}