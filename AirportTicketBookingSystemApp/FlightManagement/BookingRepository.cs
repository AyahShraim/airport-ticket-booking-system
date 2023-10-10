using AirportTicketBookingSystemApp.ResultHandler;
using AirportTicketBookingSystemApp.Utilities;
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
            using var writer = new StreamWriter(PathsUtilities.bookingsFilePath, append: true);
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
            var bookings = LoadBookings(PathsUtilities.bookingsFilePath);
            return bookings
            .Where(record => record.Email.Equals(passengerEmail))
            .ToList();
        }
        public OperationResult DeleteBookingByBookingNo(string bookingNumber, List<FlightBookingModel> bookings)
        {
            int index = bookings.FindIndex(record => record.BookingNumber.Equals(bookingNumber));
            if (index == -1) return OperationResult.FailureResult("No such booking!");
            using var writer = new StreamWriter(PathsUtilities.bookingsFilePath);
            using var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture);
            bookings.RemoveAt(index);
            csvWriter.WriteRecords(bookings);
            return OperationResult.SuccessResult("Deleted succefully!");
        }

        public OperationResult UpdataBookingsRecords(List<FlightBookingModel> bookings)
        {
            using var writer = new StreamWriter(PathsUtilities.bookingsFilePath, append: false);
            using var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csvWriter.WriteRecords(bookings);
            return OperationResult.SuccessResult($"Booking updated successfully!");
        }
    }
}
