using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using AerLingus.Helpers;
using AerLingus.Models;
using AerLingus.View_Models;
using AerLingus.Controllers.Api;
using System.Web.Http;

namespace AerLingus.Controllers
{
    public class JourneyController : Controller
    {
        private HttpClient client;

        private AerLingus_databaseEntities entities;

        public JourneyController()
        {
            client = new HttpClient();
            entities = new AerLingus_databaseEntities();
        }
        
        public ActionResult SearchJourney()
        {
            try
            {
                ViewBag.A = false;
                return View(new SearchJourneyViewModel
                {
                    Journeys = new List<Journey>(),
                    SearchJourney = new SearchJourney()
                });
            }
            catch(Exception ex)
            {
                return View("Error", (object)"ERROR 500: " + ex.Message);
            }
        }

        public ActionResult JourneyForm(Journey j)
        {
            if (!String.IsNullOrEmpty(j.TicketNo))
            {
                //Ako je sfr formular popunjen salju se podaci u API kako bi se sacuvali u bazu
                HttpClient hc = new HttpClient();
                hc.BaseAddress = new Uri(@"http://localhost:54789/api/JourneyApi/AddJourney");
                var insertRecord = hc.PostAsJsonAsync<Journey>("", j);
                insertRecord.Wait();
                var recorddisplay = insertRecord.Result;
                if (recorddisplay.IsSuccessStatusCode)
                {
                    //redirekcija u listu svih flight rekorda
                    return View("UploadSuccessful");
                }
                else
                {
                    return View("Error", j);
                }
            }
            else
            {
                return View("Error", j);
            }
        }

        //[System.Web.Http.HttpGet]
        //public ActionResult GetSearchedJourneys(SearchJourney search)
        //{
        //    try
        //    {
        //        JourneyApiController api = new JourneyApiController()
        //        {
        //            Request = new HttpRequestMessage(),
        //            Configuration = new HttpConfiguration
        //        };

        //        if (ModelState.IsValid)
        //        {
        //            SearchJourneyViewModel viewModel = new SearchJourneyViewModel
        //            {
        //                Journeys = api.GetSearchedJourneys(search)
        //            };

        //            foreach(Journey searchJourney in viewModel.Journeys)
        //            {

        //            }
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        return View("Error", (object)"ERROR 500: " + ex.Message);
        //    }
        //}
    }
}