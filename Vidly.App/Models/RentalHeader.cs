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

        [Required]
        public Movie Movie { get; set; }
        public int MovieId { get; set; }

        public DateTime DateRented { get; set; }
        public bool IsReturn { get; set; }
        public DateTime? DateReturned { get; set; }
    }
}