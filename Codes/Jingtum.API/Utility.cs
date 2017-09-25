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

        public static bool isValidAddress(String address)
        {
            try
            {
                //to be implemented.
                //this.decodeAddress(address);
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        //public byte[] encodeToBytesChecked(byte[] input, int version)
        //{
        //    byte[] buffer = new byte[input.Length + 1];
        //    byte[] buffer_1 = new byte[1];
        //    buffer_1[0] = (byte)version;
        //    //System.arraycopy(input, 0, buffer, 1, input.length);
        //    buffer = buffer_1.Concat(input);
        //    buffer
        //    byte[] checkSum = copyOfRange(HashUtils.doubleDigest(buffer), 0, 4);
        //    byte[] output = new byte[buffer.length + checkSum.length];
        //    System.arraycopy(buffer, 0, output, 0, buffer.length);
        //    System.arraycopy(checkSum, 0, output, buffer.length, checkSum.length);
        //    return encodeToBytes(output);
        //}
    }
}
