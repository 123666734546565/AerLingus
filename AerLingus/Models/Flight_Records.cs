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
    
    public partial class Flight_Records
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Flight_Records()
        {
            this.JourneySegments = new HashSet<JourneySegment>();
        }
    
        public string identifierNo { get; set; }
        public string transactionType { get; set; }
        public string otherFFPNo { get; set; }
        public string otherFFPScheme { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string partnerTransactionNo { get; set; }
        public Nullable<System.DateTime> bookingDate { get; set; }
        public System.DateTime departureDate { get; set; }
        public string origin { get; set; }
        public string destination { get; set; }
        public string bookingClass { get; set; }
        public string cabinClass { get; set; }
        public string marketingFlightNo { get; set; }
        public string marketingAirline { get; set; }
        public string operatingFlightNo { get; set; }
        public string operatingAirline { get; set; }
        public string ticketNo { get; set; }
        public string externalPaxID { get; set; }
        public string couponNo { get; set; }
        public string pnrNo { get; set; }
        public Nullable<long> distance { get; set; }
        public Nullable<double> baseFare { get; set; }
        public Nullable<double> discountBase { get; set; }
        public Nullable<double> exciseTax { get; set; }
        public string customerType { get; set; }
        public string promotionCode { get; set; }
        public string ticketCurrency { get; set; }
        public string targetCurrency { get; set; }
        public Nullable<double> exchangeRate { get; set; }
        public string fareBasis { get; set; }
        public int ID { get; set; }
        public string Status { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<JourneySegment> JourneySegments { get; set; }
    }
}
