
using AirportTicketBookingSystemApp.Enums;
using AirportTicketBookingSystemApp.FlightManagement;
using AirportTicketBookingSystemApp.Interfaces;
using AirportTicketBookingSystemApp.PassengerManagement;
using AirportTicketBookingSystemApp.ResultHandler;

namespace AirportTicketBookingSystemApp.Commands_UI
{
    public class BookingFlightCommand : IPassengerMenuCommands
    {
        private List<Flight> _systemFlights;
        private PassengerServices passengerServices = new();
        private Passenger _currentPassenger;

        public BookingFlightCommand(List<Flight> systemFlights,Passenger passenger)
        {
            _systemFlights = systemFlights;
            _currentPassenger = passenger;
        }
        private void PrintClassesMenu()
        {
            Console.WriteLine("Select a class type:");
            Console.WriteLine("1.Economy");
            Console.WriteLine("2.Business");
            Console.WriteLine("3.First Class");
        }
        private Flight? ValidFlightNumber()
        {
            Console.WriteLine("Enter the flight number:");
            string flightNumber = Console.ReadLine() ?? string.Empty;
            return _systemFlights.FirstOrDefault(flight => flight.Number.ToString().Equals(flightNumber));
        }
        public void Execute()
        {
            var flight =ValidFlightNumber();
            if (flight == null)
            {
                Console.WriteLine("Not valid Flight Number");
                return;
            }
            else
            {
                PrintClassesMenu();
                int selection;
                if(int.TryParse(Console.ReadLine(), out selection))
                {
                    FlightClassType selected = (FlightClassType)selection;
                    FlightServices flightServices = new();
                    double price = flightServices.FlightPrice(flight, selected);
                    Console.WriteLine($"the price for {selected} class is {price} {flight.Currency}");
                    OperationResult result = passengerServices.BookingFlight(flight, selected, _currentPassenger.Email, price);
                    Console.WriteLine(result.Message);
                    return;
                }
            }
        }
    }
}
