

namespace AerLingus.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel;
    using AerLingus.Validations;

    public partial class Flight_Records
    {
        [DisplayName("Identifier number")]
        [MaxLength(16)]
        public string identifierNo { get; set; }

        [DisplayName("Transaction type")]
        [Required]
        [MaxLength(2)]
        public string transactionType { get; set; }

        [DisplayName("Other FFP number")]
        [MaxLength(30)]
        public string otherFFPNo { get; set; }

        [MaxLength(30)]
        [DisplayName("Other FFP scheme")]
        public string otherFFPScheme { get; set; }

        [MinLength(2)]
        [MaxLength(30)]
        [DisplayName("First name")]
        [Required]
        public string firstName { get; set; }

        [MinLength(2)]
        [MaxLength(30)]
        [DisplayName("Last name")]
        [Required]
        public string lastName { get; set; }

        [MaxLength(100)]
        [DisplayName("Partner transaction number")]
        public string partnerTransactionNo { get; set; }

        [DisplayName("Booking date")]
        public Nullable<System.DateTime> bookingDate { get; set; }

        [DisplayName("Departure date")]
        [Required]
        public System.DateTime departureDate { get; set; }

        [DisplayName("Origin")]
        [Required]
        [StringLength(maximumLength: 3, MinimumLength = 3)]
        public string origin { get; set; }

        [DisplayName("Destination")]
        [Required]
        [StringLength(maximumLength: 3, MinimumLength = 3)]
        public string destination { get; set; }

        [DisplayName("Booking class")]
        [Required]
        [MaxLength(2)]
        public string bookingClass { get; set; }

        [DisplayName("Cabin class")]
        [MaxLength(1)]
        public string cabinClass { get; set; }

        [DisplayName("Marketing flight number")]
        [Required]
        [MaxLength(4)]
        public string marketingFlightNo { get; set; }

        [MaxLength(2)]
        [DisplayName("Marketing airline")]
        [Required]
        public string marketingAirline { get; set; }

        [MaxLength(4)]
        [DisplayName("Operating flight number")]
        [Required]
        public string operatingFlightNo { get; set; }

        [MaxLength(2)]
        [DisplayName("Operating airline")]
        [Required]
        public string operatingAirline { get; set; }

        [MinLength(13)]
        [MaxLength(14)]
        //[TicketExternalValidation]
        [DisplayName("Ticket number")]
        public string ticketNo { get; set; }

        [DisplayName("External PaxID")]
        [MaxLength(25)]
        //[TicketExternalValidation]
        public string externalPaxID { get; set; }

        [MaxLength(2)]
        [DisplayName("Coupon number")]
        public string couponNo { get; set; }

        [MaxLength(6)]
        [DisplayName("pnrNo number")]
        [Required]
        public string pnrNo { get; set; }

        [DisplayName("Distance")]
        [Range(0, 99999)]
        public Nullable<long> distance { get; set; }

        [Range(0, 999999999)]
        [DisplayName("Base fare")]
        public Nullable<double> baseFare { get; set; }

        [Range(0, 999999999)]
        [DisplayName("Discount base")]
        public Nullable<double> discountBase { get; set; }

        [Range(0, 999999999)]
        [DisplayName("Excise tax")]
        public Nullable<double> exciseTax { get; set; }

        [MaxLength(1)]
        [DisplayName("Customer type")]
        public string customerType { get; set; }

        [MaxLength(1)]
        [DisplayName("Promotion code")]
        public string promotionCode { get; set; }

        [MaxLength(3)]
        [DisplayName("Ticket currency")]
       // [EIValidation]
        public string ticketCurrency { get; set; }

        [MaxLength(3)]
        [DisplayName("Target currency")]
      //  [EIValidation]
        public string targetCurrency { get; set; }

        [Range(0, 9999999999)]
        [DisplayName("Exchange rate")]
        //[EIValidation]
        public Nullable<double> exchangeRate { get; set; }

        [MaxLength(10)]
        [DisplayName("Fare basis")]
        public string fareBasis { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
    }
}
