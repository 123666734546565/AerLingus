using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AerLingus.Helpers
{
    public class ApiViewBag
    {
        public static bool RequestIsComingFromController { get; set; } = false;
        public static HttpPostedFileBase RequestedFile { get; set; }
    }
}