﻿using System;
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
        private List<Journey> listaSearch;
        private List<Journey> journeys;
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

                    //ModelState.Clear();

                    return View("SearchJourney", viewModel);
                }
                else
                {
                    ViewBag.A = true;

                    SearchJourneyViewModel viewModel = new SearchJourneyViewModel
                    {
                        Journeys = new List<Journey>()
                    };

                    return View("SearchJourney", viewModel);
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
    }
}