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
using AerLingus.View_Models;

namespace AerLingus.Controllers
    
{
    public class FlightRecordsController : Controller
    {
        private List<Flight_Records> flight_Records = new List<Flight_Records>();
        private HttpClient client;
        private List<Flight_Records> listaSearch = new List<Flight_Records>();

        public FlightRecordsController()
        {
            client = new HttpClient();         
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
                hc.BaseAddress = new Uri(@"http://localhost:54789/api/FlightRecordsApi/AddFlightRecord");
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
            ViewBag.A = false;
            return View(new SearchViewModel
            {
                FlightRecords = flight_Records,
                Search = new SearchFlightRecord()
            });
        }

        [System.Web.Http.HttpGet]
        public ActionResult GetSearchedFlightRecords(SearchFlightRecord search)
        {
            try
            {
                FlightRecordsApiController api = new FlightRecordsApiController()
                {
                    Request = new HttpRequestMessage(),
                    Configuration = new HttpConfiguration()
                };

                //listaSearch = api.GetSearchedFlightRecords(search);

                if (ModelState.IsValid)
                {                  
                    SearchViewModel viewModel = new SearchViewModel
                    {                        
                        FlightRecords = api.GetSearchedFlightRecords(search)
                    };

                    foreach (Flight_Records searchRecord in viewModel.FlightRecords)
                    {
                        listaSearch.Add(searchRecord);
                    }
                    
                    ViewBag.A = true;

                    ModelState.Clear();

                    return View("SearchFlightRecords", viewModel);
                }
                else
                {
                    ViewBag.A = true;

                    SearchViewModel viewModel = new SearchViewModel
                    {
                        FlightRecords = new List<Flight_Records>()
                    };

                    //listaSearch = api.GetSearchedFlightRecords(search);

                    //ModelState.Clear();

                    return View("SearchFlightRecords", viewModel);
                }
            }
            catch (Exception ex)
            {
                object errorMessage = ex.Message;

                return View("Error", errorMessage);
            }
        }

        

        public void ExportToCSV(SearchFlightRecord search)
        {
            StringWriter sw = new StringWriter();
            Response.ClearContent();
            Response.ContentType = "text/csv";
            Response.AddHeader("content-disposition", "attachment;filename=FlightRecords" + DateTime.Now.ToShortDateString() + ".csv");


            FlightRecordsApiController api = new FlightRecordsApiController()
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            SearchViewModel viewModel = new SearchViewModel
            {
                FlightRecords = api.GetSearchedFlightRecords(search)
            };


            foreach (Flight_Records searchRecord in viewModel.FlightRecords)
            {
                listaSearch.Add(searchRecord);
            }


            foreach (var client in listaSearch)
            {
                sw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\",\"{8}\",\"{9}\",\"{10}\",\"{11}\",\"{12}\",\"{13}\",\"{14}\",\"{15}\",\"{16}\",\"{17}\",\"{18}\",\"{19}\",\"{20}\",\"{21}\",\"{22}\",\"{23}\",\"{24}\",\"{25}\",\"{26}\",\"{27}\",\"{28}\",\"{29}\"",
                                  client.identifierNo,
                                  client.transactionType,
                                  client.otherFFPNo,
                                  client.otherFFPScheme,
                                  client.firstName,
                                   client.lastName,
                                   client.partnerTransactionNo,
                                   client.bookingDate,
                                   client.departureDate,
                                   client.origin,
                                   client.destination,
                                   client.bookingClass,
                                   client.cabinClass,
                                   client.marketingFlightNo,
                                   client.marketingAirline,
                                   client.operatingFlightNo,
                                   client.operatingAirline,
                                   client.externalPaxID,
                                   client.couponNo,
                                   client.pnrNo,
                                   client.distance,
                                   client.baseFare,
                                   client.discountBase,
                                   client.exciseTax,
                                   client.customerType,
                                   client.promotionCode,
                                   client.ticketCurrency,
                                   client.targetCurrency,
                                   client.exchangeRate,
                                   client.fareBasis));
            }
            Response.Write(sw.ToString());
            Response.End();
        }

        public void ExportToExcel(SearchFlightRecord search)
        {
            var grid = new GridView();

            FlightRecordsApiController api = new FlightRecordsApiController()
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            SearchViewModel viewModel = new SearchViewModel
            {
                FlightRecords = api.GetSearchedFlightRecords(search)
            };


            foreach (Flight_Records searchRecord in viewModel.FlightRecords)
            {
                listaSearch.Add(searchRecord);
            }

            grid.DataSource = from client in listaSearch
                              select new
                              {
                                  identifierNo = client.identifierNo,
                                  transactionType = client.transactionType,
                                  otherFFPNo = client.otherFFPNo,
                                  otherFFPScheme = client.otherFFPScheme,
                                  firstName = client.firstName,
                                  lastName = client.lastName,
                                  partnerTransactionNo = client.partnerTransactionNo,
                                  bookingDate = client.bookingDate,
                                  departureDate = client.departureDate,
                                  origin = client.origin,
                                  destination = client.destination,
                                  bookingClass = client.bookingClass,
                                  cabinClass =client.cabinClass,
                                  marketingFlightNo= client.marketingFlightNo,
                                  marketingAirline = client.marketingAirline,
                                  operatingFlightNo=client.operatingFlightNo,
                                  operatingAirline = client.operatingAirline,
                                  ticketNo = client.ticketNo,
                                  externalPaxID = client.externalPaxID,
                                  couponNo=client.couponNo,
                                  pnrNo = client.pnrNo,
                                  distance= client.distance,
                                  baseFare=client.baseFare,
                                  discountBase=client.discountBase,
                                  exciseTax=client.exciseTax,
                                  customerType= client.customerType,
                                  promotionCode= client.promotionCode,
                                  ticketCurrency  =client.ticketCurrency,
                                  targetCurrency= client.targetCurrency,
                                  exchangeRate=client.exchangeRate,
                                  fareBasis= client.fareBasis,
 
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

        public ActionResult Details()
        {
            return View();
        }
    }
}
