using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jingtum.API
{
    /// <summary>
    /// Balance class.
    /// </summary>
    public class Balance : JingtumObject
    {
        #region fields
        private double m_Value;
        private string m_Currency;
        private string m_Issuer;
        private string m_Counterparty;
        private double m_Freezed;
        #endregion

        #region properties
        public double Value
        {
            get
            {
                return m_Value;
            }

            set
            {
                m_Value = value;
            }
        }

        public string Currency
        {
            get
            {
                return m_Currency;
            }

            set
            {
                m_Currency = value;
            }
        }

        public string Issuer
        {
            get
            {
                return m_Issuer;
            }

            set
            {
                m_Issuer = value;
            }
        }

        public string Counterparty
        {
            get
            {
                return m_Counterparty;
            }

            set
            {
                m_Counterparty = value;
            }
        }

        public double Freezed
        {
            get
            {
                return m_Freezed;
            }

            set
            {
                m_Freezed = value;
            }
        }
        #endregion
    }

    public class Order : JingtumObject
    {
        #region fields
        private string m_Account;
        private string m_Pair;
        private string m_Type;
        private double m_Amount;
        private double m_Price;
        private long m_Sequence;
        #endregion

        #region properties
        public double Amount
        {
            get
            {
                return m_Amount;
            }

            set
            {
                m_Amount = value;
            }
        }

        public string Account
        {
            get
            {
                return m_Account;
            }

            set
            {
                m_Account = value;
            }
        }

        public string Type
        {
            get
            {
                return m_Type;
            }

            set
            {
                m_Type = value;
            }
        }

        public string Pair
        {
            get
            {
                return m_Pair;
            }

            set
            {
                m_Pair = value;
            }
        }

        public double Price
        {
            get
            {
                return m_Price;
            }

            set
            {
                m_Price = value;
            }
        }

        public long Sequence
        {
            get
            {
                return m_Sequence;
            }

            set
            {
                m_Sequence = value;
            }
        }
        #endregion
    }

    public class Effects : JingtumObject
    {
        #region fields
        private string m_Effect;
        private CounterParty m_CounterParty;
        private Amount m_Paid;
        private Amount m_Got;
        private Amount m_Pays;
        private Amount m_Gets;
        private string m_Type;

        private double m_Price;
        private string m_Pair;
        private object m_Amount;
        #endregion

        #region properties
        #region strings
        public string Effect
        {
            get
            {
                return m_Effect;
            }

            set
            {
                m_Effect = value;
            }
        }

        public string Type
        {
            get
            {
                return m_Type;
            }

            set
            {
                m_Type = value;
            }
        }

        public string Pair
        {
            get
            {
                return m_Pair;
            }

            set
            {
                m_Pair = value;
            }
        }
        #endregion

        #region amounts
        public Amount Paid
        {
            get
            {
                return m_Paid;
            }

            set
            {
                m_Paid = value;
            }
        }

        public Amount Got
        {
            get
            {
                return m_Got;
            }

            set
            {
                m_Got = value;
            }
        }

        public Amount Pays
        {
            get
            {
                return m_Pays;
            }

            set
            {
                m_Pays = value;
            }
        }

        public Amount Gets
        {
            get
            {
                return m_Gets;
            }

            set
            {
                m_Gets = value;
            }
        }
        #endregion

        public double Price
        {
            get
            {
                return m_Price;
            }

            set
            {
                m_Price = value;
            }
        }

        public object Amount
        {
            get
            {
                return m_Amount;
            }

            set
            {
                m_Amount = value;
            }
        }

        public CounterParty CounterParty
        {
            get
            {
                return m_CounterParty;
            }

            set
            {
                m_CounterParty = value;
            }
        }
        #endregion
    }

    public class CounterParty : JingtumObject
    {
        //    "counterparty": {
        //    "account": "j3oVY8Zho4mX3Q2ehcvjY7zukYmBLytvWi",
        //    "seq": 240,
        //    "hash": "DF388654EACCC19836523E8D79B26651F24399F41FC90C1A350E84541ACCBDA1"
        //    },

        #region fields
        private string m_Account;
        private int m_Seq;
        private string m_Hash;
        #endregion

        #region properties
        public int Seq
        {
            get
            {
                return m_Seq;
            }

            set
            {
                m_Seq = value;
            }
        }

        public string Account
        {
            get
            {
                return m_Account;
            }

            set
            {
                m_Account = value;
            }
        }

        public string Hash
        {
            get
            {
                return m_Hash;
            }

            set
            {
                m_Hash = value;
            }
        }
        #endregion
    }


    /// <summary>
    /// For paid, got, pays, gets.
    /// </summary>
    public class Amount : JingtumObject
    {
        //    "paid": {
        //    "value": "0.01269999999999527",
        //    "currency": "CNY",
        //    "issuer": "jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o9or"
        //    },

        #region fields
        private double m_Value;
        private string m_Currency;
        private string m_Issuer;
        #endregion

        #region properties
        public double Value
        {
            get
            {
                return m_Value;
            }

            set
            {
                m_Value = value;
            }
        }

        public string Currency
        {
            get
            {
                return m_Currency;
            }

            set
            {
                m_Currency = value;
            }
        }

        public string Issuer
        {
            get
            {
                return m_Issuer;
            }

            set
            {
                m_Issuer = value;
            }
        }
        #endregion
    }

    public class Payment : JingtumObject
    {
        #region fields
        private int m_Date;
        private string m_Hash;
        private double m_Fee;
        private string m_Type;
        private string m_Result;
        private List<string> m_Memos;
        private List<Effects> m_Effects;
        private object m_Amount;

        private CounterParty m_CounterParty;
        #endregion

        #region properties
        #region Date
        public int Date
        {
            get
            {
                return m_Date;
            }

            set
            {
                m_Date = value;
            }
        }

        /// <summary>
        /// Convert unix datetime to normal datetime
        /// =(m_Date+8*3600)/86400+70*365+19
        /// </summary>
        public DateTime RealDate
        {
            get
            {
                return Utility.ConvertUnixTime2DateTime(this.m_Date);
            }
        }
        #endregion

        #region strings
        public string Hash
        {
            get
            {
                return m_Hash;
            }

            set
            {
                m_Hash = value;
            }
        }

        public string Type
        {
            get
            {
                return m_Type;
            }

            set
            {
                m_Type = value;
            }
        }

        public string Result
        {
            get
            {
                return m_Result;
            }

            set
            {
                m_Result = value;
            }
        }

        public List<string> Memos
        {
            get
            {
                return m_Memos;
            }

            set
            {
                m_Memos = value;
            }
        }
        #endregion

        #region numbers
        public double Fee
        {
            get
            {
                return m_Fee;
            }

            set
            {
                m_Fee = value;
            }
        }

        public object Amount
        {
            get
            {
                return m_Amount;
            }

            set
            {
                m_Amount = value;
            }
        }
        #endregion

        public List<Effects> Effects
        {
            get
            {
                return m_Effects;
            }

            set
            {
                m_Effects = value;
            }
        }
        #endregion
    }

    public class Transaction : JingtumObject
    {
        //"transactions": [
        //{
        //"date": 1505046230,
        //"hash": "220E96A7E5C12994CF177E26B6D818EC8D9538B94A5D1582E2BB233C9D9F5F7D",
        //"type": "offernew",
        //"fee": "0.00001",
        //"result": "tesSUCCESS",
        //"memos": [],
        //"offertype": "buy",
        //"seq": 9,
        //"effects": [
        //{
        //    "effect": "offer_bought",
        //    "counterparty": {
        //    "account": "j3oVY8Zho4mX3Q2ehcvjY7zukYmBLytvWi",
        //    "seq": 240,
        //    "hash": "DF388654EACCC19836523E8D79B26651F24399F41FC90C1A350E84541ACCBDA1"
        //    },
        //    "paid": {
        //    "value": "0.01269999999999527",
        //    "currency": "CNY",
        //    "issuer": "jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o9or"
        //    },
        //    "got": {
        //    "value": "1",
        //    "currency": "SWT",
        //    "issuer": ""
        //    },
        //    "type": "bought",
        //    "price": 0.01269999999999527,
        //    "pair": "SWT/CNY:jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o9or",
        //    "amount": "1"
        //}
        //],
        //"pair": "SWT/CNY:jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o9or",
        //"amount": "1",
        //"price": 0.0127
        //}
        //]

        #region fields
        private int m_Date;
        private string m_Hash;
        private double m_Fee;
        private List<string> m_Memos;
        private string m_Type;
        private List<Effects> m_Effects;
        private object m_Amount;

        private int m_Seq;
        private string m_Result;
        private string m_OfferType;
        #endregion

        #region properties
        #region Date
        public int Date
        {
            get
            {
                return m_Date;
            }

            set
            {
                m_Date = value;
            }
        }

        /// <summary>
        /// Convert unix datetime to normal datetime
        /// =(m_Date+8*3600)/86400+70*365+19
        /// </summary>
        public DateTime RealDate
        {
            get
            {
                return Utility.ConvertUnixTime2DateTime(this.m_Date);
            }
        }
        #endregion

        #region strings
        public string Hash
        {
            get
            {
                return m_Hash;
            }

            set
            {
                m_Hash = value;
            }
        }

        public string Type
        {
            get
            {
                return m_Type;
            }

            set
            {
                m_Type = value;
            }
        }

        public string Result
        {
            get
            {
                return m_Result;
            }

            set
            {
                m_Result = value;
            }
        }

        public string OfferType
        {
            get
            {
                return m_OfferType;
            }

            set
            {
                m_OfferType = value;
            }
        }

        public List<string> Memos
        {
            get
            {
                return m_Memos;
            }

            set
            {
                m_Memos = value;
            }
        }
        #endregion

        #region numbers
        public double Fee
        {
            get
            {
                return m_Fee;
            }

            set
            {
                m_Fee = value;
            }
        }

        public int Seq
        {
            get
            {
                return m_Seq;
            }

            set
            {
                m_Seq = value;
            }
        }

        public object Amount
        {
            get
            {
                return m_Amount;
            }

            set
            {
                m_Amount = value;
            }
        }
        #endregion

        public List<Effects> Effects
        {
            get
            {
                return m_Effects;
            }

            set
            {
                m_Effects = value;
            }
        }
        #endregion
    }
}
