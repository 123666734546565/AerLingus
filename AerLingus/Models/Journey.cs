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
    using System.ComponentModel.DataAnnotations;
    using AerLingus.Validations;
    
    public partial class Journey
    {
        [SearchFlightRecordValidation]
        [Display(Name = "Identifier Number")]
        [MaxLength(16)]
        public string IdentifierNo { get; set; }

        [Display(Name = "First Name")]
        [MaxLength(30)]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [MaxLength(30)]
        public string LastName { get; set; }


        public int ID { get; set; }

        [Display(Name = "Ticket Number")]
        [MaxLength(14)]
        public string TicketNo { get; set; }
    }
}
