using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Jingtum.API.Core
{
    class B58IdentiferCodecs
    {
        public const int VER_ACCOUNT_ID        = 0;
        public const int VER_FAMILY_SEED       = 33;
        
        B58 m_B58;

        public B58IdentiferCodecs(B58 base58encoder)
        {
            this.m_B58 = base58encoder;
        }

        public byte[] Decode(String d, int version)
        {
            return m_B58.DecodeChecked(d, version);
        }

        //public String encode(byte[] d, int version)
        //{
        //    return m_B58.EncodeToStringChecked(d, version);
        //}

        public byte[] DecodeFamilySeed(String master_seed)
        {
            byte[] bytes = m_B58.DecodeChecked(master_seed, VER_FAMILY_SEED);
            return bytes;
        }

        //public String encodeAddress(byte[] bytes)
        //{
        //    return encode(bytes, VER_ACCOUNT_ID);
        //}

        public byte[] DecodeAddress(String address)
        {
            return Decode(address, VER_ACCOUNT_ID);
        }

        //public String encodeFamilySeed(byte[] bytes)
        //{
        //    return encode(bytes, VER_FAMILY_SEED);
        //}
    }

    class B58
    {
        private int[] m_Indexes;
        private char[] m_Alphabet;

        public B58(String alphabet)
        {
            SetAlphabet(alphabet);
            BuildIndexes();
        }

        private void SetAlphabet(String alphabet)
        {
            m_Alphabet = alphabet.ToArray();
        }

        private void BuildIndexes()
        {
            m_Indexes = new int[128];

            for (int i = 0; i < m_Indexes.Length; i++)
            {
                m_Indexes[i] = -1;
            }
            for (int i = 0; i < m_Alphabet.Length; i++)
            {
                m_Indexes[m_Alphabet[i]] = i;
            }
        }

        #region decode by byte
        public byte[] Decode(string input)
        {
            if (input.Length == 0)
            {
                return new byte[0];
            }

            byte[] input58 = new byte[input.Length];
            // Transform the String to a base58 byte sequence
            for (int i = 0; i < input.Length; ++i)
            {
                char c = input[i];

                int digit58 = -1;
                if (c >= 0 && c < 128)
                {
                    digit58 = m_Indexes[c];
                }

                if (digit58 < 0)
                {
                    throw new EncodingFormatException("Illegal character " + c + " at " + i);
                }
                input58[i] = (byte)digit58;
            }

            // Count leading zeroes
            int zeroCount = 0;
            while (zeroCount < input58.Length && input58[zeroCount] == 0)
            {
                ++zeroCount;
            }
            // The encoding
            byte[] temp = new byte[input.Length];
            int j = temp.Length;

            int startAt = zeroCount;
            while (startAt < input58.Length)
            {
                byte mod = Divmod256(input58, startAt);

                if (input58[startAt] == 0)
                {
                    ++startAt;
                }

                temp[--j] = mod;
            }
            // Do no add extra leading zeroes, move j to first non null byte.
            while (j < temp.Length && temp[j] == 0)
            {
                ++j;
            }

            return Utility.CopyRange(temp, j - zeroCount, temp.Length);
        }

        public byte[] DecodeChecked(string input, int version)
        {
            byte[] buffer = Decode(input);

            if (buffer.Length < 4)
            {
                throw new EncodingFormatException("Input too short");
            }

            byte actualVersion = buffer[0];
            if (actualVersion != version)
            {
                throw new EncodingFormatException("Bro, version is wrong yo");
            }

            byte[] toHash = Utility.CopyRange(buffer, 0, buffer.Length - 4);
            byte[] hashed = Utility.CopyRange(HashUtils.DoubleHash(toHash), 0, 4);
            byte[] checksum = Utility.CopyRange(buffer, buffer.Length - 4, buffer.Length);

            //if (!hashed.Equals(checksum))
            if(!Utility.IfByteArrayEquals(hashed, checksum))
            {
                throw new EncodingFormatException("Checksum does not validate");
            }

            return Utility.CopyRange(buffer, 1, buffer.Length - 4);
        }

        //
        // number -> number / 256, returns number % 256
        //
        private byte Divmod256(byte[] number58, int startAt)
        {
            int remainder = 0;
            for (int i = startAt; i < number58.Length; i++)
            {
                int digit58 = (int)number58[i] & 0xFF;
                int temp = remainder * 58 + digit58;

                //Console.WriteLine("i:" + i + "   digit58:" + digit58);

                number58[i] = (byte)(temp / 256);

                remainder = temp % 256;
            }

            //-128~127
            //remainder = (remainder > 127) ? (remainder - 256) : remainder;
            return (byte)remainder;
        }
        #endregion
    }

    class HashUtils
    {
        public static byte[] DoubleHash(byte[] input)
        {
            return HashUtils.DoubleHash(input, 0, input.Length);
        }

        public static byte[] DoubleHash(byte[] input, int offset, int length)
        {
            SHA256 sha256 = new SHA256CryptoServiceProvider();
            byte[] code = sha256.ComputeHash(input, offset, length);
            code = sha256.ComputeHash(code);
            return code;
        }
    }
}
