using AirportTicketBookingSystemApp.Interfaces;

namespace AirportTicketBookingSystemApp.Commands_UI
{
    public class FilterBookingsCommand : IManagerMenuCommands
    {
        public void Execute()
        {
            Console.WriteLine("filter");
        }
    }
}
