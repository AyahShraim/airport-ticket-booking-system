using AirportTicketBookingSystemApp.FlightManagement;
using AirportTicketBookingSystemApp.Interfaces;

namespace AirportTicketBookingTry.Services.SearchService.SearchFilter
{
    public class BookingFlightnoFilter : ISearchCriteria<FlightBookingModel>
    {
        private int _flightNumber;
        public BookingFlightnoFilter(int flightNumber)
        {
            _flightNumber = flightNumber;
        }
        public bool ApplyFilter(FlightBookingModel booking)
        {
            return booking.FlightNumber== _flightNumber;
        }
    }
}
