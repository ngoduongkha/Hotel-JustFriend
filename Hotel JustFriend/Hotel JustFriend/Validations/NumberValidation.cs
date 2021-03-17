using System;
using System.Globalization;
using System.Windows.Controls;

namespace Hotel_JustFriend.Validations
{
    class NumberValidation : ValidationRule
    {
        public int Min { get; set; }
        public int Max { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int number = 0;

            try
            {
                if ((value as string).Length > 0)
                    number = Int32.Parse(value as string);
            }
            catch (Exception)
            {
                return new ValidationResult(false, "Chỉ được nhập số!");
            }

            if ((number < Min) || (number > Max))
            {
                return new ValidationResult(false,
                  $"Vui lòng nhập trong khoảng {Min}-{Max}!");
            }
            return ValidationResult.ValidResult;
        }
    }
}
