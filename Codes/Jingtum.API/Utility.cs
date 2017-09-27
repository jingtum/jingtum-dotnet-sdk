using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Jingtum.API
{
    class Utility
    {
        public static bool IsValidCurrency(String currency)
        {
            // Currently only check the length of the currency
            if (currency != string.Empty && (currency.Length == 3 || currency.Length == 40))
            {
                return true;
            }
            return false;
        }

        public static bool IsValidAddress(String address)
        {
            return true;

            try
            {
                //to be implemented.
                Core.Config.GetB58IdentiferCodecs().DecodeAddress(address);
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public static byte[] CopyRange(byte[] buffer, int start, int end)
        {
            byte[] result = new byte[end - start];
            int j = start;

            for (int i = 0; i < end - start; i++)
            {
                result[i] = buffer[j++];                
            }

            return result;
        }

        
    }
}
