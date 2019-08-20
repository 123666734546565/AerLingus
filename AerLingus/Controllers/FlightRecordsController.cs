using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.VisualBasic;
using AerLingus.Models;
using System.Net.Http;
using System.IO;
using System.Web.Hosting;
//using AerLingus.Helpers.Models;

namespace AerLingus.Controllers
{
    public class FlightRecordsController : Controller
    {
        List<Flight_Records> flight_Records = new List<Flight_Records>();

        // GET: FlightRecords
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FlightRecordForm(Flight_Records sfr)
        { 
            if (!String.IsNullOrEmpty(sfr.firstName) || !String.IsNullOrEmpty(sfr.lastName))
            {
                //Ako je sfr formular popunjen salju se podaci u API kako bi se sacuvali u bazu
                HttpClient hc = new HttpClient();
                hc.BaseAddress = new Uri(@"http://localhost:54789/api/FlightRecordsAPI/AddFlightRecord");
                var insertRecord = hc.PostAsJsonAsync<Flight_Records>("", sfr);
                insertRecord.Wait();
                var recorddisplay = insertRecord.Result;
                if (recorddisplay.IsSuccessStatusCode)
                {
                    //redirekcija u listu svih flight rekorda
                    return RedirectToAction("Index", "Home");
                }
            }
            //Prikazuje formular za dodavanje single flight rekorda ako zeli da doda novi sfr ili ako prethodno popunjavanje nije proslo kako treba
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem
            {
                Text = "AI",
                Value = "AI"
            });
            listItems.Add(new SelectListItem
            {
                Text = "AB",
                Value = "AB",
            });

            List<SelectListItem> listItems2 = new List<SelectListItem>();
            listItems2.Add(new SelectListItem
            {
                Text = "F",
                Value = "F"
            });
            listItems2.Add(new SelectListItem
            {
                Text = "J",
                Value = "J",
            });
            listItems2.Add(new SelectListItem
            {
                Text = "W",
                Value = "W",
            });
            listItems2.Add(new SelectListItem
            {
                Text = "Y",
                Value = "Y",
            });

            List<SelectListItem> listItems3 = new List<SelectListItem>();
            listItems3.Add(new SelectListItem
            {
                Text = "A",
                Value = "A"
            });
            listItems3.Add(new SelectListItem
            {
                Text = "C",
                Value = "C",

            });
            listItems3.Add(new SelectListItem
            {
                Text = "I",
                Value = "I",
            });

            StreamReader sr = new StreamReader(HostingEnvironment.ApplicationPhysicalPath + "/Content/currencies.txt");

            List<string> listItems4 = new List<string>();

            while (!sr.EndOfStream)
            {
                listItems4.Add(sr.ReadLine());
            }

            ViewBag.list1 = listItems;
            ViewBag.list2 = listItems2;
            ViewBag.list3 = listItems3;
            ViewBag.list4 = listItems4;

            return View();
        }

     
    }
}