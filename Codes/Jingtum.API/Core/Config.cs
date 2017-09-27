using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jingtum.API.Core
{
    class Config
    {
        public const String DEFAULT_ALPHABET = "jpshnaf39wBUDNEGHJKLM4PQRST7VWXYZ2bcdeCg65rkm8oFqi1tuvAxyz";

        private static B58IdentiferCodecs m_B58IdentiferCodecs;

        public static void SetAlphabet(String alphabet)
        {
            B58 b58 = new B58(alphabet);
            m_B58IdentiferCodecs = new B58IdentiferCodecs(b58);
        }

        public static B58IdentiferCodecs GetB58IdentiferCodecs() 
        {
            SetAlphabet(DEFAULT_ALPHABET);
            return m_B58IdentiferCodecs;
        }

        /***
         * We set up all the defaults here
         */
        //static {
        //    setAlphabet(DEFAULT_ALPHABET);
        //}
    }
}
