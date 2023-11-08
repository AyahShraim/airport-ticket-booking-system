using AirportTicketBookingSystemApp.FlightManagement;
using AirportTicketBookingSystemApp.Interfaces;

namespace AirportTicketBookingSystemApp.Services.SearchService
{
    public class ArrivalCountryFilter : ISearchCriteria<Flight>
    {
        private string _arrivalCountry;
        public ArrivalCountryFilter(string arrivalCountry)
        {
            _arrivalCountry = arrivalCountry;
        }

        public bool ApplyFilter(Flight flight)
        {
            return flight.ArrivalCountry.Equals(_arrivalCountry);
        }
    }
}
