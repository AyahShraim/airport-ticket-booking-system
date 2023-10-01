using CsvHelper.Configuration.Attributes;

namespace AirportTicketBookingSystemApp.FlightManagement
{
    public class Flight
    {
        [Name("Flight Number")]
        public int Number { get; set; }

        [Name("Airline")]
        public string Airline { get; set; } = String.Empty;

        [Name("Dep Country")]
        public string DepartureCountry { get; set; } = String.Empty;

        [Name("Dep Airport")]
        public string DepartureAirport { get; set; } = String.Empty;

        [Name("Dep Date")]
        public DateTime DepartureDate { get; set; }

        [Name("Dep Time")]
        public DateTime DepartureTime { get; set; }

        [Name("Dep Day")]
        public string DepartureDay { get; set; } = String.Empty;

        [Name("Arrival Country")]
        public string ArrivalCountry { get; set; } = String.Empty;

        [Name("Arrival Airport")]
        public string ArrivalAirport { get; set; } = String.Empty;

        [Name("Arrival Date")]
        public DateTime ArrivalDate { get; set; }

        [Name("Arrival Time")]
        public DateTime ArrivalTime { get; set; }

        [Name("Arrival Day")]
        public string ArrivalDay { get; set; } = String.Empty;

        [Name("Duration")]
        public TimeSpan Duration { get; set; }

        [Name("Capacity")]
        public int TotalCapacity { get; set; }

        [Name("Economy Price")]
        public int EconomiyPrice { get; set; }

        [Name("Economy Capacity")]
        public int EconomiyCapacity { get; set; }

        [Name("Economy Available")]
        public int EconomiyAvailable { get; set; }

        [Name("Business Price")]
        public int BusinessPrice { get; set; }

        [Name("Business Capacity")]
        public int BusinessCapacity { get; set; }

        [Name("Business Available")]
        public int BusinessAvailable { get; set; }

        [Name("First Class Price")]
        public int FirstClassPrice { get; set; }

        [Name("First Class Capacity")]
        public int FirstClassCapacity { get; set; }

        [Name("First Class Available")]
        public int FirstClassAvailable { get; set; }

        [Name("Currency")]
        public string Currency { get; set; } = string.Empty;

    }
}
