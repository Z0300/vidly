using System;
using System.ComponentModel.DataAnnotations;

namespace Vidly.App.Models
{
    public class Rental
    {
        public int Id { get; set; }
        public string RentalNo { get; set; }

        [Required]
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }

        [Required]
        public Movie Movie { get; set; }
        public int MovieId { get; set; }

        public DateTime DateRented { get; set; }
        public DateTime? DateReturned { get; set; }
    }
}