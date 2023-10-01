using AirportTicketBookingSystemApp.Enums;
using AirportTicketBookingSystemApp.FlightManagement;
using AirportTicketBookingSystemApp.Interfaces;

namespace AirportTicketBookingSystemApp.Services.SearchService
{
    public class PriceFilter : ISearchCriteria<Flight>
    {
 
        private FlightClassType _classType;
        private double _maxPrice;
        public PriceFilter(double maxprice, FlightClassType classType)
        {
            _classType = classType;
            _maxPrice = maxprice;
        }

        public bool ApplyFilter(Flight flight)
        {
            bool isValid = false;
            switch (_classType)
            {
                case FlightClassType.Business:
                    isValid = flight.BusinessPrice <= _maxPrice;
                    break;

                case FlightClassType.Economy:
                    isValid = flight.EconomiyPrice <= _maxPrice;
                    break;

                case FlightClassType.FirstClass:
                    isValid = flight.FirstClassPrice<= _maxPrice;
                    break;
            }
            return isValid;
        }
    }
}

