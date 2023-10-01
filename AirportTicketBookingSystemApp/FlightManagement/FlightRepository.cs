using CsvHelper;
using System.Globalization;

namespace AirportTicketBookingSystemApp.FlightManagement
{
    public class FlightRepository
    {
        private List<Flight> _systemFlights = new List<Flight>();
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
    }
}
