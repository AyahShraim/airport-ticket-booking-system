using AirportTicketBookingSystemApp.Enums;
using CsvHelper;
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

    }
}
