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

        #region methods
        #region new methods
        public static Wallet New()
        {
            //https://api.jingtum.com/v2/wallet/
            string url = Net.APIServer.URL_SERVER_ADDRESS
            + Net.APIServer.URL_SERVER_VERSION
            + Net.APIServer.SIGN_BACK_SLASH
            + "wallet"
            + Net.APIServer.SIGN_BACK_SLASH
            + "new";
            Net.APIServer apiServer = new Net.APIServer();
            return apiServer.Request_Get<WalletResponse>(url).Wallet;
        }
        #endregion

        #region balance methods
        public List<Balance> GetBalanceList()
        {
            return this.GetBalanceListByUrl(m_Server.FormatURL(this.Address, (new Balance()).APIMethodName()));
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

            return this.GetBalanceListByUrl(m_Server.FormatURL(this.Address, (new Balance()).APIMethodName(), parameter));
        }

        private List<Balance> GetBalanceListByUrl(string url)
        {
            return m_Server.Request_Get<BalancesResponse>(url).Balances;
        }
        #endregion

        #region payment methods
        public List<Payment> GetPaymentList()
        {
            return this.m_Server.Request_Get<PaymentsResponse>(m_Server.FormatURL(this.Address, (new Payment()).APIMethodName())).Payments;
        }

        public Payment GetPayment(string hash)
        {
            string parameter = Net.APIServer.SIGN_BACK_SLASH + hash;
            PaymentResponse response = m_Server.Request_Get<PaymentResponse>((m_Server.FormatURL(this.Address, (new Payment()).APIMethodName(), parameter)));
            return response.Payment;
        }

        public SetPaymentResponse SetPayment(Payment payment, string destinationAddress, string choiceHash, List<string> memos)
        {
            string choiceString = (choiceHash == string.Empty) ? string.Empty : ",\"choice\":\"" + choiceHash + "\"";
            string memosString = string.Empty;
            if (memos != null && memos.Count > 0)
            {
                memosString = ",\"memos\":[";

                for (int i = 0; i < memos.Count; i++)
                {
                    memosString += "\"" + memos[i] + "\"";
                    if (i != memos.Count - 1)
                    {
                        memosString += ",";
                    }
                }

                memosString += "]";
            }

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
                + "{10}"//memo
                + "{1}{1}";

            StringBuilder parameters = new StringBuilder();

            parameters.AppendFormat(format, "{", "}", this.Secret, this.GenerateClientID(), this.Address, destinationAddress,
                payment.Amount.Value.ToString(), payment.Amount.Currency, payment.Amount.Issuer, choiceString, memosString);

            return this.m_Server.Request_Post<SetPaymentResponse>(m_Server.FormatURL(this.Address, (new Payment()).APIMethodName()), parameters.ToString());
        }

        private string GenerateClientID()
        {
            return "PaymentID_" + DateTime.Now.ToString("o"); // generate a unique client ID
        }

        public List<PaymentChoice> GetPaymentChoices(string destinationAddress, double value, string currency, string issuer)
        {
            string parameter = (new Payment()).APIMethodName()
                + Net.APIServer.SIGN_BACK_SLASH + (new PaymentChoice()).APIMethodName()
                + Net.APIServer.SIGN_BACK_SLASH + destinationAddress
                + Net.APIServer.SIGN_BACK_SLASH + value
                + Net.APIServer.SIGN_ADD + currency
                + Net.APIServer.SIGN_ADD + issuer;
            return this.m_Server.Request_Get<PaymentChoicesResponse>(m_Server.FormatURL(this.Address, parameter)).Choices;
        }

        public List<PaymentChoice> GetPaymentChoices(string destinationAddress, Amount amount)
        {
            return this.GetPaymentChoices(destinationAddress, amount.Value, amount.Currency, amount.Issuer);
        }
        #endregion

        #region order methods
        public Order GetOrder(string hash)
        {
            string parameter = Net.APIServer.SIGN_BACK_SLASH + hash;
            return m_Server.Request_Get<OrderResponse>((m_Server.FormatURL(this.Address, (new Order()).APIMethodName(), parameter))).Order;
        }

        public List<Order> GetOrderList()
        {
            return m_Server.Request_Get<OrdersResponse>((m_Server.FormatURL(this.Address, (new Order()).APIMethodName()))).Orders;
        }

        public SetOrderResponse SetOrder(Order order)
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

            return this.m_Server.Request_Post<SetOrderResponse>(m_Server.FormatURL(this.Address, (new Order()).APIMethodName()), parameters.ToString());    
        }

        public SetOrderResponse CancelOrder(string sequence)
        {
            string parameters = "{\"secret\":\"" + this.Secret + "\"}";
            return this.m_Server.Request_Delete<SetOrderResponse>(
                m_Server.FormatURL(this.Address, (new Order()).APIMethodName()) + Net.APIServer.SIGN_BACK_SLASH + sequence, parameters);
        }

        public OrderBookResponse GetOrderBooks(string baseCurrency, string baseCounterParty, string counterCurrency, string counterCounterParty)
        {
            string parameter = (new OrderBook()).APIMethodName()
                + Net.APIServer.SIGN_BACK_SLASH + baseCurrency
                + Net.APIServer.SIGN_ADD + baseCounterParty
                + Net.APIServer.SIGN_BACK_SLASH + counterCurrency
                + Net.APIServer.SIGN_ADD + counterCounterParty;
            return m_Server.Request_Get<OrderBookResponse>(m_Server.FormatURL(this.Address, parameter));
        }
        #endregion
        
        #region transactions methods
        public Transaction GetTransaction(string hash)
        {
            string parameter = Net.APIServer.SIGN_BACK_SLASH + hash;
            return m_Server.Request_Get<TranactionResponse>((m_Server.FormatURL(this.Address, (new Transaction()).APIMethodName(), parameter))).Transaction;
        }

        public List<Transaction> GetTransactions()
        {
            return this.GetTransactions(0, 0, 0);
        }

        public List<Transaction> GetTransactions(int results_per_page)
        {
            return this.GetTransactions(results_per_page, 0, 0);
        }


        public List<Transaction> GetTransactions(int ledger, int seq)
        {
            return this.GetTransactions(0, ledger, seq);
        }

        public List<Transaction> GetTransactions(int results_per_page, int ledger, int seq)
        {
            return this.GetTransactionsResponse(results_per_page, ledger, seq).Transactions;            
        }

        private TransactionsResponse GetTransactionsResponse(int results_per_page, int ledger, int seq)
        {
            string parameter = (results_per_page <= 0) ? string.Empty : Net.APIServer.SIGN_QUESTION_MARK + "results_per_page=" + results_per_page;
            if (ledger > 0)
            {
                parameter += (results_per_page <= 0) ? Net.APIServer.SIGN_QUESTION_MARK : Net.APIServer.SIGN_AND;
                parameter += "marker={ledger:" + ledger + ",seq:" + seq + "}";
            }

            return m_Server.Request_Get<TransactionsResponse>((m_Server.FormatURL(this.Address, (new Transaction()).APIMethodName(), parameter)));
        }

        public List<Transaction> GetAllTransactions()
        {
            return this.GetAllTransactions(20);
        }

        public List<Transaction> GetAllTransactions(int results_per_page)
        {
            List<Transaction> transactions = new List<Transaction>();

            //get the first transaction
            TransactionsResponse response = this.GetTransactionsResponse(results_per_page, 0, 0);
            transactions.AddRange(response.Transactions);

            while (response.Marker != null && response.Marker.Ledger != 0)
            {
                response = this.GetTransactionsResponse(results_per_page, response.Marker.Ledger, response.Marker.Seq);
                transactions.AddRange(response.Transactions);
            }

            return transactions;
        }
        #endregion
        #endregion
    }

    public class Account : JingtumObject
    {
        #region field
        protected const int MIN_ACTIVATED_AMOUNT = 25;
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
