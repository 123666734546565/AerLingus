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

        public async Task<ActionResult> Upload(HttpPostedFileBase file)
        {
            try
            {
                var response = await client.PostAsJsonAsync(@"http://localhost:54789/FlightRecordsApi",  file.ToString());
                
                if (response.IsSuccessStatusCode)
                    return Content("OK je");
                else return Content(response.StatusCode.ToString());
            }
            catch (Exception ex)
            {
                return Content("Something went wrong. " + ex.Message);
            }
        }

        public ActionResult FlightRecordForm()
        {
            return View();
        }

    }
}
        