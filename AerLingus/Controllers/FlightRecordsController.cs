using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.VisualBasic;
using AerLingus.Models;
using System.Data.Entity;
using System.IO;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using AerLingus.Controllers.Api;
using System.Web.Http;

namespace AerLingus.Controllers
{
    public class FlightRecordsController : Controller
    {
        private HttpClient client;

        public FlightRecordsController()
        {
            client = new HttpClient();
        }

        public ActionResult Upload(HttpPostedFileBase file)
        {
            if (file == null)
                return Content("No file selected.");

            if (file.ContentLength == 0)
                return Content("File is empty.");

            var fileName = Path.GetFileName(file.FileName);

            var path = Path.Combine(Server.MapPath("~/UploadedFiles"), fileName);

            file.SaveAs(path);

            FlightRecordsApiController api = new FlightRecordsApiController()
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            var returnedStatusCode = api.Upload(file).StatusCode;

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

        public ActionResult FlightRecordForm()
        {
            return View();
        }

        [System.Web.Http.HttpPost]
        public ActionResult DragAndDrop()
        {
            int counter = 1;
            string fname = string.Empty;

        

            try
            { 
                foreach (string fileName in Request.Files)
                {

                    string errorMessage = "";
                    HttpPostedFileBase file = Request.Files[fileName];
                    fname = file.FileName;
                    if (file != null && file.ContentLength > 0)
                    {
                        var path = Path.Combine(Server.MapPath("~/UploadedFiles"));
                        string pathString = Path.Combine(path.ToString());

                        if(Path.GetExtension(file.FileName) != ".csv")
                        {
                            errorMessage += errorMessage + "File[" + counter + "] - ERROR 421: File must have .csv extension" + "\n";
                            counter++;
                            continue;
                        }
                       

                        string fileName1 = fname + Path.GetExtension(file.FileName);
                        string uploadPath = string.Format("{0}\\{1}", pathString, fileName1);
                        file.SaveAs(uploadPath);

                        FlightRecordsApiController api = new FlightRecordsApiController()
                        {
                            Request = new HttpRequestMessage(),
                            Configuration = new HttpConfiguration()
                        };

                        var returnedStatusCode = api.Upload(file).StatusCode;

                        if (returnedStatusCode == System.Net.HttpStatusCode.OK)
                           // ViewBag.ErrorMessages = ViewBag.ErrorMessages + "File[" + counter + "] - STATUS 201: Successful" + "\n";
                        errorMessage += errorMessage + "File[" + counter + "] - STATUS 201: Successfull" + "\n";
                        else
                        {
                            if (returnedStatusCode == System.Net.HttpStatusCode.NotFound)
                                //ViewBag.ErrorMessages = ViewBag.ErrorMessages + "File[" + counter + "] - ERROR 404: No file selected" + "\n";
                                errorMessage += errorMessage + "File[" + counter + "] - ERROR 404: No file selected" + "\n";
                            else if (returnedStatusCode == System.Net.HttpStatusCode.NoContent)
                                //ViewBag.ErrorMessages = ViewBag.ErrorMessages + "File[" + counter + "] - ERROR 204: File is empty" + "\n";
                            errorMessage += errorMessage + "File[" + counter + "] - ERROR 204: File is empty" + "\n";
                            else if (returnedStatusCode == System.Net.HttpStatusCode.NotAcceptable)
                                //ViewBag.ErrorMessages = ViewBag.ErrorMessages + "File[" + counter + "] - ERROR 406: File is missing header or footer or they are not prefixed with H or F" + "\n";
                            errorMessage += errorMessage + "File[" + counter + "] - ERROR 406: File is missing header or footer or they are not prefixed with H or F" + "\n";
                            else if (returnedStatusCode == System.Net.HttpStatusCode.BadRequest)
                                //ViewBag.ErrorMessages = ViewBag.ErrorMessages + "File[" + counter + "] - ERROR 400: Footer does not have the number of potential records" + "\n";
                            errorMessage += errorMessage + "File[" + counter + "] - ERROR 400: Footer does not have the number of potential records" + "\n";
                            else if (returnedStatusCode == System.Net.HttpStatusCode.PreconditionFailed)
                                //ViewBag.ErrorMessages = ViewBag.ErrorMessages + "File[" + counter + "] - ERROR 412: No record added to database because the number of footer records does not match with the number of existing records in the file" + "\n";
                            errorMessage += errorMessage + "File[" + counter + "] - ERROR 412: No record added to database because the number of footer records does not match with the number of existing records in the file" + "\n";
                            else if (returnedStatusCode == System.Net.HttpStatusCode.Conflict)
                                //ViewBag.ErrorMessages = ViewBag.ErrorMessages + "File[" + counter + "] - ERROR 409: File with that header already exists in database" + "\n";
                            errorMessage += errorMessage + "File[" + counter + "] - ERROR 409: File with that header already exists in database" + "\n";
                            //else ViewBag.ErrorMessages = ViewBag.ErrorMessages + "File[" + counter + "] - ERROR 500: Internal Server Error" + "\n";
                            else errorMessage += errorMessage + "File[" + counter + "] - ERROR 500: Internal Server Error" + "\n";
                            counter++;

                            return Content(errorMessage);
                        }
                    }
                  
                }
                return Content("valjda");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

    }
}