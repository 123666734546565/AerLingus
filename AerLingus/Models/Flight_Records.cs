//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AerLingus.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Flight_Records
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Flight_Records()
        {
            this.JourneySegments = new HashSet<JourneySegment>();
        }

        [MaxLength(16)]
        [Display(Name = "Identifier Number")]
        public string identifierNo { get; set; }

        [Required]
        [MaxLength(2)]
        [Display(Name = "Transaction Type")]
        public string transactionType { get; set; }

        [MaxLength(30)]
        [Display(Name = "Other FFP Number")]
        public string otherFFPNo { get; set; }

        [MaxLength(30)]
        [Display(Name = "Other FFP Scheme")]
        public string otherFFPScheme { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "First Name")]
        public string firstName { get; set; }

        [Required]
        [MaxLength(30)]
        [Display(Name = "Last Name")]
        public string lastName { get; set; }

        [MaxLength(100)]
        [Display(Name = "Partner Transaction Number")]
        public string partnerTransactionNo { get; set; }

        [Display(Name = "Booking Date")]
        public Nullable<System.DateTime> bookingDate { get; set; }

        [Required]
        [Display(Name = "Departure Date")]
        public System.DateTime departureDate { get; set; }

        [Required]
        [MaxLength(5)]
        [Display(Name = "Origin")]
        public string origin { get; set; }

        [Required]
        [MaxLength(5)]
        [Display(Name = "Destination")]
        public string destination { get; set; }

        [Required]
        [MaxLength(2)]
        [Display(Name = "Booking Class")]
        public string bookingClass { get; set; }

        [MaxLength(2)]
        [Display(Name = "Cabin Class")]
        public string cabinClass { get; set; }

        [Required]
        [MaxLength(5)]
        [Display(Name = "Marketing Flight Number")]
        public string marketingFlightNo { get; set; }

        [Required]
        [MaxLength(3)]
        [Display(Name = "Marketing Airline")]
        public string marketingAirline { get; set; }

        [Required]
        [MaxLength(5)]
        [Display(Name = "Operating Flight Number")]
        public string operatingFlightNo { get; set; }

        [Required]
        [MaxLength(3)]
        [Display(Name = "Operating Airline")]
        public string operatingAirline { get; set; }

        [MinLength(13)]
        [MaxLength(14)]
        [Display(Name = "Ticket Number")]
        [Required]
        public string ticketNo { get; set; }

        [MaxLength(25)]
        [Display(Name = "External Pax ID")]
        [Required]
        public string externalPaxID { get; set; }

        [MaxLength(2)]
        [Display(Name = "Coupon Number")]
        public string couponNo { get; set; }

        [Required]
        [MaxLength(6)]
        [Display(Name = "PNR Number")]
        public string pnrNo { get; set; }

        [Range(minimum: 0, maximum: 99999)]
        [Display(Name = "Distance")]
        public Nullable<long> distance { get; set; }

        [Range(minimum: 0, maximum: 999999999)]
        [Display(Name = "Base Fare")]
        public Nullable<double> baseFare { get; set; }

        [Range(minimum: 0, maximum: 999999999)]
        [Display(Name = "Discount Base")]
        public Nullable<double> discountBase { get; set; }

        [Range(minimum: 0, maximum: 999999999)]
        [Display(Name = "Excise Tax")]
        public Nullable<double> exciseTax { get; set; }

        [MaxLength(1)]
        [Display(Name = "Customer Type")]
        public string customerType { get; set; }

        [MaxLength(100)]
        [Display(Name = "Promotion Code")]
        public string promotionCode { get; set; }

        [MaxLength(3)]
        [Display(Name = "Ticket Currency")]
        public string ticketCurrency { get; set; }

        [MaxLength(3)]
        [Display(Name = "Target Currency")]
        public string targetCurrency { get; set; }

        [Range(minimum: 0, maximum: 9999999999)]
        [Display(Name = "Exchange Rate")]
        public Nullable<double> exchangeRate { get; set; }

        [MaxLength(10)]
        [Display(Name = "Fare Basis")]
        public string fareBasis { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string Status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<JourneySegment> JourneySegments { get; set; }
    }
}
