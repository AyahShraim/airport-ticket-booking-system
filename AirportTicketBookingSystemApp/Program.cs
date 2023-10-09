using AirportTicketBookingSystemApp;
using AirportTicketBookingSystemApp.Commands_UI;
using AirportTicketBookingSystemApp.Enums;
using AirportTicketBookingSystemApp.FlightManagement;
using AirportTicketBookingSystemApp.Interfaces;
using AirportTicketBookingSystemApp.PassengerManagement;
using AirportTicketBookingSystemApp.UI_s_Commands;

PrintWelcome();

List<Flight> systemFlights = new List<Flight>();
FlightRepository flightRepository = new FlightRepository();
List<FlightBookingModel> bookings = new();
Dictionary<PassengerMenuOptions, IPassengerMenuCommands> passengerMenuCommands = new();
Dictionary<ManagerMenuOptions, IManagerMenuCommands> ManagerMenuCommands = new();
PassengerAccountUI passengerAccountUI = new();
StartTickectBookingConsoleApp();

void Initilization()
{
    LoadSystemFlightsync(flightRepository);
    LoadSystemBookings();
    CommandsInitilization();
}
void PrintWelcome()
{
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine(@"
 __| |____________________________________________| |__
(__   ____________________________________________   __)
   | |                                            | |
   | |                 Welcome To                 | |
   | |       Airport Ticket Booking System        | |
   | |                   _                        | |
   | |                 -=\`\                      | |
   | |             |\ ____\_\__                   | |
   | |            -=\c`"""""""""""""" ""`)      
   | |              `~~~~~/ /~~`                  | |
   | |                -==/ /                      | |
   | |                  '-'                       | |   
 __| |____________________________________________| |__
(__   ____________________________________________   __)                                                                                                                                                                              
                           
");
    Console.ResetColor();
    Console.WriteLine("Press any key to start!");
    Console.ReadLine();
    Console.Clear();
}
void CommandsInitilization()
{
    passengerMenuCommands = new Dictionary<PassengerMenuOptions, IPassengerMenuCommands>
    {
          {PassengerMenuOptions.SearchFlight,new SearchFlightCommand(systemFlights)},
          {PassengerMenuOptions.BookFlight,new BookingFlightCommand(systemFlights,passengerAccountUI.CurrentPassenger)},
          {PassengerMenuOptions.ViewBookings,new ViewPersonalBookingsCommand(passengerAccountUI.CurrentPassenger,systemFlights)},
          {PassengerMenuOptions.CancelBooking,new CancelBookingCommand()}
    };

    ManagerMenuCommands = new Dictionary<ManagerMenuOptions, IManagerMenuCommands>
    {
         {ManagerMenuOptions.FilterBookings,new FilterBookingsCommand(bookings, systemFlights)},
         {ManagerMenuOptions.UploadFlights,new UploadFlightsCommand()},
         {ManagerMenuOptions.ViewFlightValidationDetails,new ViewFlightValidationDetailsCommand()}
    };
}
void StartTickectBookingConsoleApp()
{
    PrintMainMenuOptions();
    HandleMainMenuSelection();
}
void PrintMainMenuOptions()
{
    Console.WriteLine(@"
|-------------------------|
|Select an action to start|
|-------------------------|

");
    Console.WriteLine("1.Create new account");
    Console.WriteLine("2.Log in");
    Console.WriteLine("3.Manager Services");
    Console.WriteLine("0.Close Application");
}
void HandleMainMenuSelection()
{
    Console.WriteLine("Your Selection");
    string? userSelection = Console.ReadLine();
    int selection;
    if (int.TryParse(userSelection, out selection))
    {
        MainMenuOptions selected = (MainMenuOptions)selection;
        switch (selected)
        {
            case MainMenuOptions.Register:
                passengerAccountUI.RegisterPassenger();
                break;

            case MainMenuOptions.Login:
                bool valid = passengerAccountUI.PassengerLogIn();
                if (valid)
                {
                    Initilization();
                    StartPssengerServicesConsole(passengerAccountUI.CurrentPassenger);
                }
                break;

            case MainMenuOptions.ManagerServices:
                StartManagerServices();
                break;

            case MainMenuOptions.Exit:
                break;

            default:
                break;
        }
        StartTickectBookingConsoleApp();
    }
    else
    {
        Console.WriteLine("Enter a valid choice");
        StartTickectBookingConsoleApp();
    }
}
void LoadSystemFlightsync(FlightRepository flightRepository)
{
    flightRepository.LoadFlights(Utilities.SystemFlightsPath);
    systemFlights = flightRepository.SystemFlights;
}
void StartPssengerServicesConsole(Passenger currentPassenger)
{
    Console.WriteLine($"Welcome {currentPassenger.FirstName} {currentPassenger.LastName}");
    PrintPassengerServicesConsole();
}
void PrintPassengerServicesConsole()
{
    Console.WriteLine(@"

| What do you want to do? |
|-------------------------|

");
    Console.WriteLine("1.Search a flight");
    Console.WriteLine("2.Book flight");
    Console.WriteLine("3.View personal Bookings");
    Console.WriteLine("4.Edit Booking");
    Console.WriteLine("5.Cancel Booking");
    Console.WriteLine("0.Exit");
    HandlePassengerServicesSelection();
}
void HandlePassengerServicesSelection()
{
    Console.WriteLine("Your Selection");
    string? userSelection = Console.ReadLine();
    int selection;
    if (int.TryParse(userSelection, out selection))
    {
        PassengerMenuOptions selected = (PassengerMenuOptions)selection;
        try
        {
            passengerMenuCommands[selected].Execute();
        }
        catch
        { 
            Console.WriteLine("Not valid choice");
        }
        finally
        {
            PrintPassengerServicesConsole();
        }
    }
    else
    {
        Console.WriteLine("not valid choice");
        PrintPassengerServicesConsole();
    }
}
void StartManagerServices()
{
    Console.WriteLine("enter your secret key");
    string secretKey = Console.ReadLine() ?? string.Empty;
    if (!secretKey.Equals(Utilities.SecretKey))
    {
        Console.WriteLine("not valid secret key");
        return;
    }
    else
    {
        Initilization();
        PrintManagerMenuOptions();
    }
}
void PrintManagerMenuOptions()
{
    Console.WriteLine(@"
|-------------------------|
|Select an action to start|
|-------------------------|

");
    Console.WriteLine("1.Filter Bookings");
    Console.WriteLine("2.Upload new Files");
    Console.WriteLine("3.View flight validation details");
    Console.WriteLine("0.Close Application");
    HandleManagerMenuSelection();
}
void HandleManagerMenuSelection()
{
    Console.WriteLine("Your Selection");
    string? userSelection = Console.ReadLine();
    int selection;
    if (int.TryParse(userSelection, out selection))
    {
        ManagerMenuOptions selected = (ManagerMenuOptions)selection;
        try
        {
            ManagerMenuCommands[selected].Execute();
        }
        catch
        {
            Console.WriteLine("Not valid choice");
        }
        finally
        {
            PrintManagerMenuOptions();
        }
    }
    else
    {
        Console.WriteLine("not valid choice");
        PrintManagerMenuOptions();
    }
}

void LoadSystemBookings()
{
    BookingRepository bookingRepository = new();
    bookings = bookingRepository.LoadBookings(Utilities.bookingsFilePath);
}