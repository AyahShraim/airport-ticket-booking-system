using AirportTicketBookingSystemApp.ResultHandler;
using CsvHelper;

using System.Globalization;

namespace AirportTicketBookingSystemApp.FlightManagement
{
    public class BookingRepository
    {
        private List<FlightBookingModel> _Bookings;
        public BookingRepository()
        {
            _Bookings = new();
        }

        public OperationResult AddNewBooking(FlightBookingModel flightBooking)
        {

            using var writer = new StreamWriter(Utilities.bookingsFilePath, append: true);
            using var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture);

            csvWriter.WriteRecord(flightBooking);
            csvWriter.NextRecord();

            return OperationResult.SuccessResult($"Booking set successfully!");
        }
        public void LoadBookings(string path)
        {
            try
            {
                using (var reader = new StreamReader(path))
                {
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        _Bookings = csv.GetRecords<FlightBookingModel>().ToList();
                    }
                }
            }
            catch (IOException)
            {
                Console.WriteLine("problem when trying to read the file");
            }
        }

        public List<FlightBookingModel> ReadBookingsByEmail(string passengerEmail)
        {
            try
            {
                using (var reader = new StreamReader(Utilities.bookingsFilePath))
                {
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        _Bookings = csv.GetRecords<FlightBookingModel>()
                            .Where(record => record.Email.Equals(passengerEmail))
                            .ToList();
                    }
                }
            }
            catch (IOException)
            {
                Console.WriteLine("problem when trying to read the file");
            }
            return _Bookings;
        }
    }
}
