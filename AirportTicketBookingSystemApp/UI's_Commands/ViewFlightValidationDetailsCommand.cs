using AirportTicketBookingSystemApp.FlightManagement;
using AirportTicketBookingSystemApp.Interfaces;
using AirportTicketBookingSystemApp.Validators;

namespace AirportTicketBookingSystemApp.UI_s_Commands
{
    internal class ViewFlightValidationDetailsCommand : IManagerMenuCommands
    {
        private FlightRepository _flightRepository;
        public ViewFlightValidationDetailsCommand()
        {
            _flightRepository = new();
        }
        public void Execute()
        {
            Console.WriteLine("your Flight Data should have those validations :");
            List<string> validationDetails  = ModelValidator.GenerateDynamicValidationDetails<Flight>();
            foreach (var detail in validationDetails)
            {
                Console.WriteLine(detail);
            }
        }
    }
}
