using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class CustomerFormViewModel
    {
        // we need list of membership type
        public IEnumerable<MembershipType> MembershipTypes { get; set; }
        /// we just want to iterate the list item so put IEnumerable

        public Customer Customer { get; set; }

    }

}