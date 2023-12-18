using AirportTicketBookingSystemApp.Enums;
using AirportTicketBookingSystemApp.FlightManagement;
using AirportTicketBookingSystemApp.Interfaces;


namespace AirportTicketBookingSystemApp.Services.SearchService.SearchFilter
{
    public class BookingClassFilter : ISearchCriteria<FlightBookingModel>
    {
        private FlightClassType _class;
        public BookingClassFilter(FlightClassType bookedClass)
        {
            _class = bookedClass;
        }
        public bool ApplyFilter(FlightBookingModel booking)
        {
            return booking.FlightClass.Equals(_class);
        }
    }
}
