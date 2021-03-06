using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Jingtum.API.Core.Crypto.Ecdsa;

namespace Jingtum.API
{
    public class Utility
    {
        #region consts
        public const string CURRENCY_SWT = "SWT";
        public const string CURRENCY_CNY = "CNY";
        public const string CURRENCY_USD = "USD";
        public const string CURRENCY_ERU = "ERU";
        public const string CURRENCY_JPY = "JPY";
        public const string CURRENCY_SPC = "SPC";
        #endregion

        public static bool IsValidCurrency(string currency)
        {
            // Currently only check the length of the currency
            if (currency != string.Empty && (currency.Length == 3 || currency.Length == 40) && currency.ToUpper() == currency)
            {
                return true;
            }
            return false;
        }

        public static string FormatCurrency(string currency)
        {
            return currency.ToUpper();
        }
        
        /// <summary>
        /// Validate the address and secret pair.
        /// </summary>
        /// <param name="address"></param>
        /// <param name="secret"></param>
        /// <returns>true if the address and secret is valid.</returns>
        public static bool ValidateKeyPair(String address, String secret)
        {
            String myAddress = null;
            try
            {
                myAddress = Seed.ComputeAddress(secret); // compute address from secret
            }
            catch (Exception)
            {
                return false;
            }

            if (!string.IsNullOrEmpty(address) && address.Equals(myAddress))
            {
                return true;
            }
            return false;
        }

        public static bool IsValidSecret(string secret)
        {
            if(secret == null || secret == string.Empty)
            {
                return false;
            }

            try
            {
                var address = Seed.ComputeAddress(secret);
                return IsValidAddress(address);
            }
            catch
            {
                return false;
            }
        }

        public static bool IsValidAddress(string address)
        {
            try
            {
                Core.Config.GetB58IdentiferCodecs().DecodeAddress(address);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public static T[] CopyRange<T>(T[] buffer, int start, int end)
        {
            T[] result = new T[end - start];
            int j = start;

            for (int i = 0; i < end - start; i++)
            {
                result[i] = buffer[j++];                
            }

            return result;
        }

        public static bool IfByteArrayEquals(byte[] a, byte[] b)
        {
            if(a == null || b == null)
            {
                return false;
            }

            if(a.Length != b.Length)
            {
                return false;
            }

            for (int i = 0; i < a.Length; i++)
            {
                if(a[i] != b[i])
                {
                    return false;
                }
            }

            return true;
        }

        public static List<string> ToStringList<T>(T obj)
        {
            PropertyInfo[] properties = obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            List<string> lines = new List<string>();

            foreach (PropertyInfo property in properties)
            {
                string line = property.Name + ": " + property.GetValue(obj);
                lines.Add(line);
            }

            return lines;
        }

        public static DateTime ConvertUnixTime2DateTime(int unixtime)
        {
            return (TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1))).AddSeconds(unixtime);
        }
    }
}
