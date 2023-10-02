

using AirportTicketBookingSystemApp.FlightManagement;
using AirportTicketBookingSystemApp.Interfaces;
using AirportTicketBookingSystemApp.PassengerManagement;
using System.Text;

namespace AirportTicketBookingSystemApp.Commands_UI
{
    public class ViewPersonalBookingsCommand : IPassengerMenuCommands
    {
        private Passenger _currentPassenger;
        private List<Flight> _systemFlights;
        private BookingRepository _bookingRepository;
        public ViewPersonalBookingsCommand(Passenger passenger, List<Flight> systemFlights)
        {
            _currentPassenger = passenger;
            _bookingRepository = new();
            _systemFlights = systemFlights;
        }
        private void printBookings(List<FlightBookingModel> bookings)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(" Booking Number                                      Class        Price             From         To    ");
            sb.AppendLine("----------------                                     ------       ------            ------       ------");
            foreach(var record in bookings)
            {
                var flight = _systemFlights.Find(f => f.Number == record.FlightNumber);
                sb.AppendLine($"{record.BookingNumber,-54}{record.FlightClass,-13}{record.Price} {record.Currency,-13}{flight?.DepartureCountry,-13}{flight?.ArrivalCountry,-9}") ;
   
            }
            sb.ToString();
            Console.WriteLine(sb.ToString());

        }
        public void Execute()
        {
            List<FlightBookingModel> bookings= _bookingRepository.ReadBookingsByEmail(_currentPassenger.Email);
            printBookings(bookings);
        }
    }
    
}
