using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jingtum.API
{
    #region base responses
    public interface IResponse
    {
        bool Success
        {
            get;
            set;
        }

        string Status_Code
        {
            get;
            set;
        }
    }

    public class BaseResponse : IResponse
    {
        #region fields
        private bool m_Success;
        private string m_StatusCode;
        #endregion

        #region properties
        public bool Success
        {
            get
            {
                return m_Success;
            }

            set
            {
                m_Success = value;
            }
        }

        public string Status_Code
        {
            get
            {
                return m_StatusCode;
            }

            set
            {
                m_StatusCode = value;
            }
        }
        #endregion
    }

    /// <summary>
    /// The response result when Jingtum API return errors.
    /// </summary>
    public class ErrorResponse : BaseResponse
    {
        #region fields
        private string m_Error_Type;
        private string m_Error;
        protected string m_Message;
        #endregion

        #region properties
        public string Message
        {
            set
            {
                m_Message = value;
            }

            get
            {
                return m_Message;
            }
        }

        public string Error_Type
        {
            set
            {
                m_Error_Type = value;
            }

            get
            {
                return m_Error_Type;
            }
        }

        public string Error
        {
            set
            {
                m_Error = value;
            }

            get
            {
                return m_Error;
            }
        }
        #endregion
    }    
    #endregion

    #region wallet responses
    public class WalletResponse : BaseResponse
    {
        #region fields
        private Wallet m_Wallet;
        #endregion

        #region properties
        public Wallet Wallet
        {
            get
            {
                return m_Wallet;
            }

            set
            {
                m_Wallet = value;
            }
        }
        #endregion
    }

    public class BalancesResponse : BaseResponse
    {
        #region fields
        private List<Balance> m_Balances = new List<Balance>();
        #endregion

        #region properties
        public List<Balance> Balances
        {
            get
            {
                return m_Balances;
            }
        }
        #endregion
    }
    #endregion

    #region payment responses
    public class PaymentResponse : BaseResponse
    {
        #region fields
        private Payment m_Payment = new Payment();
        #endregion

        #region properties
        public Payment Payment
        {
            get
            {
                return m_Payment;
            }

            set
            {
                m_Payment = value;
            }
        }
        
        #region Date
        public int Date
        {
            get
            {
                return m_Payment.Date;
            }

            set
            {
                m_Payment.Date = value;
            }
        }
        #endregion

        #region strings
        public string Hash
        {
            get
            {
                return m_Payment.Hash;
            }

            set
            {
                m_Payment.Hash = value;
            }
        }

        public string Type
        {
            get
            {
                return m_Payment.Type;
            }

            set
            {
                m_Payment.Type = value;
            }
        }

        public string Result
        {
            get
            {
                return m_Payment.Result;
            }

            set
            {
                m_Payment.Result = value;
            }
        }

        public string CounterParty
        {
            get
            {
                return m_Payment.CounterParty;
            }

            set
            {
                m_Payment.CounterParty = value;
            }
        }

        public List<string> Memos
        {
            get
            {
                return m_Payment.Memos;
            }

            set
            {
                m_Payment.Memos = value;
            }
        }
        #endregion

        #region numbers
        public double Fee
        {
            get
            {
                return m_Payment.Fee;
            }

            set
            {
                m_Payment.Fee = value;
            }
        }

        public Amount Amount
        {
            get
            {
                return m_Payment.Amount;
            }

            set
            {
                m_Payment.Amount = value;
            }
        }
        #endregion

        public List<Effects> Effects
        {
            get
            {
                return m_Payment.Effects;
            }

            set
            {
                m_Payment.Effects = value;
            }
        }
        #endregion
    }

    public class PaymentsResponse : BaseResponse
    {
        #region fields
        private List<Payment> m_Payments = new List<Payment>();
        #endregion

        #region properties
        public List<Payment> Payments
        {
            get
            {
                return m_Payments;
            }
        }
        #endregion
    }

    public class SetPaymentResponse : BaseResponse
    {
        #region fields
        private string m_Client_ID;
        private Payment m_Payment = new Payment();
        #endregion

        #region properties
        public string Client_ID
        {
            set
            {
                m_Client_ID = value;
            }

            get
            {
                return m_Client_ID;
            }
        }

        public string Hash
        {
            set
            {
                m_Payment.Hash = value;
            }

            get
            {
                return m_Payment.Hash;
            }
        }

        public string Result
        {
            set
            {
                m_Payment.Result = value;
            }

            get
            {
                return m_Payment.Result;
            }
        }

        public int Date
        {
            set
            {
                m_Payment.Date = value;
            }

            get
            {
                return m_Payment.Date;
            }
        }

        public double Fee
        {
            set
            {
                m_Payment.Fee = value;
            }

            get
            {
                return m_Payment.Fee;
            }
        }
        #endregion
    }

    public class PaymentChoicesResponse : BaseResponse
    {
        #region fields
        private List<PaymentChoice> m_Choices = new List<PaymentChoice>();
        #endregion

        #region properties
        public List<PaymentChoice> Choices
        {
            get
            {
                return m_Choices;
            }
        }
        #endregion
    }
    #endregion

    #region order responses
    public class OrderResponse : BaseResponse
    {
        #region fields
        private Order m_Order;
        #endregion

        #region properties
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
        #endregion
    }

    public class OrdersResponse : BaseResponse
    {
        #region fields
        private List<Order> m_Orders = new List<Order>();
        #endregion

        #region properties
        public List<Order> Orders
        {
            get
            {
                return m_Orders;
            }
        }
        #endregion
    }

    public class OrderBookResponse : BaseResponse
    {
        #region fields
        private string m_Pair;
        private List<OrderBook> m_Asks = new List<OrderBook>();
        private List<OrderBook> m_Bids = new List<OrderBook>();
        #endregion

        #region properties
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

        public List<OrderBook> Asks
        {
            get
            {
                return m_Asks;
            }
        }

        public List<OrderBook> Bids
        {
            get
            {
                return m_Bids;
            }
        }
        #endregion
    }

    public class SetOrderResponse : BaseResponse
    {
        #region fields
        private string m_Hash;
        private string m_Result;
        private double m_Fee;
        private int m_Sequence;
        #endregion

        #region properties
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

        public int Sequence
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
    #endregion

    #region transaction responses
    public class TransactionBaseResponse : BaseResponse
    {
        #region fields
        private Marker m_Marker;
        #endregion

        #region properties
        public Marker Marker
        {
            get
            {
                return m_Marker;
            }

            set
            {
                m_Marker = value;
            }
        }
        #endregion
    }

    public class TranactionResponse : TransactionBaseResponse
    {
        #region fields
        private Transaction m_Transaction;
        #endregion

        #region properties
        public Transaction Transaction
        {
            get
            {
                return m_Transaction;
            }

            set
            {
                m_Transaction = value;
            }
        }
        #endregion
    }

    public class TransactionsResponse : TransactionBaseResponse
    {
        #region fields
        private List<Transaction> m_Transactions = new List<Transaction>();
        #endregion

        #region properties
        public List<Transaction> Transactions
        {
            get
            {
                return m_Transactions;
            }
        }
        #endregion
    }
    #endregion

    public class DealResponse : BaseResponse
    {
        #region fields        
        private string m_Result;
        private string m_Code;
        private string m_Message;
        private string m_Tx_blob;
        private DealRequest m_Tx_json;
        #endregion

        #region properties        
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

        public string Code
        {
            get
            {
                return m_Code;
            }

            set
            {
                m_Code = value;
            }
        }

        public string Message
        {
            get
            {
                return m_Message;
            }

            set
            {
                m_Message = value;
            }
        }

        public string Tx_blob
        {
            get
            {
                return m_Tx_blob;
            }

            set
            {
                m_Tx_blob = value;
            }
        }

        public DealRequest Tx_json
        {
            get
            {
                return m_Tx_json;
            }

            set
            {
                m_Tx_json = value;
            }
        }
        #endregion
    }

    public class DeployResponse : DealResponse
    {
        #region fields
        private string m_ContractState;
        #endregion

        #region properties
        public string ContractState
        {
            get
            {
                return m_ContractState;
            }

            set
            {
                m_ContractState = value;
            }
        }
        #endregion
    }

    public class CallResponse : DealResponse
    {
        #region fields
        private ContractState m_ContractState;
        #endregion

        #region properties
        public ContractState ContractState
        {
            get
            {
                return m_ContractState;
            }

            set
            {
                m_ContractState = value;
            }
        }
        #endregion
    }
}
