using System.Collections.Generic;
using Vidly.App.Models;

namespace Vidly.App.ViewModels
{
    public class RentalFormViewModel
    {
        public string RentalNo { get; set; }
        public Customer Customer { get; set; }
    }

    public class CustomerViewModel
    {
        public Customer Customer { get; set; }
    }
}