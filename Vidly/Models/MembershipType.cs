using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class MembershipType
    {
        public byte Id { get; set; } // becuase we have only a few membership type
        
        [Required]
        public string Name { get; set; }

        public short SignUpFee { get; set; } // short use bcz fee is not more than  32000 
        public byte DurationInMonths { get; set; } // byte use  because month is 1-12 which is  between 0 - 255
        public byte DiscountRate { get; set; } // bcz disscount's measures 0-100 which is between 255


        public static readonly byte Unknown = 0;
        public static readonly byte PayAsYouGo = 1;

    }
}