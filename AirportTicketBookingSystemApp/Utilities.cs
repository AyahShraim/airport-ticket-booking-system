namespace AirportTicketBookingSystemApp
{
    public class Utilities
    {
        private const string _RootDiectory = @"C:\Users\DELL\source\repos\AirportTicketBookingSystem\AirportTicketBookingSystemApp\Data\";
        private const string _systemFlightFile = "system_flights.csv";
        private const string _bookingsFile = "bookings.csv";
        private const string _usersFile = "users.csv";
        private const string _secretKey = "manager12345";

        public static string SystemFlightsPath => Path.Combine(_RootDiectory, _systemFlightFile);
        public static string UsersFilePath => Path.Combine(_RootDiectory, _usersFile);
        public static string bookingsFilePath => Path.Combine(_RootDiectory, _bookingsFile);
        public static string SecretKey => _secretKey;
    }
}
