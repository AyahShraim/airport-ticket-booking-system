using AirportTicketBookingSystemApp.FlightManagement;
using AirportTicketBookingSystemApp.Interfaces;

namespace AirportTicketBookingSystemApp.Services.SearchService
{
    public class DepartureCountryFilter : ISearchCriteria<Flight>
    {
        private string _departureCountry;
        public DepartureCountryFilter(string departureCountry)
        {
            _departureCountry = departureCountry;
        }

        public bool ApplyFilter(Flight flight)
        {
            return flight.DepartureCountry.Equals(_departureCountry);
        }
    }

  
}
