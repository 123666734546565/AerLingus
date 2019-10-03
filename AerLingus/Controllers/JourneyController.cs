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
using System.Threading.Tasks;

namespace AerLingus.Controllers
{
    public class JourneyController : Controller
    {
        private HttpClient client;
        private List<Journey> journeys;
        private List<Journey> listaSearch;
        private AerLingus_databaseEntities entities;

        public JourneyController()
        {
            client = new HttpClient();
            journeys = new List<Journey>();
            listaSearch = new List<Journey>();         
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

        public ActionResult JourneyForm()
        {
            return View();
           
                
        }

        public ActionResult AddJourney(Journey j)
        {
            client.BaseAddress = new Uri(@"http://localhost:54789/api/JourneyApi/AddJourney");
            var insertRecord = client.PostAsJsonAsync<Journey>("", j);
            insertRecord.Wait();
            var recorddisplay = insertRecord.Result;
            if (recorddisplay.IsSuccessStatusCode)
            {
                //redirekcija u listu svih flight rekorda
                return View("UploadSuccessful");
            }
            else
            {
                return View("JourneyForm");
            }
        }

        [System.Web.Http.HttpGet]
        public ActionResult GetSearchedJourneys(SearchJourneyViewModel searchV)
        {
            var search = searchV.SearchJourney;

            try
            {
                JourneyApiController api = new JourneyApiController()
                {
                    Request = new HttpRequestMessage(),
                    Configuration = new HttpConfiguration()
                };

                if (ModelState.IsValid)
                {
                    SearchJourneyViewModel viewModel = new SearchJourneyViewModel
                    {
                        Journeys = api.GetSearchedJourneys(search)
                    };

                    foreach (Journey searchJourney in viewModel.Journeys)
                    {
                        listaSearch.Add(searchJourney);
                    }

                    ViewBag.A = true;

                    ModelState.Clear();

                    return View("_SearchJourneyList", viewModel);
                }
                else
                {
                    ViewBag.A = true;

                    SearchJourneyViewModel viewModel = new SearchJourneyViewModel
                    {
                        Journeys = new List<Journey>()
                    };

                    return View("_SearchJourneyList", viewModel);
                }
            }
            catch (Exception ex)
            {
                return View("Error", (object)"ERROR 500: " + ex.Message);
            }
        }

        public ActionResult Edit(int id)
        {
            try
            {
                var searchedJourney = entities.Journeys.SingleOrDefault(j => j.ID == id);

                if (searchedJourney == null)
                    return HttpNotFound("Journey with requested ID has not been found.");

                return View(searchedJourney);
            }
            catch(Exception ex)
            {
                return View("Error", (object)"ERROR 500: " + ex.Message);
            }
        }

        [System.Web.Http.HttpPut]
        public async Task<ActionResult> EditJourney(Journey journey)
        {
            try
            {
                var response = await client.PutAsJsonAsync(@"http://localhost:54789/api/JourneyApi/" + journey.ID.ToString(), journey);

                object errorMessage = null;

                if(response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return View("UploadSuccessful");
                }
                else if(response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    errorMessage = "ERROR 400: Invalid Model State";
                else if(response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    errorMessage = "ERROR 404: Record with requested ID has not been found";
                else errorMessage = "ERROR 500: Internal Server Error";

                return View("Error", errorMessage);
            }
            catch(Exception ex)
            {
                return View("Error", (object)"ERROR 500: " + ex.Message);

            }
        }

        public async Task<ActionResult> Details(int id)
        {
            try
            {
                var response = await client.GetAsync(@"http://localhost:54789/api/JourneyApi/" + id.ToString());

                if (response.IsSuccessStatusCode)
                    return View(await response.Content.ReadAsAsync<Journey>());

                else return View("Error", (object)"ERROR 404: Record with requested ID has not been found");
            }
            catch(Exception ex)
            {
                return View("Error", (object)"ERROR 500: " + ex.Message);
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                var searchedJourney = entities.Journeys.SingleOrDefault(j => j.ID == id);

                if (searchedJourney == null)
                    return HttpNotFound("Journey with requested ID has not been found.");

                return View(searchedJourney);
            }
            catch(Exception ex)
            {
                return View("Error", (object)"ERROR 500: " + ex.Message);
            }
        }

        [System.Web.Http.HttpDelete]
        public async Task<ActionResult> DeleteJourney(int id)
        {
            try
            {
                var response = await client.DeleteAsync(@"http://localhost:54789/api/JourneyApi/" + id.ToString());

                object errorMessage = null;

                if(response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return View("UploadSuccessful");
                }
                else if(response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    errorMessage = "ERROR 400: Invalid Model State";
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    errorMessage = "ERROR 404: Journey with requested ID has not been found";
                else errorMessage = "ERROR 500: Internal Server Error";

                return View("Error", errorMessage);
            }
            catch (Exception ex)
            {
                return View("Error", (object)"ERROR 500: " + ex.Message);
            }
        }

        //public ActionResult JourneySegments(int id)
        //{
        //    var journeySegment = entities.JourneySegments.SingleOrDefault(js => js.);
        //}
    }
}