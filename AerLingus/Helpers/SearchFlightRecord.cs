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


        //MOZDA NE RADE ANOTACIJE!!!


		[Display(Name = "Identifier Number")]
        [Required (ErrorMessage = "Maximum length must be 16 characters"), MaxLength(16)]
        public string S_identifierNo { get; set; }

		[Display(Name = "Other FFP Number")]
        [Required(ErrorMessage = "Maximum length must be 30 characters"), MaxLength(30)]
        public string S_otherFFPNo { get; set; }

		[Display(Name = "First Name")]
        [MinLength(2)]
        [MaxLength(30)]
        public string S_firstName { get; set; }

		[Display(Name = "Last Name")]
        [MinLength(2)]
        [MaxLength(30)]
        [Required(ErrorMessage ="Minimum length must be 2. Maximum length must be 30")]
        public string S_lastName { get; set; }

		[Display(Name = "Departure Date")]
        public Nullable<DateTime> S_departureDate { get; set; }

		[Display(Name = "Origin")]
        [Required(ErrorMessage = "Origin must be exectly 3 characters")]
        [StringLength(maximumLength: 3, MinimumLength = 3)]
        public string S_Origin { get; set; }

		[Display(Name = "Destination")]
        
        [StringLength(maximumLength: 3, MinimumLength = 3)]
        [Required(ErrorMessage = "Destination must be exectly 3 characters")]
        public string S_destination { get; set; }

		[Display(Name = "Booking Class")]
        [MaxLength(2)]
        [Required(ErrorMessage ="Maximum length is 2")]
        public string S_bookingClass { get; set; }

		[Display(Name = "Operating Airline")]
        [MaxLength(2)]
        [Required(ErrorMessage = "Maximum length is 2")]
        public string S_operatingAirline { get; set; }

		[Display(Name = "Ticket Number")]
        [MinLength(13)]
        [MaxLength(14)]
        [Required(ErrorMessage = "Minimum length must be 13. Maximum length must be 14")]
        public string S_ticketNo { get; set; }

		[Display(Name = "External Pax ID")]
        [MaxLength(25)]
        [Required(ErrorMessage = "Maximum length is 25")]
        [TicketExternalValidation]
        public string S_externalPaxID { get; set; }

		[Display(Name = "PNR Number")]
        [MaxLength(6)]
        [Required(ErrorMessage = "Maximum length is 6")]
        public string S_pnrNo { get; set; }

	}
}