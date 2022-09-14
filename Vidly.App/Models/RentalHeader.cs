using System;
using System.ComponentModel.DataAnnotations;

namespace Vidly.App.Models
{
    public class RentalHeader
    {
        public int Id { get; set; }
        public string RentalNo { get; set; }
        [Required]
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
    }
}