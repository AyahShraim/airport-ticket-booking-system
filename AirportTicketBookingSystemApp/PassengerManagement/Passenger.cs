using CsvHelper.Configuration.Attributes;

namespace AirportTicketBookingTry.PassengerManagement
{
    public class Passenger
    {
        [Name("First Name")]
        public string FirstName { get; set; } = string.Empty;

        [Name("Last Name")]
        public string LastName { get; set; } = string.Empty;

        [Name("Email")]
        public string Email { get; set; } = string.Empty;

        [Name("Password")]
        public string Password { get; set; } = string.Empty;

        public Passenger(string firstName, string lastName, string email, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
        }
        public Passenger()
        {

        }
    }
}
