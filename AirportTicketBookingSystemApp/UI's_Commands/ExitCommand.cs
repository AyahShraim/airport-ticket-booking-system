using AirportTicketBookingSystemApp.Interfaces;

namespace AirportTicketBookingSystemApp.UI_s_Commands
{
    public class ExitCommand : IMenuCommands
    {
        public void Execute()
        {
            Console.WriteLine("good bye!");
            Environment.Exit(0);
        }
    }
}
