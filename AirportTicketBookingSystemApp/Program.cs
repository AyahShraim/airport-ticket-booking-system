using AirportTicketBookingSystemApp.Enums;
using AirportTicketBookingSystemApp.UI_s_Commands;

PrintWelcome();
StartTickectBookingConsoleApp();


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
    Console.WriteLine("3.Mnager Services");
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
        PassengerAccountUI passengerAccountUI = new();
        switch (selected)
        {
            case MainMenuOptions.Register:

                passengerAccountUI.RegisterPassenger();
                break;

            case MainMenuOptions.Login:
                bool valid = passengerAccountUI.PassengerLogIn();
                if (valid) StartPssengerServicesConsole();
                break;

            case MainMenuOptions.ManagerServices:
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
void StartPssengerServicesConsole()
{
    Console.WriteLine($"Welcome {PassengerAccountUI._currentPassenger.FirstName}");
    PrintPassengerServicesConsole();
}
void PrintPassengerServicesConsole()
{
    Console.WriteLine(@"
|-------------------------|
|Select an action to start|
|-------------------------|

");
    Console.WriteLine("1.View All flights");
    Console.WriteLine("2.Search a flight");
    Console.WriteLine("3.Book flight");
    Console.WriteLine("4.Manage BooKings");
    Console.WriteLine("5.Log out");
    Console.WriteLine("0.Exit");
}