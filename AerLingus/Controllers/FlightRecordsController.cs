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

        // GET: FlightRecords
        public ActionResult Index()
        {
            return View();
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
                    return Content("404: No file selected");
                else if (returnedStatusCode == System.Net.HttpStatusCode.NoContent)
                    return Content("204: File is empty");
                else if (returnedStatusCode == System.Net.HttpStatusCode.NotAcceptable)
                    return Content("406: File is missing header or footer or they are not prefixed with H or F");
                else if (returnedStatusCode == System.Net.HttpStatusCode.PreconditionFailed)
                    return Content("412: No record added to database because number of footer records do not match");
                else return Content("500: Internl Server Error");
            }
        }

        public ActionResult FlightRecordForm()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult Upload(HttpPostedFileBase file)
        //{
        //    if (file == null)
        //        return Content("No file selected.");

            //if (file.ContentLength > 0)
            //{
            //    var fileName = Path.GetFileName(file.FileName);
            //    var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
            //    file.SaveAs(path);
            //}
            //file.InputStream.
            //var contents = new StreamReader(file.InputStream).ReadLine();

            //if (contents == null)
            //    return Content("JESTE NULL");
            //else return Content("NIJE NULL  " + contents + "--" + file.ContentLength);

            //return RedirectToAction("Index", "Home");
        }
    }
        