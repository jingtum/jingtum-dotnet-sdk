using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace Jingtum.API
{
    public class Wallet : AccountClass
    {
        #region fields
        API.Net.APIServer m_Server = new Net.APIServer();
        #endregion

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

        #region methods
        private Net.Response GetResponseByUrl(string url)
        {
            return JsonConvert.DeserializeObject<Net.Response>(m_Server.GetResponse(url));
        }

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

            if (Utility.isValidAddress(issuer))
            {
                parameter += (parameter != string.Empty) ? Net.APIServer.SIGN_AND : Net.APIServer.SIGN_QUESTION_MARK + "issuer=" + issuer;
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
        #endregion

        #region payment methods
        #endregion

        #region transactions methods
        #endregion
        #endregion
    }

    public class AccountClass 
    {
        protected const int MIN_ACTIVATED_AMOUNT = 25;
        protected string m_Address;
        protected string m_Secret;
    }
}
