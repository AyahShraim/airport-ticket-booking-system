﻿
using AirportTicketBookingSystemApp.Enums;
using AirportTicketBookingSystemApp.FlightManagement;
using AirportTicketBookingSystemApp.Interfaces;
using AirportTicketBookingSystemApp.PassengerManagement;
using AirportTicketBookingSystemApp.ResultHandler;
using AirportTicketBookingSystemApp.Utilities;

namespace AirportTicketBookingSystemApp.Commands_UI
{
    public class BookingFlightCommand : IMenuCommands
    {
        private List<Flight> _systemFlights;
        private PassengerServices passengerServices = new();
        private Passenger _currentPassenger;
        private FlightServices _flightServices;
        private FlightRepository _flightRepository;

        public BookingFlightCommand(List<Flight> systemFlights, Passenger passenger)
        {
            _systemFlights = systemFlights;
            _currentPassenger = passenger;
            _flightServices = new();
            _flightRepository = new();
        }

        private Flight? ValidFlightNumber()
        {
            Console.WriteLine("Enter the flight number:");
            string flightNumber = Console.ReadLine() ?? string.Empty;
            return _systemFlights.FirstOrDefault(flight => flight.Number.ToString().Equals(flightNumber));
        }
        private double checkingPrice(Flight flight, FlightClassType classType)
        {
            double price = _flightServices.FlightPrice(flight, classType);
            Console.WriteLine($"the price for {classType} class is {price} {flight.Currency}");
            return price;
        }
        private bool confirmBooking()
        {
            Console.WriteLine("Enter K to confirm the booking of the flight");
            char key = Console.ReadKey().KeyChar;
            if (key is 'K') return true;
            else return false;
        }
        public void Execute()
        {
            var flight = ValidFlightNumber();
            if (flight == null)
            {
                Console.WriteLine("Not valid Flight Number");
                return;
            }
            else
            {
                UserInputOutputUtilities.PrintClassesMenu();
                int selection;
                if (int.TryParse(Console.ReadLine(), out selection))
                {
                    FlightClassType selectedClass = (FlightClassType)selection;
                    double price = checkingPrice(flight, selectedClass);
                    if (!confirmBooking())
                    {
                        Console.WriteLine("\nyou didn't confirm booking!");
                        return;
                    }
                    OperationResult result = passengerServices.BookingFlight(flight, selectedClass, _currentPassenger.Email, price);
                    _flightRepository.DecreaseAvailableSeats(flight.Number, selectedClass, _systemFlights);
                    Console.WriteLine(result.Message);
                    if (result.IsSuccess) Console.WriteLine($"booking number :{result.Data}");
                    return;
                }
            }
        }
    }
}