using System;
using System.Security.Cryptography;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Security;

namespace Jingtum.API.Core
{
    internal class Utils
    {
        public static String BigHex(BigInteger bn)
        {
            return B16.ToStringTrimmed(bn.ToByteArray());
        }

        public static BigInteger UBigInt(byte[] bytes)
        {
            return new BigInteger(1, bytes);
        }
    }

    internal class HashUtils
    {
        public static byte[] DoubleHash(byte[] input)
        {
            return DoubleHash(input, 0, input.Length);
        }

        public static byte[] DoubleHash(byte[] input, int offset, int length)
        {
            SHA256 sha256 = new SHA256CryptoServiceProvider();
            byte[] code = sha256.ComputeHash(input, offset, length);
            code = sha256.ComputeHash(code);
            return code;
        }

        /// <summary>
        /// Calculates the SHA-256 hash of the given byte range, and then hashes the resulting hash again.
        /// That is standard procedure in Bitcoin. The resulting hash is in big endian form. 
        /// </summary>
        /// <param name="input">Input byte array to be hashed.</param>
        /// <returns>Hash of the input byte in Big Endian form.</returns>
        public static byte[] SHA256_RIPEMD160(byte[] input)
        {
            try
            {
                byte[] sha256 = new SHA256CryptoServiceProvider().ComputeHash(input);
                var digest = new RipeMD160Digest();
                digest.BlockUpdate(sha256, 0, sha256.Length);
                byte[] result = new byte[20];
                digest.DoFinal(result, 0);
                return result;
            }
            catch (GeneralSecurityException e)
            {
                throw e;  // Cannot happen.
            }
        }
    }  
}
