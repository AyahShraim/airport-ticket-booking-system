using CsvHelper.Configuration.Attributes;
using System.ComponentModel.DataAnnotations;

namespace AirportTicketBookingSystemApp.FlightManagement
{
    public class Flight
    {
        [Name("Flight Number")]
        [Required]
        public int Number { get; set; }

        [Name("Airline")]
        [Required]
        public string Airline { get; set; } = String.Empty;

        [Name("Dep Country")]
        [Required]
        public string DepartureCountry { get; set; } = String.Empty;

        [Name("Dep Airport")]
        [Required]
        public string DepartureAirport { get; set; } = String.Empty;

        [Name("Dep Date")]
        [Required]
        [FutureDateRange]
        public DateTime DepartureDate { get; set; }

        [Name("Dep Time")]
        [Required]
        [DataType(DataType.Time)]
        public TimeOnly DepartureTime { get; set; }

        [Name("Arrival Country")]
        [Required]
        public string ArrivalCountry { get; set; } = String.Empty;

        [Name("Arrival Airport")]
        [Required]
        public string ArrivalAirport { get; set; } = String.Empty;

        [Name("Arrival Date")]
        [Required]
        [FutureDateRange]
        public DateTime ArrivalDate { get; set; }

        [Name("Arrival Time")]
        [Required]
        [DataType(DataType.Time)]
        public TimeOnly ArrivalTime { get; set; }

        [Name("Economy Price")]
        [Required]
        [Range(1, double.MaxValue)]
        public double EconomiyPrice { get; set; }

        [Name("Economy Available")]
        [Required]
        [Range(0, int.MaxValue)]
        public int EconomiyAvailable { get; set; }

        [Name("Business Price")]
        [Required]
        [Range(1, double.MaxValue)]
        public double BusinessPrice { get; set; }

        [Name("Business Available")]
        [Required]
        [Range(0, int.MaxValue)]
        public int BusinessAvailable { get; set; }

        [Name("First Class Price")]
        [Required]
        [Range(1, double.MaxValue)]
        public double FirstClassPrice { get; set; }

        [Name("First Class Available")]
        [Range(0, int.MaxValue)]
        [Required]
        public int FirstClassAvailable { get; set; }

        [Name("Currency")]
        [Required]
        public string Currency { get; set; } = string.Empty;
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class FutureDateRangeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateTime date && date >= DateTime.Now)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult($"Departure date must be in range {DateTime.Now.Date} -> future.");
        }
    }
}
