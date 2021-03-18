using Hotel_JustFriend.Utility;
using System;
using System.Globalization;
using System.Windows.Controls;

namespace Hotel_JustFriend.Validations
{
    class MoneyValidation : ValidationRule
    {
        public decimal Divisor { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            decimal money = 0;

            try
            {
                if ((value as string).Length > 0)
                    money = Converter.Instance.ConvertCurrencyToDecimal(value as string);
            }
            catch (Exception)
            {
                return new ValidationResult(false, "Chỉ được nhập số!");
            }

            if (money % Divisor != 0)
            {
                string divisorString = Converter.Instance.ConvertDecimalToCurrency(Divisor);
                return new ValidationResult(false, string.Format("Phải là bội số của {0} VNĐ!", divisorString));
            }
            return ValidationResult.ValidResult;
        }
    }
}