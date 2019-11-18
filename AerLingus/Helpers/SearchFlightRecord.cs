using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using AerLingus.Validations;


namespace AerLingus.Helpers
{
    public class SearchFlightRecord
    {
        [SearchFlightRecordValidation]
		[Display(Name = "Identifier Number")]
        //[MaxLength(16)]
        [Required(ErrorMessage = "No valid id number"), MaxLength(16)]
        public string S_identifierNo { get; set; }

		[Display(Name = "Other FFP Number")]
        //[MaxLength(30)]
        [Required(ErrorMessage = "Other FFP number"), MaxLength(30)]
        public string S_otherFFPNo { get; set; }

		[Display(Name = "First Name")]
        //[MaxLength(30)]
        [Required(ErrorMessage = "First Name invalid"), MaxLength(30)]
        public string S_firstName { get; set; }

		[Display(Name = "Last Name")]
        //[MaxLength(30)]
        [Required(ErrorMessage = "Last name invalid"), MaxLength(30)]
        public string S_lastName { get; set; }

		[Display(Name = "Departure Date")]
        public Nullable<DateTime> S_departureDate { get; set; }

		[Display(Name = "Origin")]
        //[MaxLength(3)]
        [Required(ErrorMessage = "Origin invalid"), MaxLength(30)]

        public string S_Origin { get; set; }

		[Display(Name = "Destination")]       
        //[MaxLength(3)]
        [Required(ErrorMessage = "Destination invalid"), MaxLength(3)]
        public string S_destination { get; set; }

		[Display(Name = "Booking Class")]
        //[MaxLength(2)]
        [Required(ErrorMessage = "Booking class invalid"), MaxLength(2)]
        public string S_bookingClass { get; set; }

		[Display(Name = "Operating Airline")]
        //[MaxLength(2)]
        [Required(ErrorMessage = "Operating airline invalid"), MaxLength(2)]
        public string S_operatingAirline { get; set; }

		[Display(Name = "Ticket Number")]
        [Required(ErrorMessage = "Ticket number invalid"), MaxLength(14)]
        //[MaxLength(14)]
        public string S_ticketNo { get; set; }

		[Display(Name = "External Pax ID")]
        //[MaxLength(25)]
        [Required(ErrorMessage = "External Pax ID invalid"), MaxLength(25)]
        public string S_externalPaxID { get; set; }

		[Display(Name = "PNR Number")]
        [Required(ErrorMessage = "PNR number invalid"), MaxLength(6)]
        //[MaxLength(6)]
        public string S_pnrNo { get; set; }
	}
}