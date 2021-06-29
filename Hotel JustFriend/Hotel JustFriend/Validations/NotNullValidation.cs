using System.Globalization;
using System.Windows.Controls;

namespace Hotel_JustFriend.Validations
{
    public class NotNullValidation : ValidationRule
    {
        public string ErrorMessage { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            ValidationResult result = new ValidationResult(true, null);

            if (value == null)
                return new ValidationResult(false, ErrorMessage);
            if (string.IsNullOrEmpty(value.ToString()) || string.IsNullOrWhiteSpace(value.ToString()))
                return new ValidationResult(false, ErrorMessage);
            if (value.ToString() == "0")
                return new ValidationResult(false, ErrorMessage);

            return result;
        }
    }
}
