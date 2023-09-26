using System.Net.Mail;

namespace AirportTicketBookingTry.PassengerManagement
{
    internal class PassengerValidation
    {
        public static bool IsValidName(string name)
        {
            if (!string.IsNullOrEmpty(name) && name.Length > 1) 
                return true;

            return false;
        }
        public static bool IsValidEmail(string email)
        {
            var valid = true;
            try
            {
                var emailAddress = new MailAddress(email);
            }
            catch
            {
                valid = false;
            }
            return valid;

        }
        public static bool IsValidPassword(string password )
        {
            if(!string.IsNullOrEmpty(password)&& password.Length>5)
            {
                return true;
            }
            return false;
        }
    }
}
