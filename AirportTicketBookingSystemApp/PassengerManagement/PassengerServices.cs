

using AirportTicketBookingSystemApp.Enums;
using AirportTicketBookingSystemApp.FlightManagement;
using AirportTicketBookingSystemApp.Interfaces;
using AirportTicketBookingSystemApp.ResultHandler;
using AirportTicketBookingSystemApp.Services.SearchService;

namespace AirportTicketBookingSystemApp.PassengerManagement
{
    public class PassengerServices
    {
        private BookingRepository _bookingRepository;
        private FlightRepository _flightRepository;
        private FlightServices _flightServices;

        public PassengerServices()
        {
            _bookingRepository = new();
            _flightRepository = new();
            _flightServices = new();
        }

        private List<ISearchCriteria<Flight>> SearchCriteria(Dictionary<string, object> parameters)
        {
            List<ISearchCriteria<Flight>> searchCriteria = new List<ISearchCriteria<Flight>>();

            bool IsKey(string key)
            {
                return parameters.ContainsKey(key);
            }

            if (IsKey("DepartureCountry"))
            {
                searchCriteria.Add(new DepartureCountryFilter((string)parameters["DepartureCountry"]));
            }

            if (IsKey("ArrivalCountry"))
            {
                searchCriteria.Add(new ArrivalCountryFilter((string)parameters["ArrivalCountry"]));
            }

            if (IsKey("DepartureAirport"))
            {
                searchCriteria.Add(new DepartureAirportFilter((string)parameters["DepartureAirport"]));
            }

            if (IsKey("ArrivalAirport"))
            {
                searchCriteria.Add(new ArrivalAirportFilter((string)parameters["ArrivalAirport"]));
            }

            if (IsKey("DepartureDate"))
            {
                searchCriteria.Add(new DepartureDateFilter((DateTime)parameters["DepartureDate"]));
            }
            if (IsKey("Class"))
            {
                searchCriteria.Add(new ClassFilter((FlightClassType)parameters["Class"]));
            }

            if (IsKey("MaxPrice"))
            {
                searchCriteria.Add(new PriceFilter((double)parameters["Price"], (FlightClassType)parameters["Class"]));
            }
            return searchCriteria;
        }
        public List<Flight> SearchFlight(List<Flight> flights, Dictionary<string, object> parameters)
        {
            List<ISearchCriteria<Flight>> searchCriterias = new List<ISearchCriteria<Flight>>();
            searchCriterias = SearchCriteria(parameters);
            List<Flight> filteredFlights = new List<Flight>(flights);
            filteredFlights = flights.Where(flight => searchCriterias.All(criteria => criteria.ApplyFilter(flight))).ToList();
            return filteredFlights;
        }

        public OperationResult BookingFlight(Flight flight, FlightClassType flightClassType, string email, double price)
        {
            bool isAvailable = _flightServices.FlightClassSeatAvailable(flight, flightClassType);
            if (!isAvailable)
            {
                return OperationResult.FailureResult("No Availabe seats");
            }

            FlightBookingModel flightBookingModel = new(flight.Number, email, flightClassType, price);
            _bookingRepository.AddNewBooking(flightBookingModel);
            _flightRepository.DecreaseAvailableSeats(flight.Number, flightClassType);

            return OperationResult.SuccessResult("your booking set succefully }");
        }
    }
}
