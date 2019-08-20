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

            if (!flight.Upload(file).IsSuccessStatusCode)
                return Content(flight.poruka);

            return RedirectToAction("Index", "Home");
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
        