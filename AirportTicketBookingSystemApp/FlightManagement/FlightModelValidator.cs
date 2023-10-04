using System.ComponentModel.DataAnnotations;
using System.Reflection;


namespace AirportTicketBookingSystemApp.FlightManagement
{
    public static class  FlightModelValidator
    {
        public static void GenerateDynamicValidationDetails<T>(){

            PropertyInfo[] propertires = typeof(T).GetProperties(); 
            foreach(PropertyInfo prop in propertires)
            {
                Console.WriteLine($"****{prop.Name}****");


                var validationAtrributes = prop.GetCustomAttributes<ValidationAttribute>();
                foreach (var validationAttribute in validationAtrributes)
                {
                    Console.WriteLine($" - Type: {validationAttribute.GetType().Name}");
                    Console.WriteLine($" - Constraint: {GetValidationDetails(validationAttribute)}");
                }
            }
        }
        private static string GetValidationDetails(ValidationAttribute validationAttribute)
        {
            if (validationAttribute is RequiredAttribute)
            {
                return "Required";
            }
            else if (validationAttribute is RangeAttribute rangeAttribute)
            {
                return $"Range ({rangeAttribute.Minimum} - {rangeAttribute.Maximum})";
            }
            else
            {
                return "Custom";
            }
        }
    }
}
