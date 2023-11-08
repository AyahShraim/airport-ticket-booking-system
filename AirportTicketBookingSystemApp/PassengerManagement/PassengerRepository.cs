using System.Globalization;
using AirportTicketBookingSystemApp.ResultHandler;
using AirportTicketBookingSystemApp.Utilities;
using CsvHelper;


namespace AirportTicketBookingSystemApp.PassengerManagement
{
    public class PassengerRepository
    {
        public OperationResult AddNewPassenger(Passenger passenger)
        {
            if (IsExistPassenger(passenger.Email))
            {
                return OperationResult.FailureResult("email already exist, try different email or login!");
            }
            using var writer = new StreamWriter(PathsUtilities.UsersFilePath, append: true);
            using var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csvWriter.WriteRecord(passenger);
            csvWriter.NextRecord();
            return OperationResult.SuccessResult("Account created successfully!");
        }
        private bool IsExistPassenger(string email)
        {
            using (var reader = new StreamReader(PathsUtilities.UsersFilePath))
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
            using (var reader = new StreamReader(PathsUtilities.UsersFilePath))
            {
                using var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
                var passenger = csvReader
                  .GetRecords<Passenger>()
                  .FirstOrDefault(value => value.Email.Equals(email) && value.Password.Equals(password));

                if (passenger != null)
                {
                    return OperationResult.SuccessDataMessage("....................", passenger);
                }
                else
                {
                    return OperationResult.FailureResult("Passenger not found.");
                }
            }
        }
    }
}