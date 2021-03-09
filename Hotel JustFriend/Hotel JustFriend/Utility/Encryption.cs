using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_JustFriend.Utility
{
    class Encryption
    {
        private byte[] CalculateSHA256(string str)
        {
            SHA256 sha256 = SHA256Managed.Create();
            byte[] hashValue;
            UTF8Encoding objUtf8 = new UTF8Encoding();
            hashValue = sha256.ComputeHash(objUtf8.GetBytes(str));

            return hashValue;
        }
    }
}
