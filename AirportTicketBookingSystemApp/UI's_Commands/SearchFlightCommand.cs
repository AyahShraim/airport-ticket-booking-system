using AirportTicketBookingSystemApp.Enums;
using AirportTicketBookingSystemApp.FlightManagement;
using AirportTicketBookingSystemApp.Interfaces;
using AirportTicketBookingSystemApp.PassengerManagement;
using System.Text;

namespace AirportTicketBookingSystemApp.Commands_UI
{
    public class SearchFlightCommand : IPassengerMenuCommands
    {
        private List<Flight> _flights;
        private  List<Flight> _systemFlights = new();
        private PassengerServices passengerServices = new();
        public SearchFlightCommand(List<Flight> flights)
        {
            _flights = flights;
        }
      
        private void PrintSearchMenu()
        {
            Console.WriteLine("Choose one or more of those prameters to search flights");
            Console.WriteLine(@"
1.Departure Country         2.Arrival Country
3.Departure Airport         4.Arrival Airport
5.Departure Date            6.Class
7.Price                     0.Finish

");
        }  
        private bool IsValidString(string text)
        {
            return !string.IsNullOrEmpty(text);
        }

        private void PrintClassesMenu()
        {
            Console.WriteLine("Select a class type:");
            Console.WriteLine("1.Economy");
            Console.WriteLine("2.Business");
            Console.WriteLine("3.First Class");
        }

        private void PrintSearchResult(List<Flight> flights)
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Flight No       Airline     From        To      Departure Airport       Arrival Airport      AtTime     ");
            sb.AppendLine("---------       -------     ----       ----     -----------------       --------------       -----      ");
            flights.ForEach(flight=>
            sb.AppendLine($"{flight.Number,-16}{flight.Airline,-13}{flight.DepartureCountry,-11}{flight.ArrivalCountry,-9}{flight.DepartureAirport,-25}{flight.ArrivalAirport,-16}{flight.DepartureTime.TimeOfDay,-12}")
            );
            sb.ToString();
            Console.WriteLine(sb.ToString());
        }
        private void HandleUserSelection(SearchFlightKeys key, Dictionary<string, object> parameters)
        {
                if (key.Equals(SearchFlightKeys.ArrivalCountry) ||
                    key.Equals(SearchFlightKeys.DepartureCountry) ||
                    key.Equals(SearchFlightKeys.DepartureAirport) ||
                    key.Equals(SearchFlightKeys.ArrivalAirport)
                   )
                {
                    Console.WriteLine("enter value:");
                    string stringValue = Console.ReadLine() ?? string.Empty;
                    if (IsValidString(stringValue))  parameters.Add(key.ToString(), stringValue);
                }
                else if (key.Equals(SearchFlightKeys.MaxPrice))
                {
                    double value;
                    Console.WriteLine("enter upper limit of price:");
                    string stringValue = Console.ReadLine() ?? string.Empty;
                    if(parameters.ContainsKey("Class"))
                    {
                        if (double.TryParse(stringValue, out value)) parameters.Add(key.ToString(), value);
                    }
                    else
                    {
                        Console.WriteLine("Select the class plese");
                        HandleUserSelection(SearchFlightKeys.Class, parameters);
                    }

                }
                else if (key.Equals(SearchFlightKeys.Class))
                {
                    PrintClassesMenu();
                    int selectedClass;
                    if (int.TryParse(Console.ReadLine(), out selectedClass))
                    {
                        FlightClassType classType = (FlightClassType)selectedClass;
                        parameters.Add(key.ToString(), classType);
                    }

                }
                else if (key.Equals(SearchFlightKeys.DepartureDate))
                {
                    DateTime departureDtae = new DateTime().Date;
                    Console.WriteLine("enter Departure date value:");
                    string stringValue = Console.ReadLine() ?? string.Empty;
                    if (DateTime.TryParse(stringValue,out departureDtae))
                    parameters.Add(key.ToString(), departureDtae);
                }
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
                    HandleUserSelection(searchKey,parameters);
                }
                continue;
            }
            if(parameters.Count==0) { Console.WriteLine("No such flight!"); }
            else
            {
                _systemFlights = passengerServices.SearchFlight(_flights, parameters);
                PrintSearchResult(_systemFlights);
            }       
            _systemFlights.Clear();
        }
    }
}
