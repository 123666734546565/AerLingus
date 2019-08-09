using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.VisualBasic;
using AerLingus.Models;

namespace AerLingus.Controllers
{
    public class FlightRecordsController : Controller
    {
        // GET: FlightRecords
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FlightRecordForm()
        {
            return View();
        }
        /*  public ActionResult Upload(HttpPostedFileBase file)
          {
              if (file == null)
                  return Content("No file selected.");

              if (file.ContentLength == 0)
                  return Content("File is empty.");

              string failedToAddRecords = string.Empty;

              bool addedAtLeastOneRecord = false;

              try
              {
                  string tempRecord = string.Empty;

                  string header = string.Empty;
                  string[] headerArray = null;

                  //string body = string.Empty;
                  string[] bodyArray = null;

                  string footer = string.Empty;
                  string[] footerArray = null;

                  Flight_Records record = new Flight_Records();

                  string content = string.Empty;

                  System.IO.StreamReader streamReader = new System.IO.StreamReader(file.InputStream);

                  header = streamReader.ReadLine();

                  headerArray = header.Split('|');

                  if (headerArray[0].ToUpper() == "H")
                  {
                      while (!streamReader.EndOfStream)
                      {
                          tempRecord = streamReader.ReadLine();

                          switch (tempRecord[0])
                          {
                              case 'R':
                                  {
                                      bodyArray = tempRecord.Split('|');

                                      if (bodyArray[1] != string.Empty && bodyArray[1].Length <= 16)
                                      {
                                          record.identifierNo = bodyArray[1];
                                      }
                                      else
                                      {
                                          record.identifierNo = null;
                                      }

                                      if (bodyArray[2] != string.Empty && bodyArray[2].Length <= 2)
                                      {
                                          record.transactionType = bodyArray[2];
                                      }
                                      else
                                      {
                                          record.transactionType = null;
                                      }

                                      if (bodyArray[3] != string.Empty && bodyArray[3].Length <= 30)
                                      {
                                          record.otherFFPNo = bodyArray[3];
                                      }
                                      else
                                      {
                                          record.otherFFPNo = null;
                                      }

                                      if (bodyArray[4] != string.Empty)
                                      {
                                          record.otherFFPScheme = bodyArray[4];
                                      }
                                      else
                                      {
                                          record.otherFFPScheme = null;
                                      }

                                      if (bodyArray[5] != string.Empty)
                                      {
                                          record.firstName = bodyArray[5];
                                      }
                                      else
                                      {
                                          record.firstName = null;
                                      }

                                      if (bodyArray[6] != string.Empty)
                                      {
                                          record.lastName = bodyArray[6];
                                      }
                                      else
                                      {
                                          record.lastName = null;
                                      }

                                      if (bodyArray[7] != string.Empty)
                                      {
                                          record.partnerTransactionNo = bodyArray[7];
                                      }
                                      else
                                      {
                                          record.partnerTransactionNo = null;
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
                                      }

                                      if (bodyArray[10] != string.Empty)
                                      {
                                          record.origin = bodyArray[10];
                                      }
                                      else
                                      {
                                          record.origin = null;
                                      }

                                      if (bodyArray[11] != string.Empty)
                                      {
                                          record.destination = bodyArray[11];
                                      }
                                      else
                                      {
                                          record.destination = null;
                                      }

                                      if (bodyArray[12] != string.Empty)
                                      {
                                          record.bookingClass = bodyArray[12];
                                      }
                                      else
                                      {
                                          record.bookingClass = null;
                                      }

                                      if (bodyArray[13] != string.Empty)
                                      {
                                          record.cabinClass = bodyArray[13];
                                      }
                                      else
                                      {
                                          record.cabinClass = null;
                                      }

                                      if (bodyArray[14] != string.Empty &&
                                          Information.IsNumeric(bodyArray[14]))
                                      {
                                          record.marketingFlightNo = bodyArray[14];
                                      }
                                      else
                                      {
                                          record.marketingFlightNo = null;
                                      }

                                      if (bodyArray[15] != string.Empty)
                                      {
                                          record.marketingAirline = bodyArray[15];
                                      }
                                      else
                                      {
                                          record.marketingAirline = null;
                                      }

                                      if (bodyArray[16] != string.Empty &&
                                          Information.IsNumeric(bodyArray[16]))
                                      {
                                          record.operatingFlightNo = bodyArray[16];
                                      }
                                      else
                                      {
                                          record.operatingFlightNo = null;
                                      }

                                      if (bodyArray[17] != string.Empty)
                                      {
                                          record.operatingAirline = bodyArray[17];
                                      }
                                      else
                                      {
                                          record.operatingAirline = null;
                                      }

                                      if (bodyArray[18] != string.Empty &&
                                          (bodyArray[18].Length == 13 || bodyArray[18].Length == 14) &&
                                          Information.IsNumeric(bodyArray[18]))
                                      {
                                          record.ticketNo = bodyArray[18];
                                      }
                                      else
                                      {
                                          record.ticketNo = null;
                                      }

                                      if (bodyArray[19] != string.Empty)
                                      {
                                          record.externalPaxID = bodyArray[19];
                                      }
                                      else
                                      {
                                          record.externalPaxID = null;
                                      }

                                      if (bodyArray[20] != string.Empty)
                                      {
                                          record.couponNo = bodyArray[20];
                                      }
                                      else
                                      {
                                          record.couponNo = null;
                                      }

                                      if (bodyArray[21] != string.Empty &&
                                          (char.IsUpper(bodyArray[21][0]) && char.IsLetter(bodyArray[21][0])) &&
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
                                          record.pnrNo = null;
                                      }

                                      if (bodyArray[22] != string.Empty)
                                      {
                                          record.distance = Convert.ToInt64(bodyArray[22]);
                                      }
                                      else
                                      {
                                          record.distance = default(long);
                                      }

                                      if (bodyArray[23] != string.Empty)
                                      {
                                          record.baseFare = Convert.ToSingle(bodyArray[23]);
                                      }
                                      else
                                      {
                                          record.baseFare = default(float);
                                      }

                                      if (bodyArray[24] != string.Empty)
                                      {
                                          record.discountBase = Convert.ToSingle(bodyArray[24]);
                                      }
                                      else
                                      {
                                          record.discountBase = default(float);
                                      }

                                      if (bodyArray[25] != string.Empty)
                                      {
                                          record.exciseTax = Convert.ToSingle(bodyArray[25]);
                                      }
                                      else
                                      {
                                          record.exciseTax = default(float);
                                      }

                                      if (bodyArray[26] != string.Empty &&
                                          (char.ToUpper(bodyArray[26][0]) == 'A' || char.ToUpper(bodyArray[26][0]) == 'C' || char.ToUpper(bodyArray[26][0]) == 'I'))
                                      {
                                          record.customerType = bodyArray[26];
                                      }
                                      else
                                      {
                                          record.customerType = null;
                                      }

                                      if (bodyArray[27] != string.Empty)
                                      {
                                          record.promotionCode = bodyArray[27];
                                      }
                                      else
                                      {
                                          record.promotionCode = null;
                                      }

                                      if (bodyArray[28] != string.Empty)
                                      {
                                          record.ticketCurrency = bodyArray[28];
                                      }
                                      else
                                      {
                                          record.ticketCurrency = null;
                                      }

                                      if (bodyArray[29] != string.Empty)
                                      {
                                          record.targetCurrency = bodyArray[29];
                                      }
                                      else
                                      {
                                          record.targetCurrency = null;
                                      }

                                      if (bodyArray[30] != string.Empty)
                                      {
                                          record.exchangeRate = Convert.ToSingle(bodyArray[30]);
                                      }
                                      else
                                      {
                                          record.exchangeRate = null;
                                      }

                                      if (bodyArray[31] != string.Empty)
                                      {
                                          record.fareBasis = bodyArray[31];
                                      }
                                      else
                                      {
                                          record.fareBasis = null;
                                      }



                                      break;
                                  }
                              case 'F':
                                  {
                                      footerArray = tempRecord.Split('|');

                                      break;
                                  }
                              default: failedToAddRecords = failedToAddRecords + tempRecord + "\n"; break;
                          }
                      }
                  }
                  else return Content("Error: File has no header or it is not prefixed with H.");
              }
              catch (Exception ex)
              {
                  return Content("Something went wrong." + ex.Message);
              }
          }*/
        // }
    }
}