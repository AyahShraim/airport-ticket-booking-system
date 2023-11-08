using AirportTicketBookingSystemApp.FlightManagement;
using AirportTicketBookingSystemApp.Services.SearchService;
using AutoFixture;
using AutoFixture.AutoMoq;

namespace AirportTicketBookingSystemApp.Tests
{
    public class FilteredSearchShould
    {
        private readonly IFixture _fixture;
        private readonly FilteredSearch _sut;

        public FilteredSearchShould()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
            _sut = _fixture.Create<FilteredSearch>();
        }
        [Fact]
        public void FilterFlights_ShouldFilterFlightsByParameters()
        {   
            var flights = _fixture.CreateMany<Flight>().ToList();
            var flight = _fixture.Create<Flight>();
            flights.Add(flight);
            var parameters = new Dictionary<string, object>
            {
                {"DepartureCountry",flight.DepartureCountry }
            };

            var filteredFlights = _sut.SearchFlight(flights, parameters);
            Assert.Contains(flight, filteredFlights);
            foreach (var nonMatchingFlight in flights.Where(f => f != flight))
            {
                Assert.DoesNotContain(nonMatchingFlight, filteredFlights);
            }
        }

        [Fact]
        public void FilterBookings_ShouldFilterBookingsByParameters()
        { 
            var bookings = _fixture.CreateMany<FlightBookingModel>().ToList();
            var filteredFlights = _fixture.CreateMany<Flight>().ToList();
            var booking = _fixture.Create<FlightBookingModel>();
            bookings.Add(booking);
            var parameters = new Dictionary<string, object>
            {
                {"Email",booking.Email }
            };

            var filteredBookings = _sut.SearchBookings(bookings, filteredFlights, parameters);
            Assert.Contains(booking, bookings);
            foreach (var nonMatchingBooking in bookings.Where(book => book != booking))
            {
                Assert.DoesNotContain(nonMatchingBooking, filteredBookings);
            }
        }
    }
}