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
using System.Text;

namespace AerLingus.Controllers.Api
{
    public class FlightRecordsApiController : ApiController
    {
        //private List<Flight_Records> list;
        private AerLingus_databaseEntities entities;

        //public Predicate<T> Or<T>(params Predicate<T>[] predicates)
        //{
        //    return delegate (T item)
        //    {
        //        foreach (Predicate<T> predicate in predicates)
        //        {
        //            if (predicate(item))
        //            {
        //                return true;
        //            }
        //        }
        //        return false;
        //    };
        //}

        //public Predicate<T> And<T>(params Predicate<T>[] predicates)
        //{
        //    return delegate (T item)
        //    {
        //        foreach (Predicate<T> predicate in predicates)
        //        {
        //            if (!predicate(item))
        //            {
        //                return false;
        //            }
        //        }
        //        return true;
        //    };
        //}

        public FlightRecordsApiController()
        {
            //list = entities.Flight_Records.ToList();
            entities = new AerLingus_databaseEntities();
        }

        [System.Web.Http.HttpGet]
        public IEnumerable<Flight_Records> GetFlightRecords()
        {
            return entities.Flight_Records.ToList();
        }

        [System.Web.Http.HttpGet]
        public IHttpActionResult GetFlightRecord(int id)
        {
            var flightRecord = entities.Flight_Records.SingleOrDefault(fr => fr.ID == id);

            if (flightRecord == null)
                return NotFound();

            return Ok(flightRecord);
        }

        [System.Web.Http.HttpPost]
        public HttpResponseMessage Upload(HttpPostedFileBase file)
        {
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

                numberOfFooterRecords = Convert.ToInt32(footerArray[1]);

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

                                    recordsNotAdded++;

                                    failedToAddRecords = failedToAddRecords + tempRecord + "\n";

                                    continue;
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
                                    record.baseFare = default(float);
                                }

                                if (bodyArray[24] != string.Empty &&
                                    bodyArray[24].Length <= 8)
                                {
                                    record.discountBase = Convert.ToSingle(bodyArray[24]);
                                }
                                else
                                {
                                    record.discountBase = default(float);
                                }

                                if (bodyArray[25] != string.Empty &&
                                    bodyArray[25].Length <= 8)
                                {
                                    record.exciseTax = Convert.ToSingle(bodyArray[25]);
                                }
                                else
                                {
                                    record.exciseTax = default(float);
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
                                    record.exchangeRate = default(float);
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
                                            recordsAdded++;
                                            entities.Flight_Records.Add(record);
                                            entities.SaveChanges();
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
                                        recordsAdded++;
                                        entities.Flight_Records.Add(record);
                                        entities.SaveChanges();
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
                                failedToAddRecords = failedToAddRecords + tempRecord + "[Default]" + "\n";

                                recordsNotAdded++;

                                continue;
                            }
                    }
                }

                streamReader.DiscardBufferedData();

                stream.Position = 0;

                StreamReader streamReader2 = new StreamReader(stream);

                FR_Batch_Files batch = new FR_Batch_Files();

                batch.Header = header;

                if (entities.FR_Batch_Files.Any(b => b.Header == batch.Header))
                {
                    return Request.CreateResponse(HttpStatusCode.Conflict);
                }
                else
                {          
                    batch.Footer = footer;
                    batch.Content = streamReader2.ReadToEnd();

                    entities.FR_Batch_Files.Add(batch);
                    entities.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }
}
