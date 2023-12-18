using AirportTicketBookingSystemApp.FlightManagement;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace AirportTicketBookingSystemApp.Validators
{
    public static class ModelValidator
    {
        public static List<String> GenerateDynamicValidationDetails<T>()
        {
            PropertyInfo[] propertires = typeof(T).GetProperties();
            var validationDetails = new List<string>();
            foreach (PropertyInfo property in propertires)
            {
                validationDetails.Add($"\n{property.Name}\n");
                validationDetails.Add($"    -Type:{property.PropertyType.Name}");
                var validationAtrributes = property.GetCustomAttributes<ValidationAttribute>();
                foreach (var validationAttribute in validationAtrributes)
                {
                    validationDetails.Add($"    -Constraint: {GetValidationDetails(validationAttribute)}");
                }
            }
            return validationDetails;
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
            else if (validationAttribute is FutureDateRangeAttribute futureDateAttribute)
            {
                return $"Allowed range: today ({DateTime.Now.Date}) -> future";
            }
            else
            {
                return "no constraint!";
            }
        }
    }
}
