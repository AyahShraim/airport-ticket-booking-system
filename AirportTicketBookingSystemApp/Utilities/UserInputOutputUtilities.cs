using AirportTicketBookingSystemApp.Enums;

namespace AirportTicketBookingSystemApp.Utilities
{
    public static class UserInputOutputUtilities
    {
        public static bool HandleStringInput(string value)
        {
            return IsValidString(value);
        }
        public static double? HandleDoubleInput(string value)
        {
            if (double.TryParse(value, out double doubleValue))
            {
                return doubleValue;
            }
            return null;
        }
        public static int? HandleIntInput(string value)
        {
            if (int.TryParse(value, out int intValue))
            {
                return intValue;
            }
            return null;
        }
        public static DateTime? HandleDateInput(string value)
        {

            if (DateTime.TryParse(value, out DateTime dateValue))
            {
                return dateValue.Date;
            }
            return null;
        }
        public static FlightClassType? HandleClassInput()
        {
            PrintClassesMenu();
            string value = Console.ReadLine() ?? string.Empty;
            int? selectedClass = HandleIntInput(value);
            if (selectedClass != null)
            {
                return (FlightClassType)selectedClass;
            }
            return null;
        }
        private static bool IsValidString(string value)
        {
            return !string.IsNullOrEmpty(value);
        }
        public static void PrintClassesMenu()
        {
            Console.WriteLine("Select a class type:");
            Console.WriteLine("1.Economy");
            Console.WriteLine("2.Business");
            Console.WriteLine("3.First Class");
        }
    }
}
