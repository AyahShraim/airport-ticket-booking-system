

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
        public OperationResult BookingFlight(Flight flight, FlightClassType flightClassType, string email, double price)
        {
            bool isAvailable = _flightServices.FlightClassSeatAvailable(flight, flightClassType);
            if (!isAvailable)
            {
                return OperationResult.FailureResult("\nNo Availabe seats");
            }

            FlightBookingModel flightBookingModel = new(flight.Number, email, flightClassType, price, flight.Currency, FlightBookingModel.GenerateBookingNumber());
            _bookingRepository.AddNewBooking(flightBookingModel);
            _flightRepository.DecreaseAvailableSeats(flight.Number, flightClassType);
            return OperationResult.SuccessDataMessage("\nyour booking set succefully! ", flightBookingModel.BookingNumber);
        }
    }
}
