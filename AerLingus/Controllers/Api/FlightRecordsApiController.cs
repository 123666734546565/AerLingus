using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AerLingus.Models;
using System.IO;
using System.Web;
using Microsoft.VisualBasic;
using AerLingus.Validations;
using System.Threading.Tasks;
using AerLingus.Helpers;
using System.Globalization;

namespace AerLingus.Controllers.Api
{
    public class FlightRecordsApiController : ApiController
    {
        public string poruka;
        public string FailedRecords { get; set; }
        private AerLingus_databaseEntities entities;

        public FlightRecordsApiController()
        {
            FailedRecords = string.Empty;
            entities = new AerLingus_databaseEntities();
        }

        [HttpGet]
        public IEnumerable<Flight_Records> GetFlightRecords()
        {
            if (!entities.Flight_Records.Any())
                return default(IEnumerable<Flight_Records>);

            return entities.Flight_Records;
        }

        [HttpGet]
        public IHttpActionResult GetFlightRecord(int id)
        {
            var flightRecord = entities.Flight_Records.SingleOrDefault(fr => fr.ID == id);

            if (flightRecord == null)
                return NotFound();

            return Ok(flightRecord);
        }

        [HttpGet]
        [Route("api/FlightRecordsApi/SearchApi")]
        public List<Flight_Records> GetSearchedFlightRecordsApi()
        {
            DateTime departureDate = default(DateTime);
            bool departureDateConversionSuccessful = true;

            if (HttpContext.Current.Request.QueryString["departureDate"] != null)
                departureDateConversionSuccessful = DateTime.TryParse(HttpContext.Current.Request.QueryString["departureDate"], out departureDate);

            string identifierNo = HttpContext.Current.Request.QueryString["identifierNo"] != null ? HttpContext.Current.Request.QueryString["identifierNo"] : null;
            string otherFFPNo = HttpContext.Current.Request.QueryString["otherFFPNo"] != null ? HttpContext.Current.Request.QueryString["otherFFPNo"] : null;
            string firstName = HttpContext.Current.Request.QueryString["firstName"] != null ? HttpContext.Current.Request.QueryString["firstName"] : null;
            string lastName = HttpContext.Current.Request.QueryString["lastName"] != null ? HttpContext.Current.Request.QueryString["lastName"] : null;
            string origin = HttpContext.Current.Request.QueryString["origin"] != null ? HttpContext.Current.Request.QueryString["origin"] : null;
            string destination = HttpContext.Current.Request.QueryString["destination"] != null ? HttpContext.Current.Request.QueryString["destination"] : null;
            string bookingClass = HttpContext.Current.Request.QueryString["bookingClass"] != null ? HttpContext.Current.Request.QueryString["bookingClass"] : null;
            string operatingAirline = HttpContext.Current.Request.QueryString["operatingAirline"] != null ? HttpContext.Current.Request.QueryString["operatingAirline"] : null;
            string ticketNo = HttpContext.Current.Request.QueryString["ticketNo"] != null ? HttpContext.Current.Request.QueryString["ticketNo"] : null;
            string externalPaxID = HttpContext.Current.Request.QueryString["externalPaxID"] != null ? HttpContext.Current.Request.QueryString["externalPaxID"] : null;
            string pnrNo = HttpContext.Current.Request.QueryString["pnrNo"] != null ? HttpContext.Current.Request.QueryString["pnrNo"] : null;

            if (HttpContext.Current.Request.QueryString["departureDate"] != null && departureDateConversionSuccessful)
            {
                var searchedRecords = entities.Flight_Records.Where(fr =>
                                                        (identifierNo != null ? fr.identifierNo.StartsWith(identifierNo) : fr.identifierNo == fr.identifierNo) &&
                                                        (otherFFPNo != null ? fr.otherFFPNo.StartsWith(otherFFPNo) : fr.otherFFPNo == fr.otherFFPNo) &&
                                                        (pnrNo != null ? fr.pnrNo.StartsWith(pnrNo) : fr.pnrNo == fr.pnrNo) &&
                                                        (firstName != null ? fr.firstName.StartsWith(firstName) : fr.firstName == fr.firstName) &&
                                                        (lastName != null ? fr.lastName.StartsWith(lastName) : fr.lastName == fr.lastName) &&
                                                        (operatingAirline != null ? fr.operatingAirline.StartsWith(fr.operatingAirline) : fr.operatingAirline == fr.operatingAirline) &&
                                                        (externalPaxID != null ? fr.externalPaxID.StartsWith(externalPaxID) : fr.externalPaxID == fr.externalPaxID) &&
                                                        (ticketNo != null ? fr.ticketNo.StartsWith(ticketNo) : fr.ticketNo == fr.ticketNo) &&
                                                        (bookingClass != null ? fr.bookingClass.StartsWith(bookingClass) : fr.bookingClass == fr.bookingClass) &&
                                                        (departureDate != default(DateTime) ? fr.departureDate == departureDate : fr.departureDate == fr.departureDate) &&
                                                        (destination != null ? fr.destination.StartsWith(destination) : fr.destination == fr.destination) &&
                                                        (origin != null ? fr.origin.StartsWith(origin) : fr.origin == fr.origin)
                                                        ).ToList();

                return searchedRecords;
            }
            else if (HttpContext.Current.Request.QueryString["departureDate"] != null && !departureDateConversionSuccessful)
            {
                return default(List<Flight_Records>);
            }
            else
            {
                var searchedRecords = entities.Flight_Records.Where(fr =>
                                                        (identifierNo != null ? fr.identifierNo.StartsWith(identifierNo) : fr.identifierNo == fr.identifierNo) &&
                                                        (otherFFPNo != null ? fr.otherFFPNo.StartsWith(otherFFPNo) : fr.otherFFPNo == fr.otherFFPNo) &&
                                                        (pnrNo != null ? fr.pnrNo.StartsWith(pnrNo) : fr.pnrNo == fr.pnrNo) &&
                                                        (firstName != null ? fr.firstName.StartsWith(firstName) : fr.firstName == fr.firstName) &&
                                                        (lastName != null ? fr.lastName.StartsWith(lastName) : fr.lastName == fr.lastName) &&
                                                        (operatingAirline != null ? fr.operatingAirline.StartsWith(fr.operatingAirline) : fr.operatingAirline == fr.operatingAirline) &&
                                                        (externalPaxID != null ? fr.externalPaxID.StartsWith(externalPaxID) : fr.externalPaxID == fr.externalPaxID) &&
                                                        (ticketNo != null ? fr.ticketNo.StartsWith(ticketNo) : fr.ticketNo == fr.ticketNo) &&
                                                        (bookingClass != null ? fr.bookingClass.StartsWith(bookingClass) : fr.bookingClass == fr.bookingClass) &&
                                                        (destination != null ? fr.destination.StartsWith(destination) : fr.destination == fr.destination) &&
                                                        (origin != null ? fr.origin.StartsWith(origin) : fr.origin == fr.origin)
                                                        ).ToList();

                return searchedRecords;
            }
        }

        [Route("api/FlightRecordsApi/Search")]
        public List<Flight_Records> GetSearchedFlightRecords(SearchFlightRecord search)
        {
            /*if (search.S_identifierNo == null && search.S_otherFFPNo == null && search.S_pnrNo == null &&
                search.S_firstName == null && search.S_lastName == null && search.S_operatingAirline == null &&
                search.S_externalPaxID == null && search.S_ticketNo == null && search.S_Origin == null &&
                search.S_bookingClass == null && search.S_departureDate == null && search.S_destination == null
                )
                return new List<Flight_Records>();*/

            var searchedRecords = entities.Flight_Records.Where(fr =>
                                                            (search.S_identifierNo != null ? fr.identifierNo.StartsWith(search.S_identifierNo) : fr.identifierNo == fr.identifierNo) &&
                                                            (search.S_otherFFPNo != null ? fr.otherFFPNo.StartsWith(search.S_otherFFPNo) : fr.otherFFPNo == fr.otherFFPNo) &&
                                                            (search.S_pnrNo != null ? fr.pnrNo.StartsWith(search.S_pnrNo) : fr.pnrNo == fr.pnrNo) &&
                                                            (search.S_firstName != null ? fr.firstName.StartsWith(search.S_firstName) : fr.firstName == fr.firstName) &&
                                                            (search.S_lastName != null ? fr.lastName.StartsWith(search.S_lastName) : fr.lastName == fr.lastName) &&
                                                            (search.S_operatingAirline != null ? fr.operatingAirline.StartsWith(search.S_operatingAirline) : fr.operatingAirline == fr.operatingAirline) &&
                                                            (search.S_externalPaxID != null ? fr.externalPaxID.StartsWith(search.S_externalPaxID) : fr.externalPaxID == fr.externalPaxID) &&
                                                            (search.S_ticketNo != null ? fr.ticketNo.StartsWith(search.S_ticketNo) : fr.ticketNo == fr.ticketNo) &&
                                                            (search.S_bookingClass != null ? fr.bookingClass.StartsWith(search.S_bookingClass) : fr.bookingClass == fr.bookingClass) &&
                                                            (search.S_departureDate != null ? fr.departureDate == search.S_departureDate : fr.departureDate == fr.departureDate) &&
                                                            (search.S_destination != null ? fr.destination.StartsWith(search.S_destination) : fr.destination == fr.destination) &&
                                                            (search.S_Origin != null ? fr.origin.StartsWith(search.S_Origin) : fr.origin == fr.origin)
                                                            ).ToList();

            return searchedRecords;
        }

