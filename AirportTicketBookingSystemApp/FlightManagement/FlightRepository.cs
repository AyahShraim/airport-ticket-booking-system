using AirportTicketBookingSystemApp.Enums;
using AirportTicketBookingSystemApp.ResultHandler;
using AirportTicketBookingSystemApp.Utilities;
using CsvHelper;
using CsvHelper.TypeConversion;
using System.Globalization;

namespace AirportTicketBookingSystemApp.FlightManagement
{
    public class FlightRepository
    {
        private static List<Flight> _systemFlights = new List<Flight>();
        public List<Flight> SystemFlights
        {
            get => _systemFlights;

        }
        public OperationResult BatchFileUpload(string path)
        {
            var systemFlights = LoadFlights(PathsUtilities.SystemFlightsPath);
            List<Flight> uploadedFlights;
            string validationErrors = "";
            try
            {
                try
                {
                    uploadedFlights = LoadFlights(path);
                    validationErrors = FlightValidator.ValidateImportedFlights(uploadedFlights);
                    if (string.IsNullOrEmpty(validationErrors))
                    {
                        int maxFlightCount = systemFlights.Count;
                        foreach (var item in uploadedFlights)
                        {
                            item.Number = maxFlightCount++;
                        }
                        systemFlights.AddRange(uploadedFlights.ToList());
                        SaveNewFlightsToSystem(PathsUtilities.SystemFlightsPath, systemFlights);
                        return OperationResult.SuccessResult("flights added successfully");
                    }
                }
                //TODO: include type conversion in error list
                catch (TypeConverterException ex)
                {
                    validationErrors += $"Error type for {ex.Text} {ex.MemberMapData.Member} {ex.TypeConverter}";
                }
               return OperationResult.FaiulreDataMessage("validation errors", validationErrors);
            }
            catch (Exception ex)
            {
                return OperationResult.FailureResult($"Exception: {ex.Message}");
            }
        } 
        public List<Flight> LoadFlights(string path)
        {
            try
            {
                using (var reader = new StreamReader(path))
                {
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        return  csv.GetRecords<Flight>().ToList();
                         
                    }
                }
            }
            catch
            {
                return new List<Flight>();
            }
        }
        public void SaveNewFlightsToSystem(string path, List<Flight> flights)
        {
            using var writer = new StreamWriter(path, append: false);
            using var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csvWriter.WriteRecords(flights);
        }
        public void DecreaseAvailableSeats(int flightNumber, FlightClassType flightClass, List<Flight> flights)
        {
            int flightIndex = flights.FindIndex(flight => flight.Number == flightNumber);
            switch (flightClass)
            {
                case FlightClassType.FirstClass:
                    _systemFlights[flightIndex].FirstClassAvailable--;
                    break;
                case FlightClassType.Economy:
                    _systemFlights[flightIndex].EconomiyAvailable--;
                    break;
                case FlightClassType.Business:
                    _systemFlights[flightIndex].BusinessAvailable--;
                    break;
            }
            SaveNewFlightsToSystem(PathsUtilities.SystemFlightsPath, flights);
        }
    }
}
