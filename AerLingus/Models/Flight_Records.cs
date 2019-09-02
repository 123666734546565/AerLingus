

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
        [MaxLength(16)]
        [DisplayName("Identifier number")]
        public string identifierNo { get; set; }

        [Required]
        [MaxLength(2)]
        [DisplayName("Transaction type")]
        public string transactionType { get; set; }

        [MaxLength(30)]
        [DisplayName("Other FFP number")]
        public string otherFFPNo { get; set; }

        [MaxLength(30)]
        [DisplayName("Other FFP scheme")]
        public string otherFFPScheme { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(30)]
        [DisplayName("First name")]
        public string firstName { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(30)]
        [DisplayName("Last name")]
        public string lastName { get; set; }

        [MaxLength(100)]
        [DisplayName("Partner transaction number")]
        public string partnerTransactionNo { get; set; }

        [DisplayName("Booking date")]
        public Nullable<System.DateTime> bookingDate { get; set; }

        [Required]
        [DisplayName("Departure date")]
        public System.DateTime departureDate { get; set; }

        [Required]
        [DisplayName("Origin")]
        [StringLength(maximumLength: 3, MinimumLength = 3)]
        public string origin { get; set; }

        [Required]
        [DisplayName("Destination")]
        [StringLength(maximumLength: 3, MinimumLength = 3)]
        public string destination { get; set; }

        [Required]
        [MaxLength(2)]
        [DisplayName("Booking class")]
        public string bookingClass { get; set; }

        [MaxLength(1)]
        [DisplayName("Cabin class")]
        public string cabinClass { get; set; }

        [Required]
        [MaxLength(4)]
        [DisplayName("Marketing flight number")]
        public string marketingFlightNo { get; set; }

        [Required]
        [MaxLength(2)]
        [DisplayName("Marketing airline")]
        public string marketingAirline { get; set; }

        [Required]
        [MaxLength(4)]
        [DisplayName("Operating flight number")]
        public string operatingFlightNo { get; set; }

        [Required]
        [MaxLength(2)]
        [DisplayName("Operating airline")]
        public string operatingAirline { get; set; }

        [MinLength(13)]
        [MaxLength(14)]
        [TicketExternalValidation]
        [DisplayName("Ticket number")]
        public string ticketNo { get; set; }

        [MaxLength(25)]
        [TicketExternalValidation]
        [DisplayName("External PaxID")]
        public string externalPaxID { get; set; }

        [MaxLength(2)]
        [DisplayName("Coupon number")]
        public string couponNo { get; set; }

        [Required]
        [MaxLength(6)]
        [DisplayName("pnrNo number")]
        public string pnrNo { get; set; }

        [Range(0, 99999)]
        [DisplayName("Distance")]
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

        [MaxLength(100)]
        [DisplayName("Promotion code")]
        public string promotionCode { get; set; }

        [MaxLength(3)]
        [DisplayName("Ticket currency")]
        [EIValidation]
        public string ticketCurrency { get; set; }

        [EIValidation]
        [MaxLength(3)]
        [DisplayName("Target currency")]
        public string targetCurrency { get; set; }

        [EIValidation]
        [Range(0, 9999999999)]
        [DisplayName("Exchange rate")]
        public Nullable<double> exchangeRate { get; set; }

        [MaxLength(10)]
        [DisplayName("Fare basis")]
        public string fareBasis { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
    }
}
