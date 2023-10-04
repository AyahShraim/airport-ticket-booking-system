using AirportTicketBookingSystemApp.FlightManagement;
using AirportTicketBookingSystemApp.Interfaces;


namespace AirportTicketBookingSystemApp.Services.SearchService.SearchFilter
{
    public class BookingClassFilter : ISearchCriteria<FlightBookingModel>
    {
        private string _class;
        public BookingClassFilter(string bookedClass)
        {
            _class = bookedClass;
        }
        public bool ApplyFilter(FlightBookingModel booking)
        {
            return booking.FlightClass.Equals(_class);
        }
    }
}
