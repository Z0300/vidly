using System.Collections.Generic;

namespace Vidly.App.Dtos
{
    public class NewRentalDto
    {
        public int CustomerId { get; set; }
        public List<int> MovieIds { get; set; }
    }

    public class ReturnMoviesDto
    {
        public int rentalId { get; set; }
        public List<int> MovieIds { get; set; }
    }
    public class ReturnMovieDto
    {
        public int rentalId { get; set; }
        public int MovieId { get; set; }
    }
}