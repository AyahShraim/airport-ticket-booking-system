using AirportTicketBookingSystemApp.Enums;
using AirportTicketBookingSystemApp.FlightManagement;
using AirportTicketBookingSystemApp.Interfaces;

namespace AirportTicketBookingSystemApp.Services.SearchService
{
    public class ClassFilter : ISearchCriteria<Flight>
    {
        private FlightClassType _class;
        private FlightServices _flightServices;
        public ClassFilter(FlightClassType flightClass)
        {
            _class = flightClass;
            _flightServices = new();
        }

        public bool ApplyFilter(Flight flight)
        {
            return _flightServices.FlightClassSeatAvailable(flight, _class);
        }
    }
}
