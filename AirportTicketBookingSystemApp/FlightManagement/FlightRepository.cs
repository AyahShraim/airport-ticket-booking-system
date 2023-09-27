using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystemApp.FlightManagement
{
    public class FlightRepository
    {
        public List<Flight> UploadFlights(string path)
        {
            List<Flight> flights;
            try
            {
                using (var reader = new StreamReader(path))
                {
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        flights = csv.GetRecords<Flight>().ToList();
                    }

                }
            }
            catch (IOException)
            {
                Console.WriteLine("no such file");
                flights = new List<Flight>();
            }
            return flights;
        }
    }
}