        [HttpPost]
        [Route("api/FlightRecordsApi/UploadUASCP")]
        public HttpResponseMessage UploadUASCP()
        {
            try
            {
                if (ApiViewBag.UploadRequest.RequestIsComingFromController)
                {
                    ApiViewBag.UploadRequest.RequestIsComingFromController = false;

                    HttpPostedFileBase file = ApiViewBag.UploadRequest.RequestedFile;

                    if (file == null)
                        return Request.CreateResponse(HttpStatusCode.NotFound);
                    if (file.ContentLength == 0)
                        return Request.CreateResponse(HttpStatusCode.NoContent);

                    Stream stream = file.InputStream;

                    StreamReader streamReader = new StreamReader(stream);

                    string headerMetaData = streamReader.ReadLine();

                    string footerMetaData = string.Empty;

                    int lineCounter = 0;

                    while (!streamReader.EndOfStream)
                    {
                        footerMetaData = streamReader.ReadLine();
                        lineCounter++;
                    }

                    stream.Position = 0;

                    streamReader.ReadLine();

                    while (!streamReader.EndOfStream)
                    {
                        string record = streamReader.ReadLine();

                        if (record != footerMetaData)
                        {
                            PreStagging preStagging = new PreStagging();
                            preStagging.HeaderMetaData = headerMetaData;
                            preStagging.RecordMetaData = record;
                            preStagging.FooterMetaData = footerMetaData;
                            entities.PreStaggings.Add(preStagging);
                        }
                    }

                    stream.Position = 0;

                    streamReader.ReadLine();

                    string header = string.Empty;

                    for (int i = 12; i <= 13; i++)
                    {
                        if (headerMetaData[i] == ' ')
                            continue;

                        header += headerMetaData[i];
                    }

                    lineCounter--;

                    string temp = string.Empty;
                    int numberOfFooterRecords = 0;

                    for (int i = 2; i <= 10; i++)
                    {
                        temp += footerMetaData[i];
                    }

                    numberOfFooterRecords = Convert.ToInt32(temp);

                    if (numberOfFooterRecords != lineCounter)
                        return Request.CreateResponse(HttpStatusCode.NotAcceptable);

                    int currentLine = 0;

                    //header data
                    string recordTypeHeader = string.Empty;
                    string fileTypeHeader = string.Empty;
                    string deliverySequenceNoHeader = string.Empty;
                    string sendingSystemHeader = string.Empty;
                    string sendingAirLineHeader = string.Empty;
                    string receivingAirlineHeader = string.Empty;
                    string createDateHeader = string.Empty;
                    string versionHeader = string.Empty;
                    string carrierFileReferenceHeader = string.Empty;
                    string fillerHeader = string.Empty;

                    //footer data
                    string recordTypeFooter = string.Empty;
                    string totalNumberOfRecordsFooter = string.Empty;
                    string numberOfAcceptedRecordsWithoutChangesFooter = string.Empty;
                    string numberOfAcceptedRecordsWithChangesFooter = string.Empty;
                    string numberOfRejectedRecordsFooter = string.Empty;
                    string fillerFooter = string.Empty;

                    //header
                    for (int i = 0; i <= 1; i++)
                    {
                        recordTypeHeader += headerMetaData[i];
                    }

                    for (int i = 2; i <= 3; i++)
                    {
                        fileTypeHeader += headerMetaData[i];
                    }

                    for (int i = 4; i <= 8; i++)
                    {
                        deliverySequenceNoHeader += headerMetaData[i];
                    }

                    for (int i = 9; i <= 11; i++)
                    {
                        sendingSystemHeader += headerMetaData[i];
                    }

                    for (int i = 12; i <= 14; i++)
                    {
                        sendingAirLineHeader += headerMetaData[i];
                    }

                    for (int i = 15; i <= 17; i++)
                    {
                        receivingAirlineHeader += headerMetaData[i];
                    }

                    for (int i = 18; i <= 25; i++)
                    {
                        createDateHeader += headerMetaData[i];
                    }

                    for (int i = 26; i <= 27; i++)
                    {
                        versionHeader += headerMetaData[i];
                    }

                    for (int i = 28; i <= 43; i++)
                    {
                        carrierFileReferenceHeader += headerMetaData[i];
                    }

                    for (int i = 44; i <= 499; i++)
                    {
                        fillerHeader += headerMetaData[i];
                    }

                    //footer
                    for (int i = 0; i <= 1; i++)
                    {
                        recordTypeFooter += footerMetaData[i];
                    }

                    for (int i = 2; i <= 10; i++)
                    {
                        totalNumberOfRecordsFooter += footerMetaData[i];
                    }

                    for (int i = 11; i <= 19; i++)
                    {
                        numberOfAcceptedRecordsWithoutChangesFooter += footerMetaData[i];
                    }

                    for (int i = 20; i <= 28; i++)
                    {
                        numberOfAcceptedRecordsWithChangesFooter += footerMetaData[i];
                    }

                    for (int i = 29; i <= 37; i++)
                    {
                        numberOfRejectedRecordsFooter += footerMetaData[i];
                    }

                    for (int i = 38; i <= 499; i++)
                    {
                        fillerFooter += footerMetaData[i];
                    }

                    lineCounter--;

                    while (currentLine <= lineCounter)
                    {
                        Flight_Records flightRecords = new Flight_Records();
                        string record = streamReader.ReadLine();
                        StaggingRejectedUA rejectedUA = new StaggingRejectedUA();

                        if (record.Length < 500)
                        {
                            rejectedUA.Header = headerMetaData;
                            rejectedUA.Record = record;
                            rejectedUA.Footer = footerMetaData;
                            rejectedUA.Description = "Incorrect length.";
                            rejectedUA.DownloadCounter = 0;
                            entities.StaggingRejectedUAs.Add(rejectedUA);
                            entities.SaveChanges();
                            currentLine++;
                            continue;
                        }

                        StaggingUA staggingUA = new StaggingUA();

                        //header
                        staggingUA.RecordTypeHeader = recordTypeHeader;
                        staggingUA.FileTypeHeader = fileTypeHeader;
                        staggingUA.DeliverySequenceNoHeader = deliverySequenceNoHeader;
                        staggingUA.SendingSystemHeader = sendingSystemHeader;
                        staggingUA.SendingAirlineHeader = sendingAirLineHeader;
                        staggingUA.ReceivingAirlineHeader = receivingAirlineHeader;
                        staggingUA.CreateDateHeader = createDateHeader;
                        staggingUA.VersionHeader = versionHeader;
                        staggingUA.CarrierFileReferenceHeader = carrierFileReferenceHeader;
                        staggingUA.FillerHeader = fillerHeader;

                        //footer
                        staggingUA.RecordTypeFooter = recordTypeFooter;
                        staggingUA.TotalNumbersOfRecords = totalNumberOfRecordsFooter;
                        staggingUA.NumberOfAcceptedRecordsWithoutChanges = numberOfAcceptedRecordsWithoutChangesFooter;
                        staggingUA.NumberOfAcceptedRecordsWithChanges = numberOfAcceptedRecordsWithChangesFooter;
                        staggingUA.NumberOfRejectedRecords = numberOfRejectedRecordsFooter;
                        staggingUA.FillerFooter = fillerFooter;

                        if (header.ToUpper() == "UA")
                        {
                            flightRecords.Status = "Sent from UA";
                        }

                        flightRecords.externalPaxID = "12345";

                        for (int i = 0; i <= 1; i++)
                        {
                            staggingUA.RecordType += record[i];

                            if (record[i] == ' ')
                                continue;

                            flightRecords.transactionType += record[i];
                        }

                        for (int i = 2; i <= 4; i++)
                        {
                            staggingUA.FFPProgram += record[i];
                        }

                        for (int i = 5; i <= 24; i++)
                        {
                            staggingUA.FFPMemberNumber += record[i];
                        }

                        for (int i = 25; i <= 54; i++)
                        {
                            staggingUA.FFPMemberLastName += record[i];

                            if (record[i] == ' ')
                                continue;

                            flightRecords.lastName += record[i];
                        }

                        for (int i = 55; i <= 104; i++)
                        {
                            staggingUA.FFPMemberFirstName += record[i];

                            if (record[i] == ' ')
                                continue;

                            flightRecords.firstName += record[i];
                        }

                        staggingUA.NameCheckOverride = record[105].ToString();

                        for (int i = 106; i <= 108; i++)
                        {
                            staggingUA.OperatingAirlineCode += record[i];

                            if (record[i] == ' ')
                                continue;

                            flightRecords.operatingAirline += record[i];
                        }

                        for (int i = 109; i <= 113; i++)
                        {
                            staggingUA.OperatingFlightNumber += record[i];

                            if (record[i] == ' ')
                                continue;

                            flightRecords.operatingFlightNo += record[i];
                        }

                        for (int i = 114; i <= 116; i++)
                        {
                            staggingUA.MarketingAirlineCode += record[i];

                            if (record[i] == ' ')
                                continue;

                            flightRecords.marketingAirline += record[i];
                        }

                        for (int i = 117; i <= 121; i++)
                        {
                            staggingUA.MarketingFlightNumber += record[i];

                            if (record[i] == ' ')
                                continue;

                            flightRecords.marketingFlightNo += record[i];
                        }

                        staggingUA.CodeShareIndicator = record[122].ToString();

                        string day = null;
                        string month = "";
                        string year = "";

                        for (int i = 123; i <= 126; i++)
                        {
                            if (record[i] == ' ')
                                continue;

                            year = year + record[i];
                        }


                        for (int i = 127; i <= 128; i++)
                        {
                            if (record[i] == ' ')
                                continue;

                            month = month + record[i];
                        }

                        for (int i = 129; i <= 130; i++)
                        {
                            if (record[i] == ' ')
                                continue;

                            day = day + record[i];
                        }

                        string date = day + "/" + month + "/" + year;

                        DateTime dateTime = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                        staggingUA.FlightDepartureDate = year + month + day;
                        flightRecords.departureDate = dateTime;

                        for (int i = 131; i <= 138; i++)
                        {
                            staggingUA.FlightArrivalDate += record[i];
                        }

                        for (int i = 139; i <= 143; i++)
                        {
                            staggingUA.OriginAirportCode += record[i];

                            if (record[i] == ' ')
                                continue;

                            flightRecords.origin += record[i];
                        }

                        for (int i = 144; i <= 148; i++)
                        {
                            staggingUA.DestinationAirportCode += record[i];

                            if (record[i] == ' ')
                                continue;

                            flightRecords.destination += record[i];
                        }

                        for (int i = 149; i <= 150; i++)
                        {
                            staggingUA.OperatingBookingClassCode += record[i];

                            if (record[i] == ' ')
                                continue;

                            flightRecords.bookingClass += record[i];
                        }

                        for (int i = 151; i <= 152; i++)
                        {
                            staggingUA.FlownCabinClassCode += record[i];

                            if (record[i] == ' ')
                                continue;

                            flightRecords.cabinClass += record[i];
                        }

                        for (int i = 153; i <= 154; i++)
                        {
                            staggingUA.OperatingRevenueBookingClassCode += record[i];
                        }

                        for (int i = 155; i <= 156; i++)
                        {
                            staggingUA.MarketingBookingClass += record[i];
                        }

                        for (int i = 157; i <= 158; i++)
                        {
                            staggingUA.UpgradeIndicator += record[i];
                        }

                        for (int i = 159; i <= 160; i++)
                        {
                            staggingUA.DowngradeIndicator += record[i];
                        }

                        for (int i = 161; i <= 175; i++)
                        {
                            staggingUA.EntitlementNumber += record[i];
                        }

                        for (int i = 176; i <= 188; i++)
                        {
                            staggingUA.TicketNumber += record[i];

                            if (record[i] == ' ')
                                continue;

                            flightRecords.ticketNo += record[i];
                        }

                        for (int i = 189; i <= 190; i++)
                        {
                            staggingUA.CouponNumber += record[i];

                            if (record[i] == ' ')
                                continue;

                            flightRecords.couponNo += record[i];
                        }

                        for (int i = 191; i <= 198; i++)
                        {
                            staggingUA.FareBasisCode += record[i];

                            if (record[i] == ' ')
                                continue;

                            flightRecords.fareBasis += record[i];
                        }

                        for (int i = 199; i <= 202; i++)
                        {
                            staggingUA.SeatNumber += record[i];
                        }

                        for (int i = 203; i <= 208; i++)
                        {
                            staggingUA.PNRNumber += record[i];

                            if (record[i] == ' ')
                                continue;

                            flightRecords.pnrNo += record[i];
                        }

                        staggingUA.UpdateCode = record[209].ToString();

                        for (int i = 210; i <= 214; i++)
                        {
                            staggingUA.BaseFlightMiles += record[i];
                        }

                        for (int i = 215; i <= 216; i++)
                        {
                            staggingUA.CheckInSourceCode += record[i];
                        }

                        for (int i = 217; i <= 218; i++)
                        {
                            staggingUA.BookingSourceCode += record[i];
                        }

                        for (int i = 219; i <= 234; i++)
                        {
                            staggingUA.OperatingAirlineAuthorizationNo += record[i];
                        }

                        for (int i = 235; i <= 250; i++)
                        {
                            staggingUA.InternalAirlineReferenceNoFFPAirline += record[i];
                        }

                        for (int i = 251; i <= 252; i++)
                        {
                            staggingUA.AccrualPostingStatus += record[i];
                        }

                        for (int i = 253; i <= 255; i++)
                        {
                            staggingUA.ResponseCode1 += record[i];
                        }

                        for (int i = 256; i <= 258; i++)
                        {
                            staggingUA.ResponseCode2 += record[i];
                        }

                        for (int i = 259; i <= 261; i++)
                        {
                            staggingUA.ResponseCode3 += record[i];
                        }

                        for (int i = 262; i <= 264; i++)
                        {
                            staggingUA.ResponseCode4 += record[i];
                        }

                        for (int i = 265; i <= 267; i++)
                        {
                            staggingUA.ResponseCode5 += record[i];
                        }

                        for (int i = 268; i <= 270; i++)
                        {
                            staggingUA.ResponseCode6 += record[i];
                        }

                        for (int i = 271; i <= 272; i++)
                        {
                            staggingUA.TierLever += record[i];
                        }

                        staggingUA.Gender = record[273].ToString();

                        for (int i = 274; i <= 399; i++)
                        {
                            staggingUA.Filler += record[i];
                        }

                        for (int i = 400; i <= 499; i++)
                        {
                            staggingUA.AirlineAdditionalInformation += record[i];
                        }

                        currentLine++;

                        //013
                        if (Validation.IsModelStateValid(flightRecords))
                        {
                            //002
                            if (header != "" && header == flightRecords.operatingAirline)
                            {
                                //015
                                if (staggingUA.NameCheckOverride == "Y" || staggingUA.NameCheckOverride == "N")
                                {
                                    //027
                                    if ((flightRecords.origin != "" && flightRecords.origin != null) ||
                                        flightRecords.destination != "" && flightRecords.destination != null)
                                    {
                                        //023
                                        if (flightRecords.transactionType == "01" || flightRecords.transactionType == "02" ||
                                            flightRecords.transactionType == "03" || flightRecords.transactionType == "04")
                                        {
                                            if (flightRecords.marketingAirline != "" && flightRecords.marketingAirline != null)
                                            {
                                                //051
                                                if (staggingUA.FFPMemberNumber != string.Empty && staggingUA.FFPMemberNumber != null)
                                                {
                                                    entities.StaggingUAs.Add(staggingUA);
                                                    entities.Flight_Records.Add(flightRecords);
                                                }
                                                else
                                                {
                                                    staggingUA.Description = "051: Invalid FFP number";
                                                    staggingUA.DownloadCounter = 0;
                                                    entities.StaggingUAs.Add(staggingUA);
                                                }
                                            }
                                            else
                                            {
                                                staggingUA.Description = "044: Invalid marketing airline code";
                                                staggingUA.DownloadCounter = 0;
                                                entities.StaggingUAs.Add(staggingUA);
                                            }
                                        }
                                        else
                                        {
                                            staggingUA.Description = "023: Invalid transaction type";
                                            staggingUA.DownloadCounter = 0;
                                            entities.StaggingUAs.Add(staggingUA);
                                        }
                                    }
                                    else
                                    {
                                        staggingUA.Description = "027: Invalid origin and/or destination airport code";
                                        staggingUA.DownloadCounter = 0;
                                        entities.StaggingUAs.Add(staggingUA);
                                    }
                                }
                                else
                                {
                                    staggingUA.Description = "Invalid name check override.";
                                    staggingUA.DownloadCounter = 0;
                                    entities.StaggingUAs.Add(staggingUA);
                                }
                            }
                            else
                            {
                                staggingUA.Description = "Header is incorrect.";
                                staggingUA.DownloadCounter = 0;
                                entities.StaggingUAs.Add(staggingUA);
                            }
                        }
                        else
                        {
                            staggingUA.Description = "Mandatory fields data is missing or in invalid format";
                            staggingUA.DownloadCounter = 0;
                            entities.StaggingUAs.Add(staggingUA);
                        }
                    }

                    entities.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    var currentRequest = HttpContext.Current;

                    var file = currentRequest.Request.Files[0];

                    if (file == null)
                        return Request.CreateResponse(HttpStatusCode.NotFound);
                    if (file.ContentLength == 0)
                        return Request.CreateResponse(HttpStatusCode.NoContent);

                    Stream stream = file.InputStream;

                    StreamReader streamReader = new StreamReader(stream);

                    string headerMetaData = streamReader.ReadLine();

                    string footerMetaData = string.Empty;

                    int lineCounter = 0;

                    while (!streamReader.EndOfStream)
                    {
                        footerMetaData = streamReader.ReadLine();
                        lineCounter++;
                    }

                    stream.Position = 0;

                    streamReader.ReadLine();

                    while (!streamReader.EndOfStream)
                    {
                        string record = streamReader.ReadLine();

                        if (record != footerMetaData)
                        {
                            PreStagging preStagging = new PreStagging();
                            preStagging.HeaderMetaData = headerMetaData;
                            preStagging.RecordMetaData = record;
                            preStagging.FooterMetaData = footerMetaData;
                            entities.PreStaggings.Add(preStagging);
                        }
                    }

                    stream.Position = 0;

                    streamReader.ReadLine();

                    string header = string.Empty;

                    for (int i = 12; i <= 13; i++)
                    {
                        if (headerMetaData[i] == ' ')
                            continue;

                        header += headerMetaData[i];
                    }

                    //while (!streamReader.EndOfStream)
                    //{
                    //    streamReader.ReadLine();
                    //    lineCounter++;
                    //}

                    lineCounter--;

                    string temp = string.Empty;
                    int numberOfFooterRecords = 0;

                    for (int i = 2; i <= 10; i++)
                    {
                        temp += footerMetaData[i];
                    }

                    numberOfFooterRecords = Convert.ToInt32(temp);

                    if (numberOfFooterRecords != lineCounter)
                        return Request.CreateResponse(HttpStatusCode.NotAcceptable);

                    int currentLine = 0;

                    //header data
                    string recordTypeHeader = string.Empty;
                    string fileTypeHeader = string.Empty;
                    string deliverySequenceNoHeader = string.Empty;
                    string sendingSystemHeader = string.Empty;
                    string sendingAirLineHeader = string.Empty;
                    string receivingAirlineHeader = string.Empty;
                    string createDateHeader = string.Empty;
                    string versionHeader = string.Empty;
                    string carrierFileReferenceHeader = string.Empty;
                    string fillerHeader = string.Empty;

                    //footer data
                    string recordTypeFooter = string.Empty;
                    string totalNumberOfRecordsFooter = string.Empty;
                    string numberOfAcceptedRecordsWithoutChangesFooter = string.Empty;
                    string numberOfAcceptedRecordsWithChangesFooter = string.Empty;
                    string numberOfRejectedRecordsFooter = string.Empty;
                    string fillerFooter = string.Empty;

                    //header
                    for (int i = 0; i <= 1; i++)
                    {
                        recordTypeHeader += headerMetaData[i];
                    }

                    for (int i = 2; i <= 3; i++)
                    {
                        fileTypeHeader += headerMetaData[i];
                    }

                    for (int i = 4; i <= 8; i++)
                    {
                        deliverySequenceNoHeader += headerMetaData[i];
                    }

                    for (int i = 9; i <= 11; i++)
                    {
                        sendingSystemHeader += headerMetaData[i];
                    }

                    for (int i = 12; i <= 14; i++)
                    {
                        sendingAirLineHeader += headerMetaData[i];
                    }

                    for (int i = 15; i <= 17; i++)
                    {
                        receivingAirlineHeader += headerMetaData[i];
                    }

                    for (int i = 18; i <= 25; i++)
                    {
                        createDateHeader += headerMetaData[i];
                    }

                    for (int i = 26; i <= 27; i++)
                    {
                        versionHeader += headerMetaData[i];
                    }

                    for (int i = 28; i <= 43; i++)
                    {
                        carrierFileReferenceHeader += headerMetaData[i];
                    }

                    for (int i = 44; i <= 499; i++)
                    {
                        fillerHeader += headerMetaData[i];
                    }

                    //footer
                    for (int i = 0; i <= 1; i++)
                    {
                        recordTypeFooter += footerMetaData[i];
                    }

                    for (int i = 2; i <= 10; i++)
                    {
                        totalNumberOfRecordsFooter += footerMetaData[i];
                    }

                    for (int i = 11; i <= 19; i++)
                    {
                        numberOfAcceptedRecordsWithoutChangesFooter += footerMetaData[i];
                    }

                    for (int i = 20; i <= 28; i++)
                    {
                        numberOfAcceptedRecordsWithChangesFooter += footerMetaData[i];
                    }

                    for (int i = 29; i <= 37; i++)
                    {
                        numberOfRejectedRecordsFooter += footerMetaData[i];
                    }

                    for (int i = 38; i <= 499; i++)
                    {
                        fillerFooter += footerMetaData[i];
                    }

                    lineCounter--;

                    while (currentLine <= lineCounter)
                    {
                        Flight_Records flightRecords = new Flight_Records();
                        string record = streamReader.ReadLine();
                        StaggingRejectedUA rejectedUA = new StaggingRejectedUA();

                        if (record.Length < 500)
                        {
                            rejectedUA.Header = headerMetaData;
                            rejectedUA.Record = record;
                            rejectedUA.Footer = footerMetaData;
                            rejectedUA.Description = "Incorrect length.";
                            rejectedUA.DownloadCounter = 0;
                            entities.StaggingRejectedUAs.Add(rejectedUA);
                            entities.SaveChanges();
                            currentLine++;
                            continue;
                        }

                        StaggingUA staggingUA = new StaggingUA();

                        //header
                        staggingUA.RecordTypeHeader = recordTypeHeader;
                        staggingUA.FileTypeHeader = fileTypeHeader;
                        staggingUA.DeliverySequenceNoHeader = deliverySequenceNoHeader;
                        staggingUA.SendingSystemHeader = sendingSystemHeader;
                        staggingUA.SendingAirlineHeader = sendingAirLineHeader;
                        staggingUA.ReceivingAirlineHeader = receivingAirlineHeader;
                        staggingUA.CreateDateHeader = createDateHeader;
                        staggingUA.VersionHeader = versionHeader;
                        staggingUA.CarrierFileReferenceHeader = carrierFileReferenceHeader;
                        staggingUA.FillerHeader = fillerHeader;

                        //footer
                        staggingUA.RecordTypeFooter = recordTypeFooter;
                        staggingUA.TotalNumbersOfRecords = totalNumberOfRecordsFooter;
                        staggingUA.NumberOfAcceptedRecordsWithoutChanges = numberOfAcceptedRecordsWithoutChangesFooter;
                        staggingUA.NumberOfAcceptedRecordsWithChanges = numberOfAcceptedRecordsWithChangesFooter;
                        staggingUA.NumberOfRejectedRecords = numberOfRejectedRecordsFooter;
                        staggingUA.FillerFooter = fillerFooter;

                        if (header.ToUpper() == "UA")
                        {
                            flightRecords.Status = "Sent from UA";
                        }

                        flightRecords.externalPaxID = "12345";

                        for (int i = 0; i <= 1; i++)
                        {
                            staggingUA.RecordType += record[i];

                            if (record[i] == ' ')
                                continue;

                            flightRecords.transactionType += record[i];
                        }

                        for (int i = 2; i <= 4; i++)
                        {
                            staggingUA.FFPProgram += record[i];
                        }

                        for (int i = 5; i <= 24; i++)
                        {
                            staggingUA.FFPMemberNumber += record[i];
                        }

                        for (int i = 25; i <= 54; i++)
                        {
                            staggingUA.FFPMemberLastName += record[i];

                            if (record[i] == ' ')
                                continue;

                            flightRecords.lastName += record[i];
                        }

                        for (int i = 55; i <= 104; i++)
                        {
                            staggingUA.FFPMemberFirstName += record[i];

                            if (record[i] == ' ')
                                continue;

                            flightRecords.firstName += record[i];
                        }

                        staggingUA.NameCheckOverride = record[105].ToString();

                        for (int i = 106; i <= 108; i++)
                        {
                            staggingUA.OperatingAirlineCode += record[i];

                            if (record[i] == ' ')
                                continue;

                            flightRecords.operatingAirline += record[i];
                        }

                        for (int i = 109; i <= 113; i++)
                        {
                            staggingUA.OperatingFlightNumber += record[i];

                            if (record[i] == ' ')
                                continue;

                            flightRecords.operatingFlightNo += record[i];
                        }

                        for (int i = 114; i <= 116; i++)
                        {
                            staggingUA.MarketingAirlineCode += record[i];

                            if (record[i] == ' ')
                                continue;

                            flightRecords.marketingAirline += record[i];
                        }

                        for (int i = 117; i <= 121; i++)
                        {
                            staggingUA.MarketingFlightNumber += record[i];

                            if (record[i] == ' ')
                                continue;

                            flightRecords.marketingFlightNo += record[i];
                        }

                        staggingUA.CodeShareIndicator = record[122].ToString();

                        string day = null;
                        string month = "";
                        string year = "";

                        for (int i = 123; i <= 126; i++)
                        {
                            if (record[i] == ' ')
                                continue;

                            year = year + record[i];
                        }


                        for (int i = 127; i <= 128; i++)
                        {
                            if (record[i] == ' ')
                                continue;

                            month = month + record[i];
                        }

                        for (int i = 129; i <= 130; i++)
                        {
                            if (record[i] == ' ')
                                continue;

                            day = day + record[i];
                        }

                        string date = day + "/" + month + "/" + year;

                        DateTime dateTime = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                        staggingUA.FlightDepartureDate = year + month + day;
                        flightRecords.departureDate = dateTime;

                        for (int i = 131; i <= 138; i++)
                        {
                            staggingUA.FlightArrivalDate += record[i];
                        }

                        for (int i = 139; i <= 143; i++)
                        {
                            staggingUA.OriginAirportCode += record[i];

                            if (record[i] == ' ')
                                continue;

                            flightRecords.origin += record[i];
                        }

                        for (int i = 144; i <= 148; i++)
                        {
                            staggingUA.DestinationAirportCode += record[i];

                            if (record[i] == ' ')
                                continue;

                            flightRecords.destination += record[i];
                        }

                        for (int i = 149; i <= 150; i++)
                        {
                            staggingUA.OperatingBookingClassCode += record[i];

                            if (record[i] == ' ')
                                continue;

                            flightRecords.bookingClass += record[i];
                        }

                        for (int i = 151; i <= 152; i++)
                        {
                            staggingUA.FlownCabinClassCode += record[i];

                            if (record[i] == ' ')
                                continue;

                            flightRecords.cabinClass += record[i];
                        }

                        for (int i = 153; i <= 154; i++)
                        {
                            staggingUA.OperatingRevenueBookingClassCode += record[i];
                        }

                        for (int i = 155; i <= 156; i++)
                        {
                            staggingUA.MarketingBookingClass += record[i];
                        }

                        for (int i = 157; i <= 158; i++)
                        {
                            staggingUA.UpgradeIndicator += record[i];
                        }

                        for (int i = 159; i <= 160; i++)
                        {
                            staggingUA.DowngradeIndicator += record[i];
                        }

                        for (int i = 161; i <= 175; i++)
                        {
                            staggingUA.EntitlementNumber += record[i];
                        }

                        for (int i = 176; i <= 188; i++)
                        {
                            staggingUA.TicketNumber += record[i];

                            if (record[i] == ' ')
                                continue;

                            flightRecords.ticketNo += record[i];
                        }

                        for (int i = 189; i <= 190; i++)
                        {
                            staggingUA.CouponNumber += record[i];

                            if (record[i] == ' ')
                                continue;

                            flightRecords.couponNo += record[i];
                        }

                        for (int i = 191; i <= 198; i++)
                        {
                            staggingUA.FareBasisCode += record[i];

                            if (record[i] == ' ')
                                continue;

                            flightRecords.fareBasis += record[i];
                        }

                        for (int i = 199; i <= 202; i++)
                        {
                            staggingUA.SeatNumber += record[i];
                        }

                        for (int i = 203; i <= 208; i++)
                        {
                            staggingUA.PNRNumber += record[i];

                            if (record[i] == ' ')
                                continue;

                            flightRecords.pnrNo += record[i];
                        }

                        staggingUA.UpdateCode = record[209].ToString();

                        for (int i = 210; i <= 214; i++)
                        {
                            staggingUA.BaseFlightMiles += record[i];
                        }

                        for (int i = 215; i <= 216; i++)
                        {
                            staggingUA.CheckInSourceCode += record[i];
                        }

                        for (int i = 217; i <= 218; i++)
                        {
                            staggingUA.BookingSourceCode += record[i];
                        }

                        for (int i = 219; i <= 234; i++)
                        {
                            staggingUA.OperatingAirlineAuthorizationNo += record[i];
                        }

                        for (int i = 235; i <= 250; i++)
                        {
                            staggingUA.InternalAirlineReferenceNoFFPAirline += record[i];
                        }

                        for (int i = 251; i <= 252; i++)
                        {
                            staggingUA.AccrualPostingStatus += record[i];
                        }

                        for (int i = 253; i <= 255; i++)
                        {
                            staggingUA.ResponseCode1 += record[i];
                        }

                        for (int i = 256; i <= 258; i++)
                        {
                            staggingUA.ResponseCode2 += record[i];
                        }

                        for (int i = 259; i <= 261; i++)
                        {
                            staggingUA.ResponseCode3 += record[i];
                        }

                        for (int i = 262; i <= 264; i++)
                        {
                            staggingUA.ResponseCode4 += record[i];
                        }

                        for (int i = 265; i <= 267; i++)
                        {
                            staggingUA.ResponseCode5 += record[i];
                        }

                        for (int i = 268; i <= 270; i++)
                        {
                            staggingUA.ResponseCode6 += record[i];
                        }

                        for (int i = 271; i <= 272; i++)
                        {
                            staggingUA.TierLever += record[i];
                        }

                        staggingUA.Gender = record[273].ToString();

                        for (int i = 274; i <= 399; i++)
                        {
                            staggingUA.Filler += record[i];
                        }

                        for (int i = 400; i <= 499; i++)
                        {
                            staggingUA.AirlineAdditionalInformation += record[i];
                        }

                        currentLine++;

                        //013
                        if (Validation.IsModelStateValid(flightRecords))
                        {
                            //002
                            if (header != "" && header == flightRecords.operatingAirline)
                            {
                                //015
                                if (staggingUA.NameCheckOverride == "Y" || staggingUA.NameCheckOverride == "N")
                                {
                                    //027
                                    if ((flightRecords.origin != "" && flightRecords.origin != null) ||
                                        flightRecords.destination != "" && flightRecords.destination != null)
                                    {
                                        //023
                                        if (flightRecords.transactionType == "01" || flightRecords.transactionType == "02" ||
                                            flightRecords.transactionType == "03" || flightRecords.transactionType == "04")
                                        {
                                            //039
                                            //if (seatNumber != string.Empty && seatNumber != null)
                                            //{
                                            //044
                                            if (flightRecords.marketingAirline != "" && flightRecords.marketingAirline != null)
                                            {
                                                //051
                                                if (staggingUA.FFPMemberNumber != string.Empty && staggingUA.FFPMemberNumber != null)
                                                {
                                                    entities.StaggingUAs.Add(staggingUA);
                                                    entities.Flight_Records.Add(flightRecords);
                                                }
                                                else
                                                {
                                                    staggingUA.Description = "051: Invalid FFP number";
                                                    staggingUA.DownloadCounter = 0;
                                                    entities.StaggingUAs.Add(staggingUA);
                                                }
                                            }
                                            else
                                            {
                                                staggingUA.Description = "044: Invalid marketing airline code";
                                                staggingUA.DownloadCounter = 0;
                                                entities.StaggingUAs.Add(staggingUA);
                                            }
                                        }
                                        else
                                        {
                                            staggingUA.Description = "023: Invalid transaction type";
                                            staggingUA.DownloadCounter = 0;
                                            entities.StaggingUAs.Add(staggingUA);
                                        }
                                    }
                                    else
                                    {
                                        staggingUA.Description = "027: Invalid origin and/or destination airport code";
                                        staggingUA.DownloadCounter = 0;
                                        entities.StaggingUAs.Add(staggingUA);
                                    }
                                }
                                else
                                {
                                    staggingUA.Description = "Invalid name check override.";
                                    staggingUA.DownloadCounter = 0;
                                    entities.StaggingUAs.Add(staggingUA);
                                }
                            }
                            else
                            {
                                staggingUA.Description = "Header is incorrect.";
                                staggingUA.DownloadCounter = 0;
                                entities.StaggingUAs.Add(staggingUA);
                            }
                        }
                        else
                        {
                            staggingUA.Description = "Mandatory fields data is missing or in invalid format";
                            staggingUA.DownloadCounter = 0;
                            entities.StaggingUAs.Add(staggingUA);
                        }
                    }

                    entities.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
            //catch (System.Data.Entity.Validation.DbEntityValidationException e)
            //{
            //    StreamWriter streamWriter = new StreamWriter(@"C:\Users\Aleksa\Desktop\greske.txt");
            //    foreach (var eve in e.EntityValidationErrors)
            //    {
            //        streamWriter.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
            //            eve.Entry.Entity.GetType().Name, eve.Entry.State);
            //        foreach (var ve in eve.ValidationErrors)
            //        {
            //            streamWriter.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
            //                ve.PropertyName, ve.ErrorMessage);
            //        }
            //    }
            //    return Request.CreateResponse(HttpStatusCode.InternalServerError);
            //}
            catch (Exception ex)
            {
                poruka = poruka + ex.Message;
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("api/FlightRecordsApi/Upload")]
        public HttpResponseMessage Upload()
        {
            if (ApiViewBag.UploadRequest.RequestIsComingFromController)
            {
                ApiViewBag.UploadRequest.RequestIsComingFromController = false;

                HttpPostedFileBase file = ApiViewBag.UploadRequest.RequestedFile;

                Stream stream = file.InputStream;

                if (file == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound);

                if (file.ContentLength == 0)
                    return Request.CreateResponse(HttpStatusCode.NoContent);

                var fileName = Path.GetFileName(file.FileName);

                var path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/UploadedFiles"), fileName);

                file.SaveAs(path);

                string failedToAddRecords = string.Empty;

                int recordsAdded = 0;
                int recordsNotAdded = 0;
                int numberOfFooterRecords = 0;
                int numberOfRecords = 0;

                try
                {
                    string tempRecord = string.Empty;

                    bool convertedFooterRecordsSuccessfully = false;

                    string header = string.Empty;
                    string[] headerArray = null;

                    string body = string.Empty;
                    string[] bodyArray = null;

                    string footer = string.Empty;
                    string[] footerArray = null;

                    char[] separator = new char[] { '|' };

                    Flight_Records record = new Flight_Records();

                    string content = string.Empty;

                    stream.Position = 0;

                    StreamReader streamReader1 = new StreamReader(stream);

                    header = streamReader1.ReadLine();

                    if (entities.FR_Batch_Files.Any(b => b.Header == header))
                        return Request.CreateResponse(HttpStatusCode.Conflict);

                    FR_Batch_Files batch = new FR_Batch_Files();

                    headerArray = header.Split(separator, StringSplitOptions.None);

                    if (headerArray[0].ToUpper() != "H")
                        return Request.CreateResponse(HttpStatusCode.NotAcceptable);

                    while (!streamReader1.EndOfStream)
                    {
                        footer = streamReader1.ReadLine();

                        footerArray = footer.Split(separator, StringSplitOptions.None);

                        numberOfRecords++;
                    }
                    numberOfRecords--;

                    if (footerArray[0] != "F")
                        return Request.CreateResponse(HttpStatusCode.NotAcceptable);

                    convertedFooterRecordsSuccessfully = int.TryParse(footerArray[1], out numberOfFooterRecords);

                    if (!convertedFooterRecordsSuccessfully)
                        return Request.CreateResponse(HttpStatusCode.BadRequest);

                    streamReader1.DiscardBufferedData();

                    stream.Position = 0;

                    System.IO.StreamReader streamReader = new System.IO.StreamReader(stream);

                    header = streamReader.ReadLine(); //H

                    headerArray = header.Split(separator, StringSplitOptions.None);

                    while (!streamReader.EndOfStream)
                    {
                        tempRecord = streamReader.ReadLine();

                        switch (tempRecord[0])
                        {

                            case 'R':
                                {
                                    bodyArray = tempRecord.Split(separator, StringSplitOptions.None);

                                    if (bodyArray[1] != string.Empty &&
                                        bodyArray[1].Length <= 16)
                                    {
                                        record.identifierNo = bodyArray[1];
                                    }
                                    else
                                    {
                                        record.identifierNo = null;
                                    }

                                    if (bodyArray[2] != string.Empty &&
                                        bodyArray[2].Length <= 2)
                                    {
                                        record.transactionType = bodyArray[2];
                                    }
                                    else
                                    {
                                        record.transactionType = string.Empty;

                                        recordsNotAdded++;

                                        failedToAddRecords = failedToAddRecords + tempRecord + "\n";

                                        continue;
                                    }

                                    if (bodyArray[3] != string.Empty &&
                                        bodyArray[3].Length <= 30)
                                    {
                                        record.otherFFPNo = bodyArray[3];
                                    }
                                    else
                                    {
                                        record.otherFFPNo = string.Empty;
                                    }

                                    if (bodyArray[4] != string.Empty &&
                                        bodyArray[4].Length <= 30)
                                    {
                                        record.otherFFPScheme = bodyArray[4];
                                    }
                                    else
                                    {
                                        record.otherFFPScheme = string.Empty;
                                    }

                                    if (bodyArray[5] != string.Empty &&
                                        bodyArray[5].Length <= 30)
                                    {
                                        record.firstName = bodyArray[5];
                                    }
                                    else
                                    {
                                        record.firstName = string.Empty;

                                        recordsNotAdded++;

                                        failedToAddRecords = failedToAddRecords + tempRecord + "\n";

                                        continue;
                                    }

                                    if (bodyArray[6] != string.Empty &&
                                        bodyArray[6].Length <= 30)
                                    {
                                        record.lastName = bodyArray[6];
                                    }
                                    else
                                    {
                                        record.lastName = string.Empty;

                                        recordsNotAdded++;

                                        failedToAddRecords = failedToAddRecords + tempRecord + "\n";

                                        continue;
                                    }

                                    if (bodyArray[7] != string.Empty &&
                                        bodyArray[7].Length <= 100)
                                    {
                                        record.partnerTransactionNo = bodyArray[7];
                                    }
                                    else
                                    {
                                        record.partnerTransactionNo = string.Empty;
                                    }

                                    if (bodyArray[8] != string.Empty)
                                    {
                                        record.bookingDate = Convert.ToDateTime(bodyArray[8]);
                                    }
                                    else
                                    {
                                        record.bookingDate = default(DateTime);
                                    }

                                    if (bodyArray[9] != string.Empty)
                                    {
                                        record.departureDate = Convert.ToDateTime(bodyArray[9]);
                                    }
                                    else
                                    {
                                        record.departureDate = default(DateTime);

                                        recordsNotAdded++;

                                        failedToAddRecords = failedToAddRecords + tempRecord + "\n";

                                        continue;
                                    }

                                    if (bodyArray[10] != string.Empty &&
                                        bodyArray[10].Length <= 3)
                                    {
                                        record.origin = bodyArray[10];
                                    }
                                    else
                                    {
                                        record.origin = string.Empty;

                                        recordsNotAdded++;

                                        failedToAddRecords = failedToAddRecords + tempRecord + "\n";

                                        continue;
                                    }

                                    if (bodyArray[11] != string.Empty &&
                                        bodyArray[11].Length <= 3)
                                    {
                                        record.destination = bodyArray[11];
                                    }
                                    else
                                    {
                                        record.destination = string.Empty;

                                        recordsNotAdded++;

                                        failedToAddRecords = failedToAddRecords + tempRecord + "\n";

                                        continue;
                                    }

                                    if (bodyArray[12] != string.Empty &&
                                        bodyArray[12].Length <= 2)
                                    {
                                        record.bookingClass = bodyArray[12];
                                    }
                                    else
                                    {
                                        record.bookingClass = string.Empty;

                                        recordsNotAdded++;

                                        failedToAddRecords = failedToAddRecords + tempRecord + "\n";

                                        continue;
                                    }

                                    if (bodyArray[13] != string.Empty &&
                                        bodyArray[13].Length <= 1)
                                    {
                                        record.cabinClass = bodyArray[13];
                                    }
                                    else
                                    {
                                        record.cabinClass = string.Empty;
                                    }

                                    if (bodyArray[14] != string.Empty &&
                                        Information.IsNumeric(bodyArray[14]) &&
                                        bodyArray[14].Length <= 4)
                                    {
                                        record.marketingFlightNo = bodyArray[14];
                                    }
                                    else
                                    {
                                        record.marketingFlightNo = string.Empty;

                                        recordsNotAdded++;

                                        failedToAddRecords = failedToAddRecords + tempRecord + "\n";

                                        continue;
                                    }

                                    if (bodyArray[15] != string.Empty &&
                                        bodyArray[15].Length <= 2)
                                    {
                                        record.marketingAirline = bodyArray[15];
                                    }
                                    else
                                    {
                                        record.marketingAirline = string.Empty;

                                        recordsNotAdded++;

                                        failedToAddRecords = failedToAddRecords + tempRecord + "\n";

                                        continue;
                                    }

                                    if (bodyArray[16] != string.Empty &&
                                        Information.IsNumeric(bodyArray[16]) &&
                                        bodyArray[16].Length <= 4)
                                    {
                                        record.operatingFlightNo = bodyArray[16];
                                    }
                                    else
                                    {
                                        record.operatingFlightNo = string.Empty;

                                        recordsNotAdded++;

                                        failedToAddRecords = failedToAddRecords + tempRecord + "\n";

                                        continue;
                                    }

                                    if (bodyArray[17] != string.Empty &&
                                        bodyArray[17].Length <= 2)
                                    {
                                        record.operatingAirline = bodyArray[17];
                                    }
                                    else
                                    {
                                        record.operatingAirline = string.Empty;

                                        recordsNotAdded++;

                                        failedToAddRecords = failedToAddRecords + tempRecord + "\n";

                                        continue;
                                    }

                                    if (bodyArray[18] != string.Empty &&
                                        (bodyArray[18].Length == 13 || bodyArray[18].Length == 14) &&
                                        Information.IsNumeric(bodyArray[18]))
                                    {
                                        record.ticketNo = bodyArray[18];
                                    }
                                    else
                                    {
                                        record.ticketNo = string.Empty;
                                    }

                                    if (bodyArray[19] != string.Empty &&
                                        bodyArray[19].Length <= 25)
                                    {
                                        record.externalPaxID = bodyArray[19];
                                    }
                                    else
                                    {
                                        record.externalPaxID = string.Empty;
                                    }

                                    if (bodyArray[20] != string.Empty &&
                                        bodyArray[20].Length <= 2)
                                    {
                                        record.couponNo = bodyArray[20];
                                    }
                                    else
                                    {
                                        record.couponNo = string.Empty;
                                    }

                                    if (bodyArray[21] != string.Empty &&
                                        bodyArray[21].Length == 6 &&
                                        char.IsLetterOrDigit(bodyArray[21][0]) &&
                                        char.IsLetterOrDigit(bodyArray[21][1]) &&
                                        char.IsLetterOrDigit(bodyArray[21][2]) &&
                                        char.IsLetterOrDigit(bodyArray[21][3]) &&
                                        char.IsLetterOrDigit(bodyArray[21][4]) &&
                                        char.IsLetterOrDigit(bodyArray[21][5]))
                                    {
                                        record.pnrNo = bodyArray[21];
                                    }
                                    else
                                    {
                                        record.pnrNo = string.Empty;

                                        recordsNotAdded++;

                                        failedToAddRecords = failedToAddRecords + tempRecord + "\n";

                                        continue;
                                    }

                                    if (bodyArray[22] != string.Empty &&
                                        bodyArray[22].Length <= 5)
                                    {
                                        record.distance = Convert.ToInt64(bodyArray[22]);
                                    }
                                    else
                                    {
                                        record.distance = default(long);
                                    }

                                    if (bodyArray[23] != string.Empty &&
                                        bodyArray[23].Length <= 8)
                                    {
                                        record.baseFare = Convert.ToSingle(bodyArray[23]);
                                    }
                                    else
                                    {
                                        record.baseFare = default(double);
                                    }

                                    if (bodyArray[24] != string.Empty &&
                                        bodyArray[24].Length <= 8)
                                    {
                                        record.discountBase = Convert.ToSingle(bodyArray[24]);
                                    }
                                    else
                                    {
                                        record.discountBase = default(double);
                                    }

                                    if (bodyArray[25] != string.Empty &&
                                        bodyArray[25].Length <= 8)
                                    {
                                        record.exciseTax = Convert.ToSingle(bodyArray[25]);
                                    }
                                    else
                                    {
                                        record.exciseTax = default(double);
                                    }

                                    if (bodyArray[26] != string.Empty &&
                                        bodyArray[26].Length <= 1 &&
                                        (char.ToUpper(bodyArray[26][0]) == 'A' || char.ToUpper(bodyArray[26][0]) == 'C' || char.ToUpper(bodyArray[26][0]) == 'I'))
                                    {
                                        record.customerType = bodyArray[26];
                                    }
                                    else
                                    {
                                        record.customerType = string.Empty;
                                    }

                                    if (bodyArray[27] != string.Empty &&
                                        bodyArray[27].Length <= 100)
                                    {
                                        record.promotionCode = bodyArray[27];
                                    }
                                    else
                                    {
                                        record.promotionCode = string.Empty;
                                    }

                                    if (bodyArray[28] != string.Empty &&
                                        bodyArray[28].Length <= 3)
                                    {
                                        record.ticketCurrency = bodyArray[28];
                                    }
                                    else
                                    {
                                        record.ticketCurrency = string.Empty;
                                    }

                                    if (bodyArray[29] != string.Empty &&
                                        bodyArray[29].Length <= 3)
                                    {
                                        record.targetCurrency = bodyArray[29];
                                    }
                                    else
                                    {
                                        record.targetCurrency = string.Empty;
                                    }

                                    if (bodyArray[30] != string.Empty &&
                                        bodyArray[30].Length <= 10)
                                    {
                                        record.exchangeRate = Convert.ToSingle(bodyArray[30]);
                                    }
                                    else
                                    {
                                        record.exchangeRate = default(double);
                                    }

                                    if (bodyArray[31] != string.Empty &&
                                        bodyArray[31].Length <= 10)
                                    {
                                        record.fareBasis = bodyArray[31];
                                    }
                                    else
                                    {
                                        record.fareBasis = string.Empty;
                                    }

                                    if (bodyArray[17] == "EI")
                                    {
                                        if (bodyArray[28] != string.Empty &&
                                            bodyArray[29] != string.Empty &&
                                            bodyArray[30] != string.Empty)
                                        {
                                            if (numberOfRecords == numberOfFooterRecords)
                                            {
                                                if (record.ticketNo != string.Empty || record.externalPaxID != string.Empty)
                                                {
                                                    if (record.ticketNo != string.Empty)
                                                    {
                                                        if (Validation.TicketNoValidation(record) != null)
                                                        {
                                                            failedToAddRecords = failedToAddRecords + tempRecord + "\n";
                                                            recordsNotAdded++;
                                                            continue;
                                                        }

                                                        Validation.SetEmptyPropertiesToNull(record);
                                                        recordsAdded++;
                                                        entities.Flight_Records.Add(record);
                                                        entities.SaveChanges();
                                                    }
                                                    else if (record.externalPaxID != string.Empty)
                                                    {
                                                        if (Validation.ExternalPaxIDValidation(record) != null)
                                                        {
                                                            failedToAddRecords = failedToAddRecords + tempRecord + "\n";
                                                            recordsNotAdded++;
                                                            continue;
                                                        }

                                                        Validation.SetEmptyPropertiesToNull(record);
                                                        recordsAdded++;
                                                        entities.Flight_Records.Add(record);
                                                        entities.SaveChanges();
                                                    }
                                                    else
                                                    {
                                                        failedToAddRecords = failedToAddRecords + tempRecord + "\n";
                                                        recordsNotAdded++;
                                                        continue;
                                                    }
                                                }
                                                else
                                                {
                                                    failedToAddRecords = failedToAddRecords + tempRecord + "\n";
                                                    recordsNotAdded++;
                                                    continue;
                                                }
                                            }
                                            else return Request.CreateResponse(HttpStatusCode.PreconditionFailed);
                                        }
                                        else
                                        {
                                            recordsNotAdded++;
                                            failedToAddRecords = failedToAddRecords + tempRecord + "\n";
                                        }
                                    }
                                    else if (bodyArray[17] != "EI")
                                    {
                                        if (numberOfRecords == numberOfFooterRecords)
                                        {
                                            if (record.ticketNo != string.Empty || record.externalPaxID != string.Empty)
                                            {
                                                if (record.ticketNo != string.Empty)
                                                {
                                                    if (Validation.TicketNoValidation(record) != null)
                                                    {
                                                        failedToAddRecords = failedToAddRecords + tempRecord + "\n";
                                                        recordsNotAdded++;
                                                        continue;
                                                    }

                                                    Validation.SetEmptyPropertiesToNull(record);
                                                    recordsAdded++;
                                                    entities.Flight_Records.Add(record);
                                                    entities.SaveChanges();
                                                }
                                                else if (record.externalPaxID != string.Empty)
                                                {
                                                    if (Validation.ExternalPaxIDValidation(record) != null)
                                                    {
                                                        failedToAddRecords = failedToAddRecords + tempRecord + "\n";
                                                        recordsNotAdded++;
                                                        continue;
                                                    }

                                                    Validation.SetEmptyPropertiesToNull(record);
                                                    recordsAdded++;
                                                    entities.Flight_Records.Add(record);
                                                    entities.SaveChanges();
                                                }
                                                else
                                                {
                                                    recordsNotAdded++;
                                                    failedToAddRecords = failedToAddRecords + tempRecord + "\n";
                                                    continue;
                                                }
                                            }
                                            else
                                            {
                                                failedToAddRecords = failedToAddRecords + tempRecord + "\n";
                                                recordsNotAdded++;
                                                continue;
                                            }
                                        }
                                        else return Request.CreateResponse(HttpStatusCode.PreconditionFailed);
                                    }
                                    continue;
                                }

                            case 'F':
                                {
                                    break;
                                }
                            default:
                                {
                                    failedToAddRecords = failedToAddRecords + tempRecord + "\n";

                                    recordsNotAdded++;

                                    continue;
                                }
                        }
                    }

                    streamReader.DiscardBufferedData();

                    stream.Position = 0;

                    StreamReader streamReader2 = new StreamReader(stream);

                    batch.Header = header;
                    batch.Footer = footer;
                    batch.Content = streamReader2.ReadToEnd();

                    entities.FR_Batch_Files.Add(batch);
                    entities.SaveChanges();

                    FailedRecords = failedToAddRecords;

                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                catch (Exception e)
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError);
                }
            }
            else
            {
                var currentRequest = HttpContext.Current;

                var file = currentRequest.Request.Files[0];

                Stream stream = file.InputStream;

                if (file == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound);

                if (file.ContentLength == 0)
                    return Request.CreateResponse(HttpStatusCode.NoContent);

                string failedToAddRecords = string.Empty;

                int recordsAdded = 0;
                int recordsNotAdded = 0;
                int numberOfFooterRecords = 0;
                int numberOfRecords = 0;

                try
                {
                    string tempRecord = string.Empty;

                    bool convertedFooterRecordsSuccessfully = false;

                    string header = string.Empty;
                    string[] headerArray = null;

                    string body = string.Empty;
                    string[] bodyArray = null;

                    string footer = string.Empty;
                    string[] footerArray = null;

                    char[] separator = new char[] { '|' };

                    Flight_Records record = new Flight_Records();

                    string content = string.Empty;

                    stream.Position = 0;

                    StreamReader streamReader1 = new StreamReader(stream);

                    header = streamReader1.ReadLine();

                    if (entities.FR_Batch_Files.Any(b => b.Header == header))
                        return Request.CreateResponse(HttpStatusCode.Conflict);

                    FR_Batch_Files batch = new FR_Batch_Files();

                    headerArray = header.Split(separator, StringSplitOptions.None);

                    if (headerArray[0].ToUpper() != "H")
                        return Request.CreateResponse(HttpStatusCode.NotAcceptable);

                    while (!streamReader1.EndOfStream)
                    {
                        footer = streamReader1.ReadLine();

                        footerArray = footer.Split(separator, StringSplitOptions.None);

                        numberOfRecords++;
                    }
                    numberOfRecords--;

                    if (footerArray[0] != "F")
                        return Request.CreateResponse(HttpStatusCode.NotAcceptable);

                    convertedFooterRecordsSuccessfully = int.TryParse(footerArray[1], out numberOfFooterRecords);

                    if (!convertedFooterRecordsSuccessfully)
                        return Request.CreateResponse(HttpStatusCode.BadRequest);

                    streamReader1.DiscardBufferedData();

                    stream.Position = 0;

                    System.IO.StreamReader streamReader = new System.IO.StreamReader(stream);

                    header = streamReader.ReadLine(); //H

                    headerArray = header.Split(separator, StringSplitOptions.None);

                    while (!streamReader.EndOfStream)
                    {
                        tempRecord = streamReader.ReadLine();

                        switch (tempRecord[0])
                        {

                            case 'R':
                                {
                                    bodyArray = tempRecord.Split(separator, StringSplitOptions.None);

                                    if (bodyArray[1] != string.Empty &&
                                        bodyArray[1].Length <= 16)
                                    {
                                        record.identifierNo = bodyArray[1];
                                    }
                                    else
                                    {
                                        record.identifierNo = null;
                                    }

                                    if (bodyArray[2] != string.Empty &&
                                        bodyArray[2].Length <= 2)
                                    {
                                        record.transactionType = bodyArray[2];
                                    }
                                    else
                                    {
                                        record.transactionType = string.Empty;

                                        recordsNotAdded++;

                                        failedToAddRecords = failedToAddRecords + tempRecord + "\n";

                                        continue;
                                    }

                                    if (bodyArray[3] != string.Empty &&
                                        bodyArray[3].Length <= 30)
                                    {
                                        record.otherFFPNo = bodyArray[3];
                                    }
                                    else
                                    {
                                        record.otherFFPNo = string.Empty;
                                    }

                                    if (bodyArray[4] != string.Empty &&
                                        bodyArray[4].Length <= 30)
                                    {
                                        record.otherFFPScheme = bodyArray[4];
                                    }
                                    else
                                    {
                                        record.otherFFPScheme = string.Empty;
                                    }

                                    if (bodyArray[5] != string.Empty &&
                                        bodyArray[5].Length <= 30)
                                    {
                                        record.firstName = bodyArray[5];
                                    }
                                    else
                                    {
                                        record.firstName = string.Empty;

                                        recordsNotAdded++;

                                        failedToAddRecords = failedToAddRecords + tempRecord + "\n";

                                        continue;
                                    }

                                    if (bodyArray[6] != string.Empty &&
                                        bodyArray[6].Length <= 30)
                                    {
                                        record.lastName = bodyArray[6];
                                    }
                                    else
                                    {
                                        record.lastName = string.Empty;

                                        recordsNotAdded++;

                                        failedToAddRecords = failedToAddRecords + tempRecord + "\n";

                                        continue;
                                    }

                                    if (bodyArray[7] != string.Empty &&
                                        bodyArray[7].Length <= 100)
                                    {
                                        record.partnerTransactionNo = bodyArray[7];
                                    }
                                    else
                                    {
                                        record.partnerTransactionNo = string.Empty;
                                    }

                                    if (bodyArray[8] != string.Empty)
                                    {
                                        record.bookingDate = Convert.ToDateTime(bodyArray[8]);
                                    }
                                    else
                                    {
                                        record.bookingDate = default(DateTime);
                                    }

                                    if (bodyArray[9] != string.Empty)
                                    {
                                        record.departureDate = Convert.ToDateTime(bodyArray[9]);
                                    }
                                    else
                                    {
                                        record.departureDate = default(DateTime);

                                        recordsNotAdded++;

                                        failedToAddRecords = failedToAddRecords + tempRecord + "\n";

                                        continue;
                                    }

                                    if (bodyArray[10] != string.Empty &&
                                        bodyArray[10].Length <= 3)
                                    {
                                        record.origin = bodyArray[10];
                                    }
                                    else
                                    {
                                        record.origin = string.Empty;

                                        recordsNotAdded++;

                                        failedToAddRecords = failedToAddRecords + tempRecord + "\n";

                                        continue;
                                    }

                                    if (bodyArray[11] != string.Empty &&
                                        bodyArray[11].Length <= 3)
                                    {
                                        record.destination = bodyArray[11];
                                    }
                                    else
                                    {
                                        record.destination = string.Empty;

                                        recordsNotAdded++;

                                        failedToAddRecords = failedToAddRecords + tempRecord + "\n";

                                        continue;
                                    }

                                    if (bodyArray[12] != string.Empty &&
                                        bodyArray[12].Length <= 2)
                                    {
                                        record.bookingClass = bodyArray[12];
                                    }
                                    else
                                    {
                                        record.bookingClass = string.Empty;

                                        recordsNotAdded++;

                                        failedToAddRecords = failedToAddRecords + tempRecord + "\n";

                                        continue;
                                    }

                                    if (bodyArray[13] != string.Empty &&
                                        bodyArray[13].Length <= 1)
                                    {
                                        record.cabinClass = bodyArray[13];
                                    }
                                    else
                                    {
                                        record.cabinClass = string.Empty;
                                    }

                                    if (bodyArray[14] != string.Empty &&
                                        Information.IsNumeric(bodyArray[14]) &&
                                        bodyArray[14].Length <= 4)
                                    {
                                        record.marketingFlightNo = bodyArray[14];
                                    }
                                    else
                                    {
                                        record.marketingFlightNo = string.Empty;

                                        recordsNotAdded++;

                                        failedToAddRecords = failedToAddRecords + tempRecord + "\n";

                                        continue;
                                    }

                                    if (bodyArray[15] != string.Empty &&
                                        bodyArray[15].Length <= 2)
                                    {
                                        record.marketingAirline = bodyArray[15];
                                    }
                                    else
                                    {
                                        record.marketingAirline = string.Empty;

                                        recordsNotAdded++;

                                        failedToAddRecords = failedToAddRecords + tempRecord + "\n";

                                        continue;
                                    }

                                    if (bodyArray[16] != string.Empty &&
                                        Information.IsNumeric(bodyArray[16]) &&
                                        bodyArray[16].Length <= 4)
                                    {
                                        record.operatingFlightNo = bodyArray[16];
                                    }
                                    else
                                    {
                                        record.operatingFlightNo = string.Empty;

                                        recordsNotAdded++;

                                        failedToAddRecords = failedToAddRecords + tempRecord + "\n";

                                        continue;
                                    }

                                    if (bodyArray[17] != string.Empty &&
                                        bodyArray[17].Length <= 2)
                                    {
                                        record.operatingAirline = bodyArray[17];
                                    }
                                    else
                                    {
                                        record.operatingAirline = string.Empty;

                                        recordsNotAdded++;

                                        failedToAddRecords = failedToAddRecords + tempRecord + "\n";

                                        continue;
                                    }

                                    if (bodyArray[18] != string.Empty &&
                                        (bodyArray[18].Length == 13 || bodyArray[18].Length == 14) &&
                                        Information.IsNumeric(bodyArray[18]))
                                    {
                                        record.ticketNo = bodyArray[18];
                                    }
                                    else
                                    {
                                        record.ticketNo = string.Empty;
                                    }

                                    if (bodyArray[19] != string.Empty &&
                                        bodyArray[19].Length <= 25)
                                    {
                                        record.externalPaxID = bodyArray[19];
                                    }
                                    else
                                    {
                                        record.externalPaxID = string.Empty;
                                    }

                                    if (bodyArray[20] != string.Empty &&
                                        bodyArray[20].Length <= 2)
                                    {
                                        record.couponNo = bodyArray[20];
                                    }
                                    else
                                    {
                                        record.couponNo = string.Empty;
                                    }

                                    if (bodyArray[21] != string.Empty &&
                                        bodyArray[21].Length == 6 &&
                                        char.IsLetterOrDigit(bodyArray[21][0]) &&
                                        char.IsLetterOrDigit(bodyArray[21][1]) &&
                                        char.IsLetterOrDigit(bodyArray[21][2]) &&
                                        char.IsLetterOrDigit(bodyArray[21][3]) &&
                                        char.IsLetterOrDigit(bodyArray[21][4]) &&
                                        char.IsLetterOrDigit(bodyArray[21][5]))
                                    {
                                        record.pnrNo = bodyArray[21];
                                    }
                                    else
                                    {
                                        record.pnrNo = string.Empty;

                                        recordsNotAdded++;

                                        failedToAddRecords = failedToAddRecords + tempRecord + "\n";

                                        continue;
                                    }

                                    if (bodyArray[22] != string.Empty &&
                                        bodyArray[22].Length <= 5)
                                    {
                                        record.distance = Convert.ToInt64(bodyArray[22]);
                                    }
                                    else
                                    {
                                        record.distance = default(long);
                                    }

                                    if (bodyArray[23] != string.Empty &&
                                        bodyArray[23].Length <= 8)
                                    {
                                        record.baseFare = Convert.ToSingle(bodyArray[23]);
                                    }
                                    else
                                    {
                                        record.baseFare = default(double);
                                    }

                                    if (bodyArray[24] != string.Empty &&
                                        bodyArray[24].Length <= 8)
                                    {
                                        record.discountBase = Convert.ToSingle(bodyArray[24]);
                                    }
                                    else
                                    {
                                        record.discountBase = default(double);
                                    }

                                    if (bodyArray[25] != string.Empty &&
                                        bodyArray[25].Length <= 8)
                                    {
                                        record.exciseTax = Convert.ToSingle(bodyArray[25]);
                                    }
                                    else
                                    {
                                        record.exciseTax = default(double);
                                    }

                                    if (bodyArray[26] != string.Empty &&
                                        bodyArray[26].Length <= 1 &&
                                        (char.ToUpper(bodyArray[26][0]) == 'A' || char.ToUpper(bodyArray[26][0]) == 'C' || char.ToUpper(bodyArray[26][0]) == 'I'))
                                    {
                                        record.customerType = bodyArray[26];
                                    }
                                    else
                                    {
                                        record.customerType = string.Empty;
                                    }

                                    if (bodyArray[27] != string.Empty &&
                                        bodyArray[27].Length <= 100)
                                    {
                                        record.promotionCode = bodyArray[27];
                                    }
                                    else
                                    {
                                        record.promotionCode = string.Empty;
                                    }

                                    if (bodyArray[28] != string.Empty &&
                                        bodyArray[28].Length <= 3)
                                    {
                                        record.ticketCurrency = bodyArray[28];
                                    }
                                    else
                                    {
                                        record.ticketCurrency = string.Empty;
                                    }

                                    if (bodyArray[29] != string.Empty &&
                                        bodyArray[29].Length <= 3)
                                    {
                                        record.targetCurrency = bodyArray[29];
                                    }
                                    else
                                    {
                                        record.targetCurrency = string.Empty;
                                    }

                                    if (bodyArray[30] != string.Empty &&
                                        bodyArray[30].Length <= 10)
                                    {
                                        record.exchangeRate = Convert.ToSingle(bodyArray[30]);
                                    }
                                    else
                                    {
                                        record.exchangeRate = default(double);
                                    }

                                    if (bodyArray[31] != string.Empty &&
                                        bodyArray[31].Length <= 10)
                                    {
                                        record.fareBasis = bodyArray[31];
                                    }
                                    else
                                    {
                                        record.fareBasis = string.Empty;
                                    }

                                    if (bodyArray[17] == "EI")
                                    {
                                        if (bodyArray[28] != string.Empty &&
                                            bodyArray[29] != string.Empty &&
                                            bodyArray[30] != string.Empty)
                                        {
                                            if (numberOfRecords == numberOfFooterRecords)
                                            {
                                                if (record.ticketNo != string.Empty || record.externalPaxID != string.Empty)
                                                {
                                                    if (record.ticketNo != string.Empty)
                                                    {
                                                        if (Validation.TicketNoValidation(record) != null)
                                                        {
                                                            failedToAddRecords = failedToAddRecords + tempRecord + "\n";
                                                            recordsNotAdded++;
                                                            continue;
                                                        }

                                                        Validation.SetEmptyPropertiesToNull(record);
                                                        recordsAdded++;
                                                        entities.Flight_Records.Add(record);
                                                        entities.SaveChanges();
                                                    }
                                                    else if (record.externalPaxID != string.Empty)
                                                    {
                                                        if (Validation.ExternalPaxIDValidation(record) != null)
                                                        {
                                                            failedToAddRecords = failedToAddRecords + tempRecord + "\n";
                                                            recordsNotAdded++;
                                                            continue;
                                                        }

                                                        Validation.SetEmptyPropertiesToNull(record);
                                                        recordsAdded++;
                                                        entities.Flight_Records.Add(record);
                                                        entities.SaveChanges();
                                                    }
                                                    else
                                                    {
                                                        failedToAddRecords = failedToAddRecords + tempRecord + "\n";
                                                        recordsNotAdded++;
                                                        continue;
                                                    }
                                                }
                                                else
                                                {
                                                    failedToAddRecords = failedToAddRecords + tempRecord + "\n";
                                                    recordsNotAdded++;
                                                    continue;
                                                }
                                            }
                                            else return Request.CreateResponse(HttpStatusCode.PreconditionFailed);
                                        }
                                        else
                                        {
                                            recordsNotAdded++;
                                            failedToAddRecords = failedToAddRecords + tempRecord + "\n";
                                        }
                                    }
                                    else if (bodyArray[17] != "EI")
                                    {
                                        if (numberOfRecords == numberOfFooterRecords)
                                        {
                                            if (record.ticketNo != string.Empty || record.externalPaxID != string.Empty)
                                            {
                                                if (record.ticketNo != string.Empty)
                                                {
                                                    if (Validation.TicketNoValidation(record) != null)
                                                    {
                                                        failedToAddRecords = failedToAddRecords + tempRecord + "\n";
                                                        recordsNotAdded++;
                                                        continue;
                                                    }

                                                    Validation.SetEmptyPropertiesToNull(record);
                                                    recordsAdded++;
                                                    entities.Flight_Records.Add(record);
                                                    entities.SaveChanges();
                                                }
                                                else if (record.externalPaxID != string.Empty)
                                                {
                                                    if (Validation.ExternalPaxIDValidation(record) != null)
                                                    {
                                                        failedToAddRecords = failedToAddRecords + tempRecord + "\n";
                                                        recordsNotAdded++;
                                                        continue;
                                                    }

                                                    Validation.SetEmptyPropertiesToNull(record);
                                                    recordsAdded++;
                                                    entities.Flight_Records.Add(record);
                                                    entities.SaveChanges();
                                                }
                                                else
                                                {
                                                    recordsNotAdded++;
                                                    failedToAddRecords = failedToAddRecords + tempRecord + "\n";
                                                    continue;
                                                }
                                            }
                                            else
                                            {
                                                failedToAddRecords = failedToAddRecords + tempRecord + "\n";
                                                recordsNotAdded++;
                                                continue;
                                            }
                                        }
                                        else return Request.CreateResponse(HttpStatusCode.PreconditionFailed);
                                    }
                                    continue;
                                }

                            case 'F':
                                {
                                    break;
                                }
                            default:
                                {
                                    failedToAddRecords = failedToAddRecords + tempRecord + "\n";

                                    recordsNotAdded++;

                                    continue;
                                }
                        }
                    }

                    streamReader.DiscardBufferedData();

                    stream.Position = 0;

                    StreamReader streamReader2 = new StreamReader(stream);

                    batch.Header = header;
                    batch.Footer = footer;
                    batch.Content = streamReader2.ReadToEnd();

                    entities.FR_Batch_Files.Add(batch);
                    entities.SaveChanges();

                    FailedRecords = failedToAddRecords;

                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                catch (Exception ex)
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError);
                }
            }
        }

        [HttpPost]
        [Route("api/FlightRecordsApi/AddFlightRecord")]
        // POST: api/FlightRecordsApi/AddFlightRecord
        public async Task<HttpResponseMessage> AddFlightRecord([FromBody] Flight_Records sfr)
        {
            if (!string.IsNullOrEmpty(sfr.ticketNo) || !string.IsNullOrEmpty(sfr.externalPaxID))
            {
                if (!string.IsNullOrEmpty(sfr.ticketNo))
                {
                    if (Validation.TicketNoValidation(sfr) == null)
                    {
                        entities.Flight_Records.Add(sfr);
                        await entities.SaveChangesAsync();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                    else return Request.CreateResponse(HttpStatusCode.Conflict);
                }
                else if (!string.IsNullOrEmpty(sfr.externalPaxID))
                {
                    if (Validation.ExternalPaxIDValidation(sfr) == null)
                    {
                        entities.Flight_Records.Add(sfr);
                        await entities.SaveChangesAsync();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                    else return Request.CreateResponse(HttpStatusCode.Conflict);
                }
                else return Request.CreateResponse(HttpStatusCode.Conflict);
            }
            else return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        [HttpPut]
        public HttpResponseMessage EditFlightRecord(int id, Flight_Records record)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Request.CreateResponse(HttpStatusCode.BadRequest);

                var recordInDatabase = entities.Flight_Records.SingleOrDefault(f => f.ID == id);

                if (recordInDatabase == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound);

                recordInDatabase.firstName = record.firstName;
                recordInDatabase.lastName = record.lastName;
                recordInDatabase.identifierNo = record.identifierNo;
                recordInDatabase.transactionType = record.transactionType;
                recordInDatabase.otherFFPNo = record.otherFFPNo;
                recordInDatabase.otherFFPScheme = record.otherFFPScheme;
                recordInDatabase.partnerTransactionNo = record.partnerTransactionNo;
                recordInDatabase.bookingDate = record.bookingDate;
                recordInDatabase.departureDate = record.departureDate;
                recordInDatabase.origin = record.origin;
                recordInDatabase.destination = record.destination;
                recordInDatabase.bookingClass = record.bookingClass;
                recordInDatabase.cabinClass = record.cabinClass;
                recordInDatabase.marketingFlightNo = record.marketingFlightNo;
                recordInDatabase.marketingAirline = record.marketingAirline;
                recordInDatabase.operatingFlightNo = record.operatingFlightNo;
                recordInDatabase.operatingAirline = record.operatingAirline;
                recordInDatabase.externalPaxID = record.externalPaxID;
                recordInDatabase.ticketNo = record.ticketNo;
                recordInDatabase.couponNo = record.couponNo;
                recordInDatabase.pnrNo = record.pnrNo;
                recordInDatabase.distance = record.distance;
                recordInDatabase.baseFare = record.baseFare;
                recordInDatabase.discountBase = record.discountBase;
                recordInDatabase.exciseTax = record.exciseTax;
                recordInDatabase.customerType = record.customerType;
                recordInDatabase.promotionCode = record.promotionCode;
                recordInDatabase.ticketCurrency = record.ticketCurrency;
                recordInDatabase.targetCurrency = record.targetCurrency;
                recordInDatabase.exchangeRate = record.exchangeRate;
                recordInDatabase.fareBasis = record.fareBasis;

                //entities.SaveChanges();

                //var journeyInDatabase = entities.Journeys.SingleOrDefault(j => j.TicketNo == record.ticketNo);

                //if(journeyInDatabase != null)
                //{
                //    journeyInDatabase.IdentifierNo = record.identifierNo;
                //    journeyInDatabase.FirstName = record.firstName;
                //    journeyInDatabase.LastName = record.lastName;
                //    journeyInDatabase.TicketNo = record.ticketNo;

                //    //entities.SaveChanges();

                //    var journeySegmentInDatabase = entities.JourneySegments.SingleOrDefault(js => js.IDFR == record.ID && js.couponNo == record.couponNo); //ako ne radi skloniti drugi deo

                //    if(journeySegmentInDatabase != null)
                //    {
                //        journeySegmentInDatabase.couponNo = record.couponNo;
                //        journeySegmentInDatabase.departureDate = record.departureDate;
                //        journeySegmentInDatabase.destination = record.destination;
                //        journeySegmentInDatabase.origin = record.origin;
                //        journeySegmentInDatabase.IDFR = record.ID;
                //        journeySegmentInDatabase.IDJourney = journeyInDatabase.ID;

                //        //entities.SaveChanges();
                //    }
                //}

                if (!ModelState.IsValid)
                    return Request.CreateResponse(HttpStatusCode.BadRequest);

                entities.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        public HttpResponseMessage DeleteFlightRecord(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Request.CreateResponse(HttpStatusCode.BadRequest);

                var recordInDatabase = entities.Flight_Records.SingleOrDefault(f => f.ID == id);

                if (recordInDatabase == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound);

                entities.Flight_Records.Remove(recordInDatabase);
                entities.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }
}
