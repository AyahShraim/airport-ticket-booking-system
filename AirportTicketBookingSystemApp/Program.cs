using AirportTicketBookingSystemApp.Enums;

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
        switch (selected)
        {
            case MainMenuOptions.Register:
                break;

            case MainMenuOptions.Login:
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