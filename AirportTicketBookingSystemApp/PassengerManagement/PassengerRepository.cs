using System.Globalization;
using AirportTicketBookingTry.ResultHandler;
using CsvHelper;


namespace AirportTicketBookingTry.PassengerManagement
{
    internal class PassengerRepository
    {
        private string _directory = @"C:\Users\DELL\source\repos\AirportTicketBookingSystem\AirportTicketBookingSystemApp\Data\";
        private string _usersFileName = "users.csv";

        public OperationResult AddNewPassenger(string firstName, string lastName, string email, string password)
        {
            if (IsExistPassenger(email))
            {
                return OperationResult.FailureResult("email already exist, try different email or login!");
            }
            using var writer = new StreamWriter($"{_directory}{_usersFileName}", append: true);
            using var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture);

            Passenger _passenger = new Passenger(firstName, lastName, email, password);
            csvWriter.WriteRecord(_passenger);
            csvWriter.NextRecord();

            return OperationResult.SuccessResult("Account created successfully!");
        }

        private bool IsExistPassenger(string email)
        {
            using (var reader = new StreamReader($"{_directory}{_usersFileName}"))
            {
                using var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
                return csvReader
                .GetRecords<Passenger>()
                .Select(value => value.Email)
                .Any(value => value.Equals(email) && !string.IsNullOrEmpty(value));
            }
        }

        public OperationResult CheckPassengerInfo(string email, string password)
        {
            using (var reader = new StreamReader($"{_directory}{_usersFileName}"))
            {
                using var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
                var passenger = csvReader
                  .GetRecords<Passenger>()
                  .FirstOrDefault(value => value.Email.Equals(email) && value.Password.Equals(password));

                if (passenger != null)
                {
                    return OperationResult.SuccessResult("Passenger found.");
                }
                else
                {
                    return OperationResult.FailureResult("Passenger not found.");
                }

            }
        }
    }
}