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

    public class Request_Order : Order
    {
        private string m_Secret;
        private Order m_Order;

        public string Secret
        {
            get
            {
                return m_Secret;
            }

            set
            {
                m_Secret = value;
            }
        }

        public Order Order
        {
            get
            {
                return m_Order;
            }

            set
            {
                m_Order = value;
            }
        }
    }


}
