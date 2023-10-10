using AirportTicketBookingSystemApp.FlightManagement;
using AirportTicketBookingSystemApp.Interfaces;

namespace AirportTicketBookingSystemApp.Services.SearchService
{
    public class DepartureDateFilter : ISearchCriteria<Flight>
    {
        private DateTime _depatureDate;
        public DepartureDateFilter(DateTime depatureDate)
        {
            _depatureDate = depatureDate.Date;
        }

        public bool ApplyFilter(Flight flight)
        {
            return flight.DepartureDate.Date == _depatureDate;
        }
    }
}

