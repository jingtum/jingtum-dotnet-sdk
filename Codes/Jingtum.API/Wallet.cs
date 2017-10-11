using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Jingtum.API
{
    public class Wallet : Account
    {
        #region fields
        API.Net.APIServer m_Server = new Net.APIServer();
        #endregion        

        #region constructor
        public Wallet()
        {
        }

        public Wallet(string address, string secret)
        {
            this.Address = address;
            this.Secret = secret;
        }
        #endregion

        #region properties
        public bool IsActived
        {
            get
            {
                try 
                {
                    return this.GetBalanceList(Utility.CURRENCY_SWT, string.Empty)[0].Value >= Wallet.MIN_ACTIVATED_AMOUNT;
                }
                catch(Exception ex)
                {
                    return false;
                }                
            }
        }
        #endregion

        #region methods
        #region new methods
        internal static string GetUrl_WalletNew()
        {
            //https://api.jingtum.com/v2/wallet/
            return Net.APIServer.URL_SERVER_ADDRESS
            + Net.APIServer.URL_SERVER_VERSION
            + Net.APIServer.SIGN_BACKSLASH
            + "wallet"
            + Net.APIServer.SIGN_BACKSLASH
            + "new";
        }

        public static Wallet New()
        {
            string url = Wallet.GetUrl_WalletNew();
            Net.APIServer apiServer = new Net.APIServer();
            return apiServer.Request_Get<WalletResponse>(url).Wallet;
        }
        #endregion

        #region balance methods
        internal string GetUrl_Balances()
        {
            return GetUrl_Balances(string.Empty, string.Empty);
        }

        internal string GetUrl_Balances(string currency, string issuer)
        {
            string parameter = Net.APIServer.FormatParameter_Currency(string.Empty, currency);
            parameter = Net.APIServer.FormatParameter_Issuer(parameter, issuer);
            return m_Server.FormatURL(this.Address, (new Balance()).APIMethodName(), parameter);
        }

        public List<Balance> GetBalanceList()
        {
            return m_Server.Request_Get<BalancesResponse>(GetUrl_Balances()).Balances;
        }

        public List<Balance> GetBalanceList(string currency, string issuer)
        {
            return m_Server.Request_Get<BalancesResponse>(GetUrl_Balances(currency, issuer)).Balances;
        }
        #endregion

        #region payment methods
        #region payment list
        internal string GetUrl_Payments()
        {
            return m_Server.FormatURL(this.Address, (new Payment()).APIMethodName());
        }

        internal string GetUrl_Payments(int results_per_page, int page)
        {
            string parameters = Net.APIServer.FormatParameter_Result_Per_Page(string.Empty, results_per_page, page);
            return m_Server.FormatURL(this.Address, (new Payment()).APIMethodName(), parameters);
        }

        public List<Payment> GetPaymentList()
        {
            return this.m_Server.Request_Get<PaymentsResponse>(GetUrl_Payments()).Payments;
        }

        /// <summary>
        /// Get payments by results_per_page and page.
        /// </summary>
        /// <param name="results_per_page">If parameter results_per_page is less than 1, regard as results_per_page doesn't work.</param>
        /// <param name="page">If parameter page is less than 1, regard as page doesn't work.</param>
        /// <returns></returns>
        public List<Payment> GetPaymentList(int results_per_page, int page)
        {
            return m_Server.Request_Get<PaymentsResponse>(GetUrl_Payments(results_per_page, page)).Payments;
        }

        internal string GetUrl_Payment(string hash)
        {
            string parameter = Net.APIServer.FormatParameter_Hash(hash);
            return m_Server.FormatURL(this.Address, (new Payment()).APIMethodName(), parameter);
        }

        public Payment GetPayment(string hash)
        {
            PaymentResponse response = m_Server.Request_Get<PaymentResponse>(GetUrl_Payment(hash));
            return response.Payment;
        }
        #endregion

        #region set payment
        internal string GetParameters_SetPayment(Payment payment, string destinationAddress, string choiceHash, List<string> memos)
        {
            string choiceString = (choiceHash == string.Empty) ? string.Empty : ",\"choice\":\"" + choiceHash + "\"";
            //string memosString = string.Empty;
            //if (memos != null && memos.Count > 0)
            //{
            //    memosString = ",\"memos\":[";

            //    for (int i = 0; i < memos.Count; i++)
            //    {
            //        memosString += "\"" + memos[i] + "\"";
            //        if (i != memos.Count - 1)
            //        {
            //            memosString += ",";
            //        }
            //    }

            //    memosString += "]";
            //}

            string memosString = Wallet.StringList2Parameter("memos", memos);

            //need replace this by serialize, such string is hard to update and maintain.
            string format =
                "{0}\"secret\":\"{2}\","
                + "\"client_id\":\"{3}\","
                + "\"payment\":{0}"
                + "\"source\":\"{4}\","
                + "\"destination\":\"{5}\","
                + "\"amount\":{0}"
                + "\"value\":\"{6}\","
                + "\"currency\":\"{7}\","
                + "\"issuer\":\"{8}\""
                + "{1}"
                + "{9}"//choices
                + ",{10}"//memo
                + "{1}{1}";

            StringBuilder parameters = new StringBuilder();

            parameters.AppendFormat(format, "{", "}", this.Secret, this.GenerateClientID(), this.Address, destinationAddress,
                payment.Amount.Value.ToString(), payment.Amount.Currency, payment.Amount.Issuer, choiceString, memosString);

            return parameters.ToString();
        }
        
        public SetPaymentResponse SetPayment(Payment payment, string destinationAddress, string choiceHash, List<string> memos)
        {
            string url = GetUrl_Payments();
            string parameters = GetParameters_SetPayment(payment, destinationAddress, choiceHash, memos);

            return this.m_Server.Request_Post<SetPaymentResponse>(url, parameters);
        }

        internal string GenerateClientID()
        {
            return "PaymentID_" + DateTime.Now.ToString("o"); // generate a unique client ID
        }
        #endregion

        #region payment choices
        internal string GetUrl_PaymentChoices(string destinationAddress, double value, string currency, string issuer)
        {
            string parameter = (new Payment()).APIMethodName()
                + Net.APIServer.SIGN_BACKSLASH + (new PaymentChoice()).APIMethodName()
                + Net.APIServer.SIGN_BACKSLASH + destinationAddress
                + Net.APIServer.SIGN_BACKSLASH + value
                + Net.APIServer.SIGN_ADD + currency
                + Net.APIServer.SIGN_ADD + issuer;

            return m_Server.FormatURL(this.Address, parameter);
        }

        public List<PaymentChoice> GetPaymentChoices(string destinationAddress, double value, string currency, string issuer)
        {
            return this.m_Server.Request_Get<PaymentChoicesResponse>(GetUrl_PaymentChoices(destinationAddress, value, currency, issuer)).Choices;
        }

        public List<PaymentChoice> GetPaymentChoices(string destinationAddress, Amount amount)
        {
            return this.GetPaymentChoices(destinationAddress, amount.Value, amount.Currency, amount.Issuer);
        }
        #endregion
        #endregion

        #region order methods
        #region order list
        internal string GetUrl_Order(string hash)
        {
            string parameter = Net.APIServer.FormatParameter_Hash(hash);
            return m_Server.FormatURL(this.Address, (new Order()).APIMethodName(), parameter);
        }

        public Order GetOrder(string hash)
        {
            string parameter = Net.APIServer.SIGN_BACKSLASH + hash;
            return m_Server.Request_Get<OrderResponse>(GetUrl_Order(hash)).Order;
        }

        internal string GetUrl_Orders()
        {
            return m_Server.FormatURL(this.Address, (new Order()).APIMethodName());
        }

        internal string GetUrl_Orders(int results_per_page, int page)
        {
            string parameters = Net.APIServer.FormatParameter_Result_Per_Page(string.Empty, results_per_page, page);
            return m_Server.FormatURL(this.Address, (new Order()).APIMethodName(), parameters);
        }

        public List<Order> GetOrderList()
        {
            return m_Server.Request_Get<OrdersResponse>(GetUrl_Orders()).Orders;
        }

        /// <summary>
        /// Get payments by results_per_page and page.
        /// </summary>
        /// <param name="results_per_page">If parameter results_per_page is less than 1, regard as results_per_page doesn't work.</param>
        /// <param name="page">If parameter page is less than 1, regard as page doesn't work.</param>
        /// <returns></returns>
        public List<Order> GetOrderList(int results_per_page, int page)
        {
            string parameters = Net.APIServer.FormatParameter_Result_Per_Page(string.Empty, results_per_page, page);

            return m_Server.Request_Get<OrdersResponse>(GetUrl_Orders(results_per_page, page)).Orders;
        }
        #endregion

        #region set and cancel order
        internal string GetParameters_SetOrder(Order order)
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

            return parameters.ToString();
        }

        public SetOrderResponse SetOrder(Order order)
        {
            string parameter = GetParameters_SetOrder(order);
            string url = GetUrl_Orders();

            return this.m_Server.Request_Post<SetOrderResponse>(url, parameter);    
        }

        internal string GetParameters_CancelOrder()
        {
            string parameter = "{\"secret\":\"" + this.Secret + "\"}";
            return parameter;
        }

        internal string GetUrl_CancelOrder(int sequence)
        {
            return m_Server.FormatURL(this.Address, (new Order()).APIMethodName()) + Net.APIServer.SIGN_BACKSLASH + sequence;
        }

        public SetOrderResponse CancelOrder(int sequence)
        {
            string parameter = GetParameters_CancelOrder();
            string url = GetUrl_CancelOrder(sequence);

            return this.m_Server.Request_Delete<SetOrderResponse>(url, parameter);
        }
        #endregion

        #region order books
        private string GetUrl_OrderBooks(string type, string baseCurrency, string baseCounterParty, string counterCurrency, string counterCounterParty, int results_per_page, int page)
        {
            string typeParameter = (type == string.Empty) ? string.Empty : Net.APIServer.SIGN_BACKSLASH + type;
            string parameter = (new OrderBook()).APIMethodName()
                + typeParameter
                + Net.APIServer.SIGN_BACKSLASH
                + Net.APIServer.FormatParameter_Currency_CounterParty(baseCurrency, baseCounterParty)
                + Net.APIServer.SIGN_BACKSLASH
                + Net.APIServer.FormatParameter_Currency_CounterParty(counterCurrency, counterCounterParty)
                + Net.APIServer.FormatParameter_Result_Per_Page(string.Empty, results_per_page, page);

            return Net.APIServer.URL_SERVER_ADDRESS_VERSION + parameter;
        }

        internal string GetUrl_OrderBooks(string baseCurrency, string baseCounterParty, string counterCurrency, string counterCounterParty, int results_per_page, int page)
        {
            return GetUrl_OrderBooks(string.Empty, baseCurrency, baseCounterParty, counterCurrency, counterCounterParty, results_per_page, page);
        }

        internal string GetUrl_OrderBook_Bids(string baseCurrency, string baseCounterParty, string counterCurrency, string counterCounterParty, int results_per_page, int page)
        {
            return GetUrl_OrderBooks("bids", baseCurrency, baseCounterParty, counterCurrency, counterCounterParty, results_per_page, page);
        }

        internal string GetUrl_OrderBook_Asks(string baseCurrency, string baseCounterParty, string counterCurrency, string counterCounterParty, int results_per_page, int page)
        {
            return GetUrl_OrderBooks("asks", baseCurrency, baseCounterParty, counterCurrency, counterCounterParty, results_per_page, page);
        }

        public OrderBookResponse GetOrderBooks(string baseCurrency, string baseCounterParty, string counterCurrency, string counterCounterParty, int results_per_page, int page)
        {
            return m_Server.Request_Get<OrderBookResponse>(GetUrl_OrderBooks(baseCurrency, baseCounterParty, counterCurrency, counterCounterParty, results_per_page, page));
        }

        public OrderBookResponse GetOrderBookBids(string baseCurrency, string baseCounterParty, string counterCurrency, string counterCounterParty, int results_per_page, int page)
        {
            return m_Server.Request_Get<OrderBookResponse>(GetUrl_OrderBook_Bids(baseCurrency, baseCounterParty, counterCurrency, counterCounterParty, results_per_page, page));
        }

        public OrderBookResponse GetOrderBookAsks(string baseCurrency, string baseCounterParty, string counterCurrency, string counterCounterParty, int results_per_page, int page)
        {
            return m_Server.Request_Get<OrderBookResponse>(GetUrl_OrderBook_Asks(baseCurrency, baseCounterParty, counterCurrency, counterCounterParty, results_per_page, page));
        }
        #endregion
        #endregion

        #region transactions methods
        #region one transaction
        internal string GetUrl_Transaction(string hash)
        {
            string parameter = Net.APIServer.FormatParameter_Hash(hash);
            return m_Server.FormatURL(this.Address, (new Transaction()).APIMethodName(), parameter);
        }

        public Transaction GetTransaction(string hash)
        {
            string url = GetUrl_Transaction(hash);
            return m_Server.Request_Get<TranactionResponse>(url).Transaction;
        }
        #endregion

        #region transactions
        internal string GetUrl_Transactions(int results_per_page, int page, int ledger, int seq)
        {
            string parameters = Net.APIServer.FormatParameter_Result_Per_Page(string.Empty, results_per_page, page);
            parameters = Net.APIServer.FormatParameter_Marker(parameters, ledger, seq);
            return m_Server.FormatURL(this.Address, (new Transaction()).APIMethodName(), parameters);
        }

        private TransactionsResponse GetTransactionsResponse(int results_per_page, int page, int ledger, int seq)
        {
            string url = GetUrl_Transactions(results_per_page, page, ledger, seq);
            return m_Server.Request_Get<TransactionsResponse>(url);
        }

        public List<Transaction> GetTransactions()
        {
            return this.GetTransactions(0, 0, 0, 0);
        }

        public List<Transaction> GetTransactions(int results_per_page, int page, int ledger, int seq)
        {
            return this.GetTransactionsResponse(results_per_page, page, ledger, seq).Transactions;            
        }        
        #endregion

        #region all transactions
        public List<Transaction> GetAllTransactions()
        {
            const int BEST_COUNT_OF_RESULT_PER_PAGE = 20;
            return this.GetAllTransactions(BEST_COUNT_OF_RESULT_PER_PAGE);
        }

        public List<Transaction> GetAllTransactions(int results_per_page)
        {
            List<Transaction> transactions = new List<Transaction>();

            //get the first transaction
            TransactionsResponse response = this.GetTransactionsResponse(results_per_page, 0, 0, 0);
            transactions.AddRange(response.Transactions);

            while (response.Marker != null && response.Marker.Ledger != 0)
            {
                response = this.GetTransactionsResponse(results_per_page, 0, response.Marker.Ledger, response.Marker.Seq);
                transactions.AddRange(response.Transactions);
            }

            return transactions;
        }
        #endregion
        #endregion

        #region smart contract
        #region deploy
        internal string GetParameters_Deploy(double amount, string payload)
        {
            string format =
                "{0}\"secret\":\"{2}\","
                + "\"amount\":{3},"
                + "\"payload\":\"{4}\"{1}";

            StringBuilder parameters = new StringBuilder();
            parameters.AppendFormat(format, "{", "}", this.Secret, amount, payload);

            return parameters.ToString();
        }

        internal string GetUrl_Deploy()
        {
            return m_Server.FormatURL(this.Address, "contract/deploy"); 
        }

        public DeployResponse Deploy(double amount, string payload)
        {
            string parameter = GetParameters_Deploy(amount, payload);
            string url = GetUrl_Deploy();

            return this.m_Server.Request_Post<DeployResponse>(url, parameter);
        }
        #endregion

        #region call
        internal string GetParameters_Call(string destination, List<string> parameters)
        {
            string parameterList = Wallet.StringList2Parameter("params", parameters);

            string format =
                "{0}\"secret\":\"{2}\","
                + "\"destination\":\"{3}\","
                + "{4}{1}";

            StringBuilder result = new StringBuilder();
            result.AppendFormat(format, "{", "}", this.Secret, destination, parameterList);

            return result.ToString();
        }

        internal string GetUrl_Call()
        {
            return m_Server.FormatURL(this.Address, "contract/call");
        }

        public CallResponse Call(string destination, List<string> parameters)
        {
            string parameter = GetParameters_Call(destination, parameters);
            string url = GetUrl_Call();

            return this.m_Server.Request_Post<CallResponse>(url, parameter);
        }
        #endregion
        #endregion

        #region sign
        internal string GetParameters_Sign(string blob)
        {
            string format = "{0}\"blob\":\"{2}\"{1}";
            StringBuilder parameters = new StringBuilder();
            parameters.AppendFormat(format, "{", "}", blob);

            return parameters.ToString();
        }

        internal string GetUrl_Sign()
        {
            return Net.APIServer.URL_SERVER_ADDRESS_VERSION + "sign";
        }

        public DealResponse Sign(string blob)
        {
            string parameter = GetParameters_Sign(blob);
            string url = GetUrl_Sign();

            return this.m_Server.Request_Post<DealResponse>(url, parameter);
        }
        #endregion

        #region common
        private static string StringList2Parameter(string parameterName, List<string> values)
        {
            string result = string.Empty;
            if (values != null && values.Count > 0)
            {
                result = "\"" + parameterName + "\":[";

                for (int i = 0; i < values.Count; i++)
                {
                    result += "\"" + values[i] + "\"";
                    if (i != values.Count - 1)
                    {
                        result += ",";
                    }
                }

                result += "]";
            }

            return result;
        }
        #endregion
        #endregion
    }

    public class Account : JingtumObject
    {
        #region field
        protected const double MIN_ACTIVATED_AMOUNT = 25;
        protected string m_Address;
        protected string m_Secret;
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
    }
}
