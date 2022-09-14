using System;
using System.ComponentModel.DataAnnotations;

namespace Vidly.App.Models
{
    public class RentalDetail
    {
        public int Id { get; set; }
        [Required]
        public RentalHeader RentalHeader { get; set; }
        public int RentalHeaderId { get; set; }

        [Required]
        public Movie Movie { get; set; }
        public int MovieId { get; set; }
        public DateTime DateRented { get; set; }
        public DateTime? DateReturned { get; set; }
        public bool IsReturn { get; set; }
    }
}