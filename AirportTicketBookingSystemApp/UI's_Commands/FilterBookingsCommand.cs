using AirportTicketBookingSystemApp.Enums;
using AirportTicketBookingSystemApp.FlightManagement;
using AirportTicketBookingSystemApp.Interfaces;
using AirportTicketBookingSystemApp.Services.SearchService;

namespace AirportTicketBookingSystemApp.Commands_UI
{
    public class FilterBookingsCommand : IManagerMenuCommands
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
                    Console.WriteLine("enter value:");
                    HandleUserSelection(searchKey, flightParameters, bookingsParameters);
                }
                continue;
            }
            FilteredBookings(flightParameters, bookingsParameters);
            if (_bookings.Count == 0)
            {
                Console.WriteLine("no valid bookings");
            }
            else
            {
                PrintBookings(_bookings);
            }
        }

        private void HandleUserSelection(FilterBookingsKey key, Dictionary<string, object> flightParameters, Dictionary<string, object> bookingsParameters)
        {
            string stringValue = Console.ReadLine() ?? string.Empty;
            if (key.Equals(FilterBookingsKey.ArrivalCountry) ||
                key.Equals(FilterBookingsKey.DepartureCountry) ||
                key.Equals(FilterBookingsKey.DepartureAirport) ||
                key.Equals(FilterBookingsKey.ArrivalAirport)
               )
            {
                if (IsValidString(stringValue)) flightParameters.Add(key.ToString(), stringValue);
            }
            if (key.Equals(FilterBookingsKey.DepartureDate))
            {
                DateTime departureDate = new DateTime().Date;
                if (DateTime.TryParse(stringValue, out departureDate))
                    flightParameters.Add(key.ToString(), departureDate.Date);
            }
            if (key.Equals(FilterBookingsKey.Email) ||
               key.Equals(FilterBookingsKey.BookingClass)
               )
            {
                if (IsValidString(stringValue)) bookingsParameters.Add(key.ToString(), stringValue);
            }
            if (key.Equals(FilterBookingsKey.BookingPrice))
            {
                double value;
                if (double.TryParse(stringValue, out value)) bookingsParameters.Add(key.ToString(), value);
            }
            if (key.Equals(FilterBookingsKey.FlightNumber))
            {
                int value;
                if (int.TryParse(stringValue, out value)) bookingsParameters.Add(key.ToString(), value);
            }
        }
        private bool IsValidString(string text)
        {
            return !string.IsNullOrEmpty(text);
        }

        private void FilteredBookings(Dictionary<string, object> flightParameters, Dictionary<string, object> bookingsParameters)
        {
            List<Flight> flights = new();
            if (flightParameters.Count > 0)
            {
                flights = _filteredSearch.SearchFlight(_systemFlights, flightParameters);
            }
            _bookings = _filteredSearch.SearchBookings(_bookings, flights, bookingsParameters);
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
