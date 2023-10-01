using AirportTicketBookingSystemApp.Enums;
using AirportTicketBookingSystemApp.FlightManagement;
using AirportTicketBookingSystemApp.Interfaces;

namespace AirportTicketBookingSystemApp.Services.SearchService
{
    public class ClassFilter : ISearchCriteria<Flight>
    {
        private FlightClassType _class;
        public ClassFilter(FlightClassType flightClass)
        {
            _class = flightClass;
        }

        public bool ApplyFilter(Flight flight)
        {
            if (_class.Equals(FlightClassType.Business))
            {
                return flight.BusinessAvailable > 0;
            }
            else if (_class.Equals(FlightClassType.Economy))
            {
                return flight.EconomiyAvailable > 0;
            }
            else if (_class.Equals(FlightClassType.FirstClass))
            {
                return flight.FirstClassAvailable > 0;
            }

            return false;
        }
    }
}
