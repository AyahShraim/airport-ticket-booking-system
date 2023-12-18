using AirportTicketBookingSystemApp.FlightManagement;
using AirportTicketBookingSystemApp.Interfaces;

namespace AirportTicketBookingTry.Services.SearchService.SearchFilter
{
    public class BookingEmailFilter : ISearchCriteria<FlightBookingModel>
    {
        private string _email;
        public BookingEmailFilter(string email)
        {
            _email = email;
        }
        public bool ApplyFilter(FlightBookingModel booking)
        {
            return booking.Email.Equals(_email);
        }
    }
}
