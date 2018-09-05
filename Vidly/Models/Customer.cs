using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public String Name { get; set; }

        public bool IsSubscribedToNewsLetter { get; set; }

        public MembershipType MemberShipType { get; set; }

        [Display(Name="Membership Type")]
        public byte MembershipTypeId { get; set; } // named as Convention -> entty frame work recognise this convention as foreignkey

        [Display(Name="Date Of Birth")]
        [MIn18YearsIfAMember]
        public DateTime? BirthDate { get; set; }
 

    }
}