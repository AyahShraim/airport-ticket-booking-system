using AirportTicketBookingSystemApp.FlightManagement;
using AirportTicketBookingSystemApp.Interfaces;

namespace AirportTicketBookingSystemApp.Services.SearchService.SearchFilter
{
    public class BookingClassPrice : ISearchCriteria<FlightBookingModel>
    {
        private double _price;
        public BookingClassPrice(double price)
        {
            _price = price;
        }
        public bool ApplyFilter(FlightBookingModel booking)
        {
            return booking.Price == _price;
        }
    }
}
