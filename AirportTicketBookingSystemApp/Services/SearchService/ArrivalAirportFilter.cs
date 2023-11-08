using AirportTicketBookingSystemApp.FlightManagement;
using AirportTicketBookingSystemApp.Interfaces;

namespace AirportTicketBookingSystemApp.Services.SearchService
{
    public class ArrivalAirportFilter : ISearchCriteria<Flight>
    {
        private string _arrivalAirport;
        public ArrivalAirportFilter(string arrivalAirport)
        {
            _arrivalAirport = arrivalAirport;
        }

        public bool ApplyFilter(Flight flight)
        {
            return flight.ArrivalAirport.Equals(_arrivalAirport);
        }
    }
}
