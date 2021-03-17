using System.Globalization;

namespace Hotel_JustFriend.Utility
{
    class Converter
    {
        private static Converter _Instance;

        internal static Converter Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new Converter();
                return _Instance;
            }
            set { _Instance = value; }
        }

        public string ConvertDecimalToCurrency(decimal money)
        {
            return money.ToString("#,0", CultureInfo.GetCultureInfo("en-US"));
        }

        public string ConvertDecimalToCurrency(string moneyString)
        {
            decimal money = decimal.Parse((moneyString as string), NumberStyles.Currency, CultureInfo.GetCultureInfo("en-US"));
            return money.ToString("#,0", CultureInfo.GetCultureInfo("en-US"));
        }

        public decimal ConvertCurrencyToDecimal(string moneyString)
        {
            return decimal.Parse((moneyString as string), NumberStyles.Currency, CultureInfo.GetCultureInfo("en-US"));
        }
    }
}
