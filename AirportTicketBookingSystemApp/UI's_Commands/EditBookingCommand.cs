using AirportTicketBookingSystemApp.Enums;
using AirportTicketBookingSystemApp.FlightManagement;
using AirportTicketBookingSystemApp.Interfaces;
using AirportTicketBookingSystemApp.PassengerManagement;
using AirportTicketBookingSystemApp.ResultHandler;
using AirportTicketBookingSystemApp.Utilities;

namespace AirportTicketBookingSystemApp.UI_s_Commands
{
    public class EditBookingCommand : IPassengerMenuCommands
    {
        private BookingRepository _bookingRepository;
        private PassengerServices _passengerServices;
        private FlightServices _flightServices;
        private List<Flight> _systemFlights;
        public EditBookingCommand(List<Flight> flights)
        {
            _bookingRepository = new();
            _passengerServices = new();
            _flightServices = new();
            _systemFlights = flights;
        }
        public void Execute()
        {
            List<FlightBookingModel> bookings = _bookingRepository.LoadBookings(PathsUtilities.bookingsFilePath);
            Console.WriteLine("Enter the booking number to edit");
            string bookingNumber = Console.ReadLine() ?? String.Empty;

            int index = FindBookingIndex(bookings, bookingNumber);
            if (index == -1) { Console.WriteLine("not valid booking number"); return; }

            var flight = FindFlightToEdit(bookings[index]);
            UserInputOutputUtilities.PrintClassesMenu();

            int selection;
            if (int.TryParse(Console.ReadLine(), out selection))
            {
                FlightClassType selectedClass = (FlightClassType)selection;
                if (!_flightServices.FlightClassSeatAvailable(flight, selectedClass))
                {
                    Console.WriteLine("no available seats");
                    return;
                }
                double price = _flightServices.FlightPrice(flight, selectedClass);
                bookings[index].Price = price;
                bookings[index].FlightClass = selectedClass;
                OperationResult result = _bookingRepository.UpdataBookingsRecords(bookings);
                Console.WriteLine(result.Message);
                return;
            }
        }
        private int FindBookingIndex(List<FlightBookingModel> bookings, string bookingNumber)
        {
            return bookings.FindIndex(record => record.BookingNumber.Equals(bookingNumber));
        }
        private Flight FindFlightToEdit(FlightBookingModel booking)
        {
            return _systemFlights.Single(flight => flight.Number == booking.FlightNumber);
        }
    }
}
