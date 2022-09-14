﻿using System.ComponentModel.DataAnnotations;

namespace Vidly.App.Models
{
    public class RentalDetail
    {
        public int Id { get; set; }
        public string RentalNo { get; set; }
        public int CustomerId { get; set; }
        public int MovieId { get; set; }
        public int Qty { get; set; }
    }
}