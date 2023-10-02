using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingTry
{
    public class Utilities
    {
        private const string _RootDiectory = @"C:\Users\DELL\source\repos\AirportTicketBookingTry\Data\";
        private const string _systemFlightFile = "system_flights.csv";

        public static string SystemFlightsPath => Path.Combine(_RootDiectory, _systemFlightFile);
       
    }
}
