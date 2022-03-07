
using System.Text.RegularExpressions;

namespace final_pharmacy.Services
{
    public class Validators
    {
        private string EmailPattern { get; } = @"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$";
        private string ContactPattern { get;  } = @"^(\+\d{1,3}[- ]?)?\d{10}$";
        private string PasswordPattern { get; } = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$"; //Minimum eight characters, at least one letter, one number and one special character
        private string AlphabetOnlyPattern { get; } = @"^[A-Za-z ]+$";
        private string NumberOnlyPattern { get; } = @"^[0-9.]+$";
        public bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, EmailPattern);
        }
        public bool IsValidContact(string phone)
        {
            return Regex.IsMatch(phone, ContactPattern);
        }
        public bool IsValidPassword(string password)
        {
            return Regex.IsMatch(password, PasswordPattern);
        }
        public bool IsAlphabetOnly(string word)
        {
            return Regex.IsMatch(word, AlphabetOnlyPattern);
        }
        public bool IsNumberOnly(string word)
        {
            return Regex.IsMatch(word, NumberOnlyPattern);
        }
    }
}
