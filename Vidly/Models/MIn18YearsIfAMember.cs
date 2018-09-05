using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Vidly.Models
{
    public class MIn18YearsIfAMember: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value,ValidationContext validationContext)
        {
            var customer = (Customer)validationContext.ObjectInstance;

            if(customer.MembershipTypeId == MembershipType.Unknown || customer.MembershipTypeId == MembershipType.PayAsYouGo)
            {
                return ValidationResult.Success;
            }
            if(customer.BirthDate == null)
            {
                return new ValidationResult("BirthDate is required");
            }
            var age = DateTime.Today.Year - customer.BirthDate.Value.Year;

            return (age >= 18) 
                ? ValidationResult.Success 
                : new ValidationResult("customer Should be at least 18 years old to go on a membership");

        }   
    }
}