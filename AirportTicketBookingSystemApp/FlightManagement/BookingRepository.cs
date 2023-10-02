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
        public List<FlightBookingModel> LoadBookings(string path)
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
            return _Bookings;
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

        public OperationResult DeleteBookingByBookingNo(string bookingNumber, List<FlightBookingModel> bookings)
        {
            int index = bookings.FindIndex(record => record.BookingNumber.Equals(bookingNumber));
            if (index == -1) return OperationResult.FailureResult("No such booking!");
            using var writer = new StreamWriter(Utilities.bookingsFilePath);
            using var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture);
            bookings.RemoveAt(index);
            csvWriter.WriteRecords(bookings);
            return OperationResult.SuccessResult("Deleted succefully!");
        }
    }
}
