using AerLingus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace AerLingus.Controllers
{
    public class JourneySegmentsController : Controller
    {
        private HttpClient client;
        private HttpClient client1;
        private List<JourneySegment> journeySegments;
        private List<JourneySegment> listaSearch;
        private AerLingus_databaseEntities entities;

        public JourneySegmentsController()
        {
            client = new HttpClient();
            client1 = new HttpClient();
            journeySegments = new List<JourneySegment>();
            listaSearch = new List<JourneySegment>();
            entities = new AerLingus_databaseEntities();
        }

        // GET: JourneySegments
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddJourneySegment (JourneySegment j)
        {
            client.BaseAddress = new Uri(@"http://localhost:54789/api/JourneySegmentsApi/AddJourneySegment");
            var insertRecord = client.PostAsJsonAsync<JourneySegment>("", j);
            insertRecord.Wait();
            var recorddisplay = insertRecord.Result;
            if (recorddisplay.IsSuccessStatusCode)
            {
                //redirekcija u listu svih flight rekorda
                return View("UploadSuccessful");
            }
            else
            {
                return View("JournezSegmentForm");
            }
        }
        public ActionResult JourneySegmentForm()
        {
            return View();
        }

    }
}