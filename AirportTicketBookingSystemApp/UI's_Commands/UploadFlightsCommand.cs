using AirportTicketBookingSystemApp.Interfaces;

namespace AirportTicketBookingSystemApp.Commands_UI
{
    public class UploadFlightsCommand : IManagerMenuCommands
    {
        public void Execute()
        {
            Console.WriteLine("upload");
        }
    }
}
