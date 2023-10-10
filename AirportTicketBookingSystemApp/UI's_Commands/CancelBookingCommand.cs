﻿using AirportTicketBookingSystemApp.FlightManagement;
using AirportTicketBookingSystemApp.Interfaces;
using AirportTicketBookingSystemApp.ResultHandler;
using AirportTicketBookingSystemApp.Utilities;

namespace AirportTicketBookingSystemApp.Commands_UI
{
    public class CancelBookingCommand : IMenuCommands
    {
        private BookingRepository _bookingRepository;
        public CancelBookingCommand()
        {
            _bookingRepository = new();
        }
        public void Execute()
        {
            List <FlightBookingModel> bookings = _bookingRepository.LoadBookings(PathsUtilities.bookingsFilePath);
            Console.WriteLine("write the booking number to delete your bookings :");
            string bookingNumber = Console.ReadLine() ?? String.Empty; 
            OperationResult result = _bookingRepository.DeleteBookingByBookingNo(bookingNumber, bookings);
            Console.WriteLine(result.Message);
        }
    }
}
