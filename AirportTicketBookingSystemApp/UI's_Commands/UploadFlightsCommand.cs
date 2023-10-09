using AirportTicketBookingSystemApp.FlightManagement;
using AirportTicketBookingSystemApp.Interfaces;

namespace AirportTicketBookingSystemApp.Commands_UI
{
    public class UploadFlightsCommand : IManagerMenuCommands
    {
        private FlightRepository _flightRepository;
        public UploadFlightsCommand()
        {
            _flightRepository = new();
        }
        public void Execute()
        {
            Console.WriteLine("upload");    
            try
            {
                string path = Console.ReadLine() ?? string.Empty;
                var result = _flightRepository.BatchFileUpload(path);
                Console.WriteLine("************************");
                Console.WriteLine(result.Message);
                Console.WriteLine("************************");
                if (!result.IsSuccess)
                {
                        Console.WriteLine(result.Data.ToString());              
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
