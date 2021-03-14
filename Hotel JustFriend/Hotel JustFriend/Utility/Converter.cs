using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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
            return money.ToString("#,0", new CultureInfo("vi-VN"));
        }

        public string ConvertDecimalToCurrency(string moneyString)
        {
            decimal money = decimal.Parse((moneyString as string), NumberStyles.Currency, CultureInfo.GetCultureInfo("vi-VN"));
            return money.ToString("#,0", new CultureInfo("vi-VN"));
        }

        public decimal ConvertCurrencyToDecimal(string moneyString)
        {
            return decimal.Parse((moneyString as string), NumberStyles.Currency, CultureInfo.GetCultureInfo("vi-VN"));
        }
    }
}
