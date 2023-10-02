using AirportTicketBookingSystemApp.Enums;
using CsvHelper.Configuration.Attributes;

namespace AirportTicketBookingSystemApp.FlightManagement
{
    public class FlightBookingModel
    {
        public FlightBookingModel()
        {

        }
        public FlightBookingModel(int flightNumber, string email, FlightClassType flightClass, double price, string currency, string bookingNumber)
        {
            BookingNumber = bookingNumber;
            FlightNumber = flightNumber;
            Email = email;
            Price = price;
            FlightClass = flightClass;
            Currency = currency;
        }

        [Name("Booking Number")]
        public string BookingNumber { get; set; } = String.Empty;

        [Name("Flight Number")]
        public int FlightNumber { get; set; }

        [Name("Passenger Email")]
        public string Email { get; set; } = String.Empty;

        [Name("Class")]
        public FlightClassType FlightClass { get; set; }

        [Name("Price")]
        public double Price { get; set; }

        [Name("Currency")]
        public string Currency { get; set; } = String.Empty;

        public static string GenerateBookingNumber()
        {
            return $"{DateTime.Now:yyMMddHHmmssffff}-{Guid.NewGuid():N}";
        }
    }
}

