using AirportTicketBookingSystemApp.PassengerManagement;
using AirportTicketBookingSystemApp.ResultHandler;
using System.ComponentModel.DataAnnotations;

namespace AirportTicketBookingSystemApp.UI_s_Commands
{
    public class PassengerAccountUI
    {
        public static Passenger _currentPassenger = new();
        private PassengerRepository _passengerRepository = new();
        public void RegisterPassenger()
        {
            Console.Write("Enter First name:");
            string firstName = Console.ReadLine() ?? string.Empty;

            Console.Write("Enter Last name:");
            string lastName = Console.ReadLine() ?? string.Empty;

            Console.Write("Enter Email:");
            string email = Console.ReadLine() ?? string.Empty;

            Console.Write("Password:");
            string password = Console.ReadLine() ?? string.Empty;

            Passenger passenger = new Passenger(firstName, lastName, email, password);

            ValidationContext context = new ValidationContext(passenger, null, null);
            List<ValidationResult> validationResults = new List<ValidationResult>();
            bool valid = Validator.TryValidateObject(passenger, context, validationResults, true);
            if (!valid)
            {
                foreach (ValidationResult validationResult in validationResults)
                {
                    Console.WriteLine($"-{validationResult.ErrorMessage}");
                }
            }
            else
            {
                var OperationResult = _passengerRepository.AddNewPassenger(passenger);
                Console.WriteLine(OperationResult.Message);

            }
        }
        public bool PassengerLogIn()
        {
            Console.Write("Enter your email:");
            string email = Console.ReadLine() ?? string.Empty;
            Console.Write("Enter you password");
            string password = Console.ReadLine() ?? string.Empty;

            OperationResult operationResult = _passengerRepository.CheckPassengerInfo(email, password);
            Console.WriteLine(operationResult.Message);
            if (operationResult.IsSuccess)
            {
                _currentPassenger = (Passenger)operationResult.Data;
            }
            return operationResult.IsSuccess;
        }
    }
}
