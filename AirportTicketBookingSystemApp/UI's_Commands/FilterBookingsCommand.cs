using AirportTicketBookingSystemApp.Enums;
using AirportTicketBookingSystemApp.FlightManagement;
using AirportTicketBookingSystemApp.Interfaces;
using AirportTicketBookingSystemApp.Services.SearchService;
using AirportTicketBookingSystemApp.Utilities;

namespace AirportTicketBookingSystemApp.Commands_UI
{
    public class FilterBookingsCommand : IMenuCommands
    {
        private FilteredSearch _filteredSearch;
        private List<FlightBookingModel> _bookings;
        private List<Flight> _systemFlights;

        public FilterBookingsCommand(List<FlightBookingModel> bookings, List<Flight> flights)
        {
            _filteredSearch = new();
            _bookings = bookings;
            _systemFlights = flights;
        }
        public void Execute()
        {
            Console.WriteLine($"booking count {_bookings.Count}");
            PrintFilterList();
            var flightParameters = new Dictionary<string, object>();
            var bookingsParameters = new Dictionary<string, object>();
            int selection;
            while (true)
            {
                Console.WriteLine("Enter the number of parameter (0 to finish)");
                if (int.TryParse(Console.ReadLine(), out selection))
                {
                    if (selection == 0)
                    {
                        break;
                    }
                    FilterBookingsKey searchKey = (FilterBookingsKey)selection;
                    string value = Console.ReadLine() ?? String.Empty;
                    HandleUserSelection(searchKey, flightParameters, bookingsParameters, value);
                }
                continue;
            }
            var filteredBookings = FilteredBookings(flightParameters, bookingsParameters);
            if (filteredBookings.Count == 0)
            {
                Console.WriteLine("no valid bookings");
            }
            else
            {
                PrintBookings(filteredBookings);
            }
        }
        void PrintFilterList()
        {
            Console.WriteLine("Choose one or more of those prameters to filter bookings");
            Console.WriteLine(@"
1.Departure Country         2.Arrival Country
3.Departure Airport         4.Arrival Airport
5.Departure Date            6.Class
7.Price                     8.Person
9.Flight                    0.Finish

");
        }
        private void HandleUserSelection(FilterBookingsKey key, Dictionary<string, object> flightParameters, Dictionary<string, object> bookingsParameters, string value)
        {
            if (key.Equals(FilterBookingsKey.ArrivalCountry) ||
                key.Equals(FilterBookingsKey.DepartureCountry) ||
                key.Equals(FilterBookingsKey.DepartureAirport) ||
                key.Equals(FilterBookingsKey.ArrivalAirport))
            {
                if (UserInputOutputUtilities.HandleStringInput(value)) flightParameters.Add(key.ToString(), value);
            }
            else if (key.Equals(FilterBookingsKey.DepartureDate))
            {
                DateTime? departureDate = UserInputOutputUtilities.HandleDateInput(value);
                if (departureDate != null) flightParameters.Add(key.ToString(), departureDate);
            }
            else if (key.Equals(FilterBookingsKey.Email))
            {
                if (UserInputOutputUtilities.HandleStringInput(value)) bookingsParameters.Add(key.ToString(), value);
            }
            else if(key.Equals(FilterBookingsKey.BookingClass))
            {
                FlightClassType? classType = UserInputOutputUtilities.HandleClassInput();
                if (classType != null) bookingsParameters.Add(key.ToString(), classType);
            }
            else if (key.Equals(FilterBookingsKey.BookingPrice))
            {
                double? price = UserInputOutputUtilities.HandleDoubleInput(value);
                if (price != null) bookingsParameters.Add(key.ToString(), price);
            }
            else if (key.Equals(FilterBookingsKey.FlightNumber))
            {
                int? flightNumber = UserInputOutputUtilities.HandleIntInput(value);
                if (flightNumber != null ) bookingsParameters.Add(key.ToString(), flightNumber);
            }
        }
        private List<FlightBookingModel> FilteredBookings(Dictionary<string, object> flightParameters, Dictionary<string, object> bookingsParameters)
        {
            List<Flight> flights = new();
            if (flightParameters.Count > 0)
            {
                flights = _filteredSearch.SearchFlight(_systemFlights, flightParameters);
            }
            return  _filteredSearch.SearchBookings(_bookings, flights, bookingsParameters);
        }

        private void PrintBookings(List<FlightBookingModel> flightBookings)
        {
            Console.WriteLine(@"Flight Number        booking Email       Price       Class ");
            foreach (var book in flightBookings)
            {
                Console.WriteLine($"{book.FlightNumber,-21} {book.Email,-17} {book.Price,-17}{book.FlightClass}");
            }
        }
    }
}
