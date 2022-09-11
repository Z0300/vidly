using System.Collections.Generic;

namespace Vidly.App.Dtos
{
    public class NewRentalDto
    {
        public int CustomerId { get; set; }
        public List<int> MovieIds { get; set; }
    }

    public class ReturnMovieDto
    {
        public string RentalNo { get; set; }
        public List<int> MovieIds { get; set; }
    }
}