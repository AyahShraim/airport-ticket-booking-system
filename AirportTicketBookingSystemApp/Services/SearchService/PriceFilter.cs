using AirportTicketBookingSystemApp.Enums;
using AirportTicketBookingSystemApp.FlightManagement;
using AirportTicketBookingSystemApp.Interfaces;

namespace AirportTicketBookingSystemApp.Services.SearchService
{
    public class PriceFilter : ISearchCriteria<Flight>
    {
        private FlightClassType _classType;
        private FlightServices _flightServices;
        private double _maxPrice;
        public PriceFilter(double maxprice, FlightClassType classType)
        {
            _classType = classType;
            _maxPrice = maxprice;
            _flightServices = new();
        }

        public bool ApplyFilter(Flight flight)
        {
            return _flightServices.FlightPrice(flight, _classType) <= _maxPrice;
        }
    }
}
