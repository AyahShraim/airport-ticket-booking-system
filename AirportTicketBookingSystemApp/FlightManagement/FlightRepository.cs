using AirportTicketBookingSystemApp.Enums;
using CsvHelper;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace AirportTicketBookingSystemApp.FlightManagement
{
    public class FlightRepository
    {
        private static List<Flight> _systemFlights = new List<Flight>();
        public void LoadFlights(string path)
        {
            List<Flight> flights = new List<Flight>(); ;
            try
            {
                using (var reader = new StreamReader(path))
                {
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        _systemFlights = csv.GetRecords<Flight>().ToList();
                    }
                }
            }
            catch (IOException)
            {
                Console.WriteLine("problem when trying to read the file");
            }
        }

        public async Task LoadFligtsAsync(string path)
        {
            await Task.Run(() =>
            {
                LoadFlights(path);
            });
        }

        public List<Flight> SystemFlights
        {
            get => _systemFlights;
        }

        public void DecreaseAvailableSeats(int flightNumber, FlightClassType flightClass)
        {
            int flightIndex = _systemFlights.FindIndex(flight => flight.Number == flightNumber);
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

            using var writer = new StreamWriter(Utilities.SystemFlightsPath);
            using var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csvWriter.WriteRecords(_systemFlights);
        }

        public void BatchFlightUpload(string path)
        {
            try
            {
                LoadFlights(Utilities.SystemFlightsPath);
                using (var reader = new StreamReader(path))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var newFlights = csv.GetRecords<Flight>().ToList();
                    var isValid = true;

                    foreach (var flight in newFlights)
                    {
                        Console.WriteLine($"Validating Flight {flight.Number}");
                        var validationContext = new ValidationContext(flight, null, null);
                        var validationResults = new List<ValidationResult>();

                        if (!Validator.TryValidateObject(flight, validationContext, validationResults, true))
                        {
                            Console.WriteLine($"Validation Errors for Flight {flight.Number}:");
                            foreach (var result in validationResults)
                            {
                                Console.WriteLine($" - {result.ErrorMessage}");
                            }

                            isValid = false;
                        }
                    }

                    if (isValid)
                    {
                        _systemFlights.AddRange(newFlights);
                        AppendflightstoFile(Utilities.SystemFlightsPath);
                        Console.WriteLine("Flights added Successfully");
                    }
                    else
                    {
                        Console.WriteLine("Check the erorr list.");
                    }
                }
            }
            catch (IOException)
            {
                Console.WriteLine("Problem when trying to read the file.");
            }
        }

        public void AppendflightstoFile(string path)
        {
            using var writer = new StreamWriter(path, append: false);
            using var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture);

            csvWriter.WriteRecords(_systemFlights);
        }

    }
}
