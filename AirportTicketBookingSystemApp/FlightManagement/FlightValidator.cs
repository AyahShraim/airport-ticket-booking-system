using System.ComponentModel.DataAnnotations;

namespace AirportTicketBookingSystemApp.FlightManagement
{
    internal static class FlightValidator
    {
        public static string ValidateImportedFlights(List<Flight> flights)
        {
            string errorList = "";
            int currentLine = 1;
            foreach(var flight in flights)
            {
                var validationContext = new ValidationContext(flight, null, null);
                var validationResults = new List<ValidationResult>();
                if (!Validator.TryValidateObject(flight, validationContext, validationResults, true))
                {
                    errorList += $"\nline {currentLine} erorrs:";
                    foreach (var result in validationResults)
                    {
                        errorList += $"\n - {result.ErrorMessage}";
                    }
                }
                currentLine++;
            }
            return errorList;
        }
    }
}

