using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AerLingus.Models;
using System.Data.Entity;
using System.IO;
using System.Threading.Tasks;
using System.Net.Http;
using AerLingus.Controllers.Api;
using System.Web.Http;
using System.Web.Hosting;
using AerLingus.Helpers;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace AerLingus.Controllers
    
{
    public class FlightRecordsController : Controller
    {
        List<Flight_Records> flight_Records = new List<Flight_Records>();
        private HttpClient client;
        List<SearchFlightRecord> listaSearch = new List<SearchFlightRecord>();


        public FlightRecordsController()
        {
            client = new HttpClient();
            FlightRecordsApiController api = new FlightRecordsApiController();
            
        }

        public ActionResult Upload(HttpPostedFileBase file)
        {
            ApiViewBag.RequestIsComingFromController = true;
            ApiViewBag.RequestedFile = file;

            FlightRecordsApiController api = new FlightRecordsApiController()
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            var returnedStatusCode = api.Upload().StatusCode;                     

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
                    return View("UploadSuccessful");
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

        public ActionResult SearchFlightRecords()
        {
            return View();
        }

        //[System.Web.Http.HttpGet]
        //public ActionResult GetSearchedFlightRecords(SearchFlightRecord search)
        //{
        //    try
        //    {
        //        FlightRecordsApiController api = new FlightRecordsApiController()
        //        {
        //            Request = new HttpRequestMessage(),
        //            Configuration = new HttpConfiguration()
        //        };
        //        IEnumerable<Flight_Records> gg = api.GetSearchedFlightRecords(search);

        //        return View("SearchFlightRecords", gg);
        //    }
        //    catch (Exception ex)
        //    {
        //        return View("Error: " + ex.Message);
        //    }
        //}


        public void ExportToCSV()
        {
            listaSearch.Add(new SearchFlightRecord()
            {
                S_identifierNo = "1234567891011",
                S_otherFFPNo = "123",
                S_firstName = "Stefan",
                S_lastName = "Filipovic",
                S_departureDate = null,
                S_Origin = "Rome",
                S_destination = "Belgrade",
                S_bookingClass = "A",
                S_operatingAirline = "AirSerbia",
                S_ticketNo = "963255741",
                S_externalPaxID = "  1254982985985457",
                S_pnrNo = " 654231"
            });
 
             StringWriter sw = new StringWriter();
            Response.ClearContent();
            Response.ContentType = "text/csv";
            Response.AddHeader("content-disposition", "attachment;filename=FlightRecords"+DateTime.Now.ToShortDateString()+".csv");


            foreach (var client in listaSearch)
            {
                sw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\",\"{8}\",\"{9}\",\"{10}\",\"{11}\"",
                    client.S_identifierNo,
                    client.S_otherFFPNo,
                    client.S_firstName,
                    client.S_lastName,
                    client.S_departureDate,
                    client.S_Origin,
                    client.S_destination,
                    client.S_bookingClass,
                    client.S_operatingAirline,
                    client.S_ticketNo,
                    client.S_externalPaxID,
                    client.S_pnrNo));
            }
            Response.Write(sw.ToString());
            Response.End();
        }
        public void ExportToExcel()
        {
            listaSearch.Add(new SearchFlightRecord()
            {
                S_identifierNo = "1234567891011",
                S_otherFFPNo = "123",
                S_firstName = "Stefan",
                S_lastName = "Filipovic",
                S_departureDate = null,
                S_Origin = "Rome",
                S_destination = "Belgrade",
                S_bookingClass = "A",
                S_operatingAirline = "AirSerbia",
                S_ticketNo = "963255741",
                S_externalPaxID = "  1254982985985457",
                S_pnrNo = " 654231"
            });

            var grid = new GridView();
            grid.DataSource = from client in listaSearch
                              select new
                              {
                                  S_identifierNo = client.S_identifierNo,
                                  S_otherFFPNo = client.S_otherFFPNo,
                                  S_firstName = client.S_firstName,
                                  S_lastName = client.S_lastName,
                                  S_departureDate = client.S_departureDate,
                                  S_Origin = client.S_Origin,
                                  S_destination = client.S_destination,
                                  S_bookingClass = client.S_bookingClass,
                                  S_operatingAirline = client.S_operatingAirline,
                                  S_ticketNo = client.S_ticketNo,
                                  S_externalPaxID = client.S_externalPaxID,
                                  S_pnrNo = client.S_pnrNo
                              };
            grid.DataBind();

            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment;filename=FlightRecords" + DateTime.Now.ToShortDateString() + ".xlsx");
            Response.ContentType = "application/excel";
            StringWriter sw = new StringWriter();
            
            HtmlTextWriter htmlTextWriter = new HtmlTextWriter(sw);

            grid.RenderControl(htmlTextWriter);
            Response.Write(sw.ToString());

            Response.End();

        }
    }
}
