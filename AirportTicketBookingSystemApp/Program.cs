﻿using AirportTicketBookingSystemApp;
using AirportTicketBookingSystemApp.Commands_UI;
using AirportTicketBookingSystemApp.Enums;
using AirportTicketBookingSystemApp.FlightManagement;
using AirportTicketBookingSystemApp.Interfaces;
using AirportTicketBookingSystemApp.PassengerManagement;
using AirportTicketBookingSystemApp.UI_s_Commands;

PrintWelcome();

List<Flight> systemFlights = new List<Flight>();
FlightRepository flightRepository = new FlightRepository();
Dictionary<PassengerMenuOptions, IPassengerMenuCommands> passengerMenuCommands = new();
PassengerAccountUI passengerAccountUI = new();
StartTickectBookingConsoleApp();
void Initilization()
{
    LoadSystemFlightsync(flightRepository);
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
          { PassengerMenuOptions.SearchFlight,new SearchFlightCommand(systemFlights) },
          { PassengerMenuOptions.BookFlight,new BookingFlightCommand(systemFlights,passengerAccountUI.CurrentPassenger) }
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
                //    Console.WriteLine(PassengerAccountUI._currentPassenger.FirstName);      

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
    string directory = @"C:\Users\DELL\source\repos\AirportTicketBookingTry\Data\";
    string flightsFileName = "system_flights.csv";
    string path = $"{directory}{flightsFileName}";
    flightRepository.LoadFlights(path);
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
    Console.WriteLine("3.Manage Bookings");
    Console.WriteLine("4.Log out");
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
        passengerMenuCommands[selected].Execute();
        PrintPassengerServicesConsole();
    }
    else
    {
        Console.WriteLine("not valid choice");

    }
}