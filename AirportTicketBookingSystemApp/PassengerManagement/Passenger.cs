using CsvHelper.Configuration.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AirportTicketBookingSystemApp.PassengerManagement
{
    public class Passenger
    {
        [Name("First Name")]
        [Required(ErrorMessage ="First Name is required")]
        public string FirstName { get; set; } = string.Empty;

        [Name("Last Name")]
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; } = string.Empty;

        [Name("Email")]
        [EmailAddress(ErrorMessage ="Not valid email address")]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; } = string.Empty;

        [Name("Password")]
        [Required(ErrorMessage = "Password is required")]
        [PasswordPropertyText(true)]
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
