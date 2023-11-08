using AirportTicketBookingSystemApp.FlightManagement;
using AirportTicketBookingSystemApp.Interfaces;

namespace AirportTicketBookingSystemApp.Services.SearchService
{
    public class DepartureAirportFilter : ISearchCriteria<Flight>
    {
        private string _departureAirport;
        public DepartureAirportFilter(string departureAirport)
        {
            _departureAirport = departureAirport;
        }

        public bool ApplyFilter(Flight flight)
        {
            return flight.DepartureAirport.Equals(_departureAirport);
        }
    }
}

