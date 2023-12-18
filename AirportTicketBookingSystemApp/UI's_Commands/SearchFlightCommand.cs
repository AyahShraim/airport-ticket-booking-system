using AirportTicketBookingSystemApp.Enums;
using AirportTicketBookingSystemApp.FlightManagement;
using AirportTicketBookingSystemApp.Interfaces;
using AirportTicketBookingSystemApp.Services.SearchService;
using AirportTicketBookingSystemApp.Utilities;
using System.Text;

namespace AirportTicketBookingSystemApp.Commands_UI
{
    public class SearchFlightCommand : IMenuCommands
    {
        private List<Flight> _flights;
        private List<Flight> _systemFlights;
        private FilteredSearch _filteredSearch;
        public SearchFlightCommand(List<Flight> flights)
        {
            _flights = flights;
            _systemFlights = new();
            _filteredSearch = new();
        }
        public void Execute()
        {
            PrintSearchMenu();
            Dictionary<string, object> parameters = new();
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
                    SearchFlightKeys searchKey = (SearchFlightKeys)selection;
                    string value = Console.ReadLine() ?? String.Empty;
                    HandleUserSelection(searchKey, parameters, value);
                }
                continue;
            }
            if(parameters.Count==0) { Console.WriteLine("No such flight!"); }
            else
            {
                _systemFlights = _filteredSearch.SearchFlight(_flights, parameters);
                PrintSearchResult(_systemFlights);
            }       
            _systemFlights.Clear();
        }
        private void PrintSearchMenu()
        {
            Console.WriteLine("Choose one or more of those prameters to search flights");
            Console.WriteLine(@"
1.Departure Country         2.Arrival Country
3.Departure Airport         4.Arrival Airport
5.Departure Date            6.Class
7.Price(Max Limit)          0.Finish
");
        }
        private void HandleUserSelection(SearchFlightKeys key, Dictionary<string, object> parameters, string value)
        {
            if (key.Equals(SearchFlightKeys.ArrivalCountry) ||
                key.Equals(SearchFlightKeys.DepartureCountry) ||
                key.Equals(SearchFlightKeys.DepartureAirport) ||
                key.Equals(SearchFlightKeys.ArrivalAirport))
            {
                if (UserInputOutputUtilities.HandleStringInput(value)) parameters.Add(key.ToString(), value);
            }
            else if (key.Equals(SearchFlightKeys.MaxPrice))
            {
                double? price = UserInputOutputUtilities.HandleDoubleInput(value);
                if(!parameters.ContainsKey("Class"))
                    HandleUserSelection(SearchFlightKeys.Class, parameters, value);
                if (price != null) parameters.Add(key.ToString(), price);
            }
            else if (key.Equals(SearchFlightKeys.Class))
            {
                FlightClassType? classType = UserInputOutputUtilities.HandleClassInput();
                if (classType != null) parameters.Add(key.ToString(), classType);
            }
            else if (key.Equals(SearchFlightKeys.DepartureDate))
            {
                DateTime? departureDate = UserInputOutputUtilities.HandleDateInput(value);
                if (departureDate != null) parameters.Add(key.ToString(), departureDate);
            }
        }
        private void PrintSearchResult(List<Flight> flights)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Flight No       Airline     From        To      Departure Airport       Arrival Airport      AtTime     ");
            sb.AppendLine("---------       -------     ----       ----     -----------------       --------------       -----      ");
            flights.ForEach(flight =>
            sb.AppendLine($"{flight.Number,-16}{flight.Airline,-13}{flight.DepartureCountry,-11}{flight.ArrivalCountry,-9}{flight.DepartureAirport,-25}{flight.ArrivalAirport,-16}{flight.DepartureTime,-12}")
            );
            sb.ToString();
            Console.WriteLine(sb.ToString());
        }
    }
}
