using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.Dtos
{
    public class CustomerDto
    {
        
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public String Name { get; set; }

        public bool IsSubscribedToNewsLetter { get; set; }

        public byte MembershipTypeId { get; set; } // named as Convention -> entty frame work recognise this convention as foreignkey

        public MembershipTypeDto MemberShipType { get; set; }

       // [MIn18YearsIfAMember]
        public DateTime? BirthDate { get; set; }
    }
}