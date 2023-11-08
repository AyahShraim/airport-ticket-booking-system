
using AirportTicketBookingSystemApp.Enums;

namespace AirportTicketBookingSystemApp.FlightManagement
{
    public class FlightServices
    {
        public double FlightPrice(Flight flight, FlightClassType flightClassType)
        {
            switch (flightClassType)
            {
                case FlightClassType.Economy:
                    return flight.EconomiyPrice;
                   
                case FlightClassType.Business:
                    return flight.BusinessPrice;
                   
                case FlightClassType.FirstClass:
                    return flight.FirstClassPrice;

                default:
                    return double.NaN;     
            }
        }
    
        public bool FlightClassSeatAvailable(Flight flight, FlightClassType flightClassType)
        {
            switch (flightClassType)
            {
                case FlightClassType.Economy:
                    return flight.EconomiyAvailable > 0;

                case FlightClassType.Business:
                    return flight.BusinessAvailable > 0;

                case FlightClassType.FirstClass:
                    return flight.FirstClassAvailable > 0;

                default:
                    return false;
            }
        } 
    }
}
