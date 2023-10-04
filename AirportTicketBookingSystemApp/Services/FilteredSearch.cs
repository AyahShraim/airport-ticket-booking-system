using AirportTicketBookingSystemApp.Enums;
using AirportTicketBookingSystemApp.FlightManagement;
using AirportTicketBookingSystemApp.Interfaces;
using AirportTicketBookingSystemApp.Services.SearchService.SearchFilter;
using AirportTicketBookingTry.Services.SearchService.SearchFilter;

namespace AirportTicketBookingSystemApp.Services.SearchService
{
    public class FilteredSearch
    {
        private static List<ISearchCriteria<Flight>> FlightSearchCriteria(Dictionary<string, object> parameters)
        {
            List<ISearchCriteria<Flight>> searchCriteria = new List<ISearchCriteria<Flight>>();

            bool IsKey(string key)
            {
                return parameters.ContainsKey(key);
            }

            if (IsKey("DepartureCountry"))
            {
                searchCriteria.Add(new DepartureCountryFilter((string)parameters["DepartureCountry"]));
            }

            if (IsKey("ArrivalCountry"))
            {
                searchCriteria.Add(new ArrivalCountryFilter((string)parameters["ArrivalCountry"]));
            }

            if (IsKey("DepartureAirport"))
            {
                searchCriteria.Add(new DepartureAirportFilter((string)parameters["DepartureAirport"]));
            }

            if (IsKey("ArrivalAirport"))
            {
                searchCriteria.Add(new ArrivalAirportFilter((string)parameters["ArrivalAirport"]));
            }

            if (IsKey("DepartureDate"))
            {
                searchCriteria.Add(new DepartureDateFilter((DateTime)parameters["DepartureDate"]));
            }
            if (IsKey("Class"))
            {
                searchCriteria.Add(new ClassFilter((FlightClassType)parameters["Class"]));
            }

            if (IsKey("MaxPrice"))
            {
                searchCriteria.Add(new PriceFilter((double)parameters["Price"], (FlightClassType)parameters["Class"]));
            }
            
            return searchCriteria;
        }

        private static List<ISearchCriteria<FlightBookingModel>> BookingSearchCriteria(Dictionary<string, object> parameters)
        {
            List<ISearchCriteria<FlightBookingModel>> searchCriteria = new List<ISearchCriteria<FlightBookingModel>>();

            bool IsKey(string key)
            {
                return parameters.ContainsKey(key);
            }

            if (IsKey("Email"))
            {
                searchCriteria.Add(new BookingEmailFilter((string)parameters["Email"]));
            }

            if (IsKey("FlightNumber"))
            {
                searchCriteria.Add(new BookingFlightnoFilter((int)parameters["FlightNumber"]));
            }

            if (IsKey("BookingClass"))
            {
                searchCriteria.Add(new BookingClassFilter((string)parameters["BookingClass"]));
            }
            if(IsKey("BookingPrice"))
            {
                searchCriteria.Add(new BookingClassPrice((double)parameters["BookingPrice"]));
            }
            return searchCriteria;
        }


        public List<Flight> SearchFlight(List<Flight> items, Dictionary<string, object> parameters)
        {
            List<ISearchCriteria<Flight>> searchCriterias = FlightSearchCriteria(parameters);
            List<Flight> filteredItems = new List<Flight>(items);
            filteredItems = items.Where(item => searchCriterias.All(criteria => criteria.ApplyFilter(item))).ToList();
            return filteredItems;
        }
        public List<FlightBookingModel> SearchBookings(List<FlightBookingModel> bookings,List<Flight> filteredFlights, Dictionary<string, object> parameters)
        {
           
            List<ISearchCriteria<FlightBookingModel>> bookingSearchCriteria = BookingSearchCriteria(parameters);
            List<FlightBookingModel> filteredItems = new List<FlightBookingModel>(bookings);
            if(filteredFlights.Count>0)
            bookings = bookings.Where(item =>
            {
                return filteredFlights.Any(flight => flight.Number == item.FlightNumber);
            }).ToList(); 
            filteredItems = bookings.Where(item => bookingSearchCriteria.All(criteria => criteria.ApplyFilter(item))).ToList();
            return filteredItems;
        }
    }
}
