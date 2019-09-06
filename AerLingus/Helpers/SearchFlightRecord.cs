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
        [SearchValidation]
		[Display(Name = "Identifier Number")]
        [MaxLength(16)]
        public string S_identifierNo { get; set; }

		[Display(Name = "Other FFP Number")]
        [MaxLength(30)]
        public string S_otherFFPNo { get; set; }

		[Display(Name = "First Name")]
        [MaxLength(30)]
        public string S_firstName { get; set; }

		[Display(Name = "Last Name")]
        [MaxLength(30)]
        public string S_lastName { get; set; }

		[Display(Name = "Departure Date")]
        public Nullable<DateTime> S_departureDate { get; set; }

		[Display(Name = "Origin")]
        [MaxLength(3)]
        public string S_Origin { get; set; }

		[Display(Name = "Destination")]       
        [MaxLength(3)]
        public string S_destination { get; set; }

		[Display(Name = "Booking Class")]
        [MaxLength(2)]
        public string S_bookingClass { get; set; }

		[Display(Name = "Operating Airline")]
        [MaxLength(2)]
        public string S_operatingAirline { get; set; }

		[Display(Name = "Ticket Number")]
        [MaxLength(14)]
        public string S_ticketNo { get; set; }

		[Display(Name = "External Pax ID")]
        [MaxLength(25)]
        public string S_externalPaxID { get; set; }

		[Display(Name = "PNR Number")]
        [MaxLength(6)]
        public string S_pnrNo { get; set; }
	}
}