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
    using System.ComponentModel.DataAnnotations.Schema;
    
    public partial class Flight_Records
    {
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

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public Flight_Records()
        {

        }

        public Flight_Records(Flight_Records record)
        {
            identifierNo = record.identifierNo;
            transactionType = record.transactionType;
            otherFFPNo = record.otherFFPNo;
            otherFFPScheme = record.otherFFPScheme;
            firstName = record.firstName;
            lastName = record.lastName;
            partnerTransactionNo = record.partnerTransactionNo;
            bookingDate = record.bookingDate;
            departureDate = record.departureDate;
            origin = record.origin;
            destination = record.destination;
            bookingClass = record.bookingClass;
            cabinClass = record.cabinClass;
            marketingFlightNo = record.marketingFlightNo;
            marketingAirline = record.marketingAirline;
            operatingFlightNo = record.operatingFlightNo;
            operatingFlightNo = record.operatingAirline;
            ticketNo = record.ticketNo;
            externalPaxID = record.externalPaxID;
            couponNo = record.couponNo;
            pnrNo = record.pnrNo;
            distance = record.distance;
            baseFare = record.baseFare;
            discountBase = record.discountBase;
            exciseTax = record.exciseTax;
            customerType = record.customerType;
            promotionCode = record.promotionCode;
            ticketCurrency = record.ticketCurrency;
            targetCurrency = record.targetCurrency;
            exchangeRate = record.exchangeRate;
            fareBasis = record.fareBasis;
        }
    }
}
