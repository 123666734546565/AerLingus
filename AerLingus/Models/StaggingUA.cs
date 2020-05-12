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
    
    public partial class StaggingUA
    {
        public string RecordTypeHeader { get; set; }
        public string FileTypeHeader { get; set; }
        public string DeliverySequenceNoHeader { get; set; }
        public string SendingSystemHeader { get; set; }
        public string SendingAirlineHeader { get; set; }
        public string ReceivingAirlineHeader { get; set; }
        public string CreateDateHeader { get; set; }
        public string VersionHeader { get; set; }
        public string CarrierFileReferenceHeader { get; set; }
        public string FillerHeader { get; set; }
        public string RecordType { get; set; }
        public string FFPProgram { get; set; }
        public string FFPMemberNumber { get; set; }
        public string FFPMemberLastName { get; set; }
        public string FFPMemberFirstName { get; set; }
        public string NameCheckOverride { get; set; }
        public string OperatingAirlineCode { get; set; }
        public string OperatingFlightNumber { get; set; }
        public string MarketingAirlineCode { get; set; }
        public string MarketingFlightNumber { get; set; }
        public string CodeShareIndicator { get; set; }
        public string FlightDepartureDate { get; set; }
        public string FlightArrivalDate { get; set; }
        public string OriginAirportCode { get; set; }
        public string DestinationAirportCode { get; set; }
        public string OperatingBookingClassCode { get; set; }
        public string FlownCabinClassCode { get; set; }
        public string OperatingRevenueBookingClassCode { get; set; }
        public string MarketingBookingClass { get; set; }
        public string UpgradeIndicator { get; set; }
        public string DowngradeIndicator { get; set; }
        public string EntitlementNumber { get; set; }
        public string TicketNumber { get; set; }
        public string CouponNumber { get; set; }
        public string FareBasisCode { get; set; }
        public string SeatNumber { get; set; }
        public string PNRNumber { get; set; }
        public string UpdateCode { get; set; }
        public string BaseFlightMiles { get; set; }
        public string CheckInSourceCode { get; set; }
        public string BookingSourceCode { get; set; }
        public string OperatingAirlineAuthorizationNo { get; set; }
        public string InternalAirlineReferenceNoFFPAirline { get; set; }
        public string AccrualPostingStatus { get; set; }
        public string ResponseCode1 { get; set; }
        public string ResponseCode2 { get; set; }
        public string ResponseCode3 { get; set; }
        public string ResponseCode4 { get; set; }
        public string ResponseCode5 { get; set; }
        public string ResponseCode6 { get; set; }
        public string TierLever { get; set; }
        public string Gender { get; set; }
        public string Filler { get; set; }
        public string AirlineAdditionalInformation { get; set; }
        public string RecordTypeFooter { get; set; }
        public string TotalNumbersOfRecords { get; set; }
        public string NumberOfAcceptedRecordsWithoutChanges { get; set; }
        public string NumberOfAcceptedRecordsWithChanges { get; set; }
        public string NumberOfRejectedRecords { get; set; }
        public string FillerFooter { get; set; }
        public int ID { get; set; }
        public Nullable<int> DownloadCounter { get; set; }
        public string Description { get; set; }
        public Nullable<int> JourneySegmentID { get; set; }
    }
}
