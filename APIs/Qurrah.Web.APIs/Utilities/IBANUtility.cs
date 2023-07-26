using System.Numerics;
using System.Text.RegularExpressions;

namespace Qurrah.Web.APIs.Utilities
{
    public static class IBANUtility
    {
        public static bool IsValidIban(string iban)
        {
            if (!Regex.IsMatch(iban, "^([0-9A-Z]){24}$"))
                return false;
            else if (!iban.StartsWith("SA"))
                return false;
            else if (!Constants.IBAN.BankCodes.Contains(iban.Substring(4, 2)))
                return false;
            //Move the four initial characters to the end of the string 
            iban = iban.Substring(4) + iban.Substring(0, 4);
            //Replace each letter in the string with two digits, thereby expanding the string, where A = 10, B = 11, ..., Z = 35 
            iban = iban.Select(c => char.IsNumber(c) ? c.ToString() : ((int)c - 55).ToString()).Aggregate((s, a) => s += a);
            //Interpret the string as a decimal integer and compute the remainder of that number on division by 97 
            BigInteger value;
            if (BigInteger.TryParse(iban, out value))
                //If the remainder is 1, the check digit test is passed and the IBAN might be valid. 
                return value % 97 == 1;
            else
                return false;
        }
    }
}