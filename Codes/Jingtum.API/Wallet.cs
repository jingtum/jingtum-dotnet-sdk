using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace Jingtum.API
{
    public class Wallet : Account
    {
        #region fields
        API.Net.APIServer m_Server = new Net.APIServer();
        #endregion        

        #region methods
        private Net.Response GetResponseByUrl(string url)
        {
            return JsonConvert.DeserializeObject<Net.Response>(m_Server.GetResponse(url));
        }

        #region new methods
        public static Wallet New()
        {
            string url = Net.APIServer.URL_SERVER_ADDRESS_WALLET + "new";
            Net.APIServer apiServer = new Net.APIServer();
            return JsonConvert.DeserializeObject<Net.Response>(apiServer.GetResponse(url)).Wallet;
        }
        #endregion

        #region balance methods
        public List<Balance> GetBalanceList()
        {
            return this.GetBalanceListByUrl(m_Server.FormatURL(this.Address, (new Balance()).APIMethodName));
        }
        
        public List<Balance> GetBalanceList(string currency, string issuer)
        {
            string parameter = string.Empty;

            if(Utility.IsValidCurrency(currency))
            {
                parameter += Net.APIServer.SIGN_QUESTION_MARK + "currency=" + currency;
            }
            else
            {
                throw new InvalidParameterException(JingtumMessage.INVALID_CURRENCY, currency);
            }

            if (Utility.IsValidAddress(issuer))
            {
                parameter += (parameter != string.Empty) ? Net.APIServer.SIGN_AND : Net.APIServer.SIGN_QUESTION_MARK + "issuer=" + issuer;
            }
            else
            {
                throw new InvalidParameterException(JingtumMessage.INVALID_JINGTUM_ADDRESS, issuer);
            }

            return this.GetBalanceListByUrl(m_Server.FormatURL(this.Address, (new Balance()).APIMethodName, parameter));
        }

        private List<Balance> GetBalanceListByUrl(string url)
        {            
            return GetResponseByUrl(url).Balances;
        }
        #endregion

        #region order methods
        public Order GetOrder(string hash)
        {
            string parameter = Net.APIServer.SIGN_BACK_SLASH + hash;
            return GetResponseByUrl((m_Server.FormatURL(this.Address, (new Order()).APIMethodName, parameter))).Order;
        }

        public List<Order> GetOrderList()
        {
            return GetResponseByUrl((m_Server.FormatURL(this.Address, (new Order()).APIMethodName))).Orders;
        }

        public string SetOrder(Order order)
        {
            string format =
                "{0}\"secret\":\"{2}\","
                + "\"order\":{0}"
                + "\"type\":\"{3}\","
                + "\"pair\":\"{4}\","
                + "\"amount\":\"{5}\","
                + "\"price\":{6}{1}{1}";

            StringBuilder parameters = new StringBuilder();
            parameters.AppendFormat(format, "{", "}", this.Secret, order.Type, order.Pair, order.Amount.ToString(), order.Price.ToString());

            return this.m_Server.Request("POST", m_Server.FormatURL(this.Address, (new Order()).APIMethodName), parameters.ToString());            
        }

        public string CancelOrder(string sequence)
        {
            string parameters = "{\"secret\":\"" + this.Secret + "\"}";
            return this.m_Server.Request("DELETE", 
                m_Server.FormatURL(this.Address, (new Order()).APIMethodName) + Net.APIServer.SIGN_BACK_SLASH + sequence, parameters);
        }
        #endregion

        #region payment methods
        #endregion

        #region transactions methods
        #endregion
        #endregion
    }

    public class Account : JingtumObject
    {
        protected const int MIN_ACTIVATED_AMOUNT = 25;
        protected string m_Address;
        protected string m_Secret;

        #region properties
        public string Address
        {
            get
            {
                return m_Address;
            }

            set
            {
                m_Address = value;
            }
        }

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
        #endregion
    }
}
