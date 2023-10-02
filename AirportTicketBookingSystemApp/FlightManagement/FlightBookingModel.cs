using AirportTicketBookingSystemApp.Enums;
using CsvHelper.Configuration.Attributes;

namespace AirportTicketBookingSystemApp.FlightManagement
{
    public class FlightBookingModel
    {
        public FlightBookingModel()
        {
            BookingNumber = $"{DateTime.Now:yyMMddHHmmssffff}-{Guid.NewGuid():N}";
        }
        public FlightBookingModel(int flightNumber, string email, FlightClassType flightClass, double price)
        {
            BookingNumber = $"{DateTime.Now:yyMMddHHmmssffff}-{Guid.NewGuid():N}";
            FlightNumber = flightNumber;
            Email = email;
            Price = price;
            FlightClass = flightClass;
        }

        [Name("Booking Number")]
        public string BookingNumber { get; private set; }

        [Name("Flight Number")]
        public int FlightNumber { get; set; }

        [Name("Passenger Email")]
        public string Email { get; set; } = String.Empty;

        [Name("CLass")]
        public FlightClassType FlightClass { get; set; }

        [Name("Price")]
        public double Price { get; set; }
    }
}

