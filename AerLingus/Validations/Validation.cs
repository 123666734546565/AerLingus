using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AerLingus.Models;

namespace AerLingus.Validations
{
    public abstract class Validation
    {
        private static AerLingus_databaseEntities entities = new AerLingus_databaseEntities();

        public static Flight_Records TickerNoValidation(Flight_Records record)
        {
            return (from recordInDatabase in entities.Flight_Records
                    where recordInDatabase.ticketNo == record.ticketNo &&
                          recordInDatabase.departureDate == record.departureDate &&
                          recordInDatabase.origin == record.origin
                    select recordInDatabase).SingleOrDefault();
        }

        public static Flight_Records ExternalPaxIDValidation(Flight_Records record)
        {
            return (from recordInDatabase in entities.Flight_Records
                    where recordInDatabase.externalPaxID == record.externalPaxID &&
                          recordInDatabase.departureDate == record.departureDate &&
                          recordInDatabase.origin == record.origin
                    select recordInDatabase).SingleOrDefault();
        }

        public static void SetEmptyPropertiesToNull(Flight_Records record)
        {
            record.identifierNo = record.identifierNo == string.Empty ? null : record.identifierNo;
            record.otherFFPNo = record.otherFFPNo == string.Empty ? null : record.otherFFPNo;
            record.otherFFPScheme = record.otherFFPScheme == string.Empty ? null : record.otherFFPScheme;
            record.partnerTransactionNo = record.partnerTransactionNo == string.Empty ? null : record.partnerTransactionNo;
            record.bookingDate = record.bookingDate == default(DateTime) ? null : record.bookingDate;
            record.cabinClass = record.cabinClass == string.Empty ? null : record.cabinClass;
            record.ticketNo = record.ticketNo == string.Empty ? null : record.ticketNo;
            record.externalPaxID = record.externalPaxID == string.Empty ? null : record.externalPaxID;
            record.couponNo = record.couponNo == string.Empty ? null : record.couponNo;
            record.distance = record.distance == default(long) ? null : record.distance;
            record.baseFare = record.baseFare == default(double) ? null : record.baseFare;
            record.discountBase = record.discountBase == default(double) ? null : record.discountBase;
            record.exciseTax = record.exciseTax == default(double) ? null : record.exciseTax;
            record.customerType = record.customerType == string.Empty ? null : record.customerType;
            record.promotionCode = record.promotionCode == string.Empty ? null : record.promotionCode;
            record.ticketCurrency = record.ticketCurrency == string.Empty ? null : record.ticketCurrency;
            record.targetCurrency = record.targetCurrency == string.Empty ? null : record.targetCurrency;
            record.exchangeRate = record.exchangeRate == default(double) ? null : record.exchangeRate;
            record.fareBasis = record.fareBasis == string.Empty ? null : record.fareBasis;
        }
    }
}