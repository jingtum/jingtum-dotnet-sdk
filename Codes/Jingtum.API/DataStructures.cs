using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jingtum.API
{
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
        private int m_Sequence;
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
    
    public class Amount : JingtumObject
    {
        #region sample
        //    "paid": {
        //    "value": "0.01269999999999527",
        //    "currency": "CNY",
        //    "issuer": "jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o9or"
        //    },
        #endregion

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
        private Amount m_Amount;

        private string m_CounterParty;
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

        public string CounterParty
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

        public Amount Amount
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

    public class PaymentChoice : JingtumObject
    {
        #region fields
        private Amount m_Choice;
        private string m_Key;
        #endregion

        #region properties
        public Amount Choice
        {
            get
            {
                return m_Choice;
            }

            set
            {
                m_Choice = value;
            }
        }

        public string Key
        {
            get
            {
                return m_Key;
            }

            set
            {
                m_Key = value;
            }
        }

        //public double ChoiceAsNumber
        //{
        //    get
        //    {
        //        double value;
        //        return double.TryParse(Choice.ToString(), out value) ? value : double.NaN;
        //    }
        //}

        //public Amount ChoiceAsAmount
        //{
        //    get
        //    {
        //        try 
        //        {
        //            return Newtonsoft.Json.JsonConvert.DeserializeObject<Amount>(Choice.ToString());
        //        }
        //        catch
        //        {
        //            return null;
        //        }
        //    }
        //}        
        #endregion

        public override string APIMethodName()
        {
            return "choices";
        }
    }

    public class OrderBook : JingtumObject
    {
        #region fields
        private double m_Price;
        private double m_Funded;
        private string m_Order_Maker;
        private int m_Sequence;
        private bool m_Passive;
        private bool m_Sell;
        #endregion

        #region properties
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

        public double Funded
        {
            get
            {
                return m_Funded;
            }

            set
            {
                m_Funded = value;
            }
        }

        public string Order_Maker
        {
            get
            {
                return m_Order_Maker;
            }

            set
            {
                m_Order_Maker = value;
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

        public bool Passive
        {
            get
            {
                return m_Passive;
            }

            set
            {
                m_Passive = value;
            }
        }

        public bool Sell
        {
            get
            {
                return m_Sell;
            }

            set
            {
                m_Sell = value;
            }
        }        
        #endregion

        public override string APIMethodName()
        {
            return "order_book";
        }
    }

    public class Transaction : JingtumObject
    {
        #region sample
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
        #endregion        

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

    public class Marker : JingtumObject
    {
        #region sample
        //{
        //"success": true,
        //"status_code": "0",
        //"marker": {
        //"ledger": 7559633,
        //"seq": 0
        //},
        //"transactions": [
        //{
        //    "date": 1505439030,
        //    "hash": "1178604276A08D6C66CE9133F2064E1546431FC935961F9CD060E3F146520EF3",
        //    "type": "offernew",
        //    "fee": "0.00001",
        //    "result": "tesSUCCESS",
        //    "memos": [],
        //    "offertype": "buy",
        //    "seq": 441,
        //    "effects": [
        //    {
        //        "effect": "offer_created",
        //        "type": "buy",
        //        "seq": 441,
        //        "price": 0.00009999999999999999,
        //        "pair": "SWT/CNY:jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o9or",
        //        "amount": "511231000"
        //    }
        //    ],
        //    "pair": "SWT/CNY:jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o9or",
        //    "amount": "511231000",
        //    "price": 0.00009999999999999999
        //}
        //]
        //}
        #endregion

        #region fields
        private int m_Ledger;
        private int m_Seq;
        #endregion

        #region properties
        public int Ledger
        {
            get
            {
                return m_Ledger;
            }

            set
            {
                m_Ledger = value;
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
        #endregion
    }

    //public class EngineResult : JingtumObject
    //{
    //    #region fields
    //    private string m_Result;
    //    private string m_Code;
    //    private string m_Message;
    //    #endregion

    //    #region properties
    //    public string Result
    //    {
    //        get
    //        {
    //            return m_Result;
    //        }

    //        set
    //        {
    //            m_Result = value;
    //        }
    //    }

    //    public string Code
    //    {
    //        get
    //        {
    //            return m_Code;
    //        }

    //        set
    //        {
    //            m_Code = value;
    //        }
    //    }

    //    public string Message
    //    {
    //        get
    //        {
    //            return m_Message;
    //        }

    //        set
    //        {
    //            m_Message = value;
    //        }
    //    }
    //    #endregion
    //}

    public class DealRequest : JingtumObject
    {
        #region fields
        #region common
        private string m_Account;
        private double m_Fee;
        private string m_Flags;
        private string m_Sequence;
        private string m_SigningPubKey;
        private string m_TransactionType;
        private string m_TxnSignature;
        private string m_Hash;
        #endregion

        #region special
        private string m_Method;
        private int m_Timestamp;
        private double m_Amount;
        private string m_Payload;
        private List<Arg> m_Args;
        private string m_ContractMethod;
        private string m_Destination;
        #endregion
        #endregion

        #region properties
        #region common
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

        public string Flags
        {
            get
            {
                return m_Flags;
            }

            set
            {
                m_Flags = value;
            }
        }

        public string Sequence
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

        public string SigningPubKey
        {
            get
            {
                return m_SigningPubKey;
            }

            set
            {
                m_SigningPubKey = value;
            }
        }

        public string TransactionType
        {
            get
            {
                return m_TransactionType;
            }

            set
            {
                m_TransactionType = value;
            }
        }

        public string TxnSignature
        {
            get
            {
                return m_TxnSignature;
            }

            set
            {
                m_TxnSignature = value;
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

        #region special
        public string Method
        {
            get
            {
                return m_Method;
            }

            set
            {
                m_Method = value;
            }
        }

        public int Timestamp
        {
            get
            {
                return m_Timestamp;
            }

            set
            {
                m_Timestamp = value;
            }
        }

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

        public string Payload
        {
            get
            {
                return m_Payload;
            }

            set
            {
                m_Payload = value;
            }
        }

        public List<Arg> Args
        {
            get
            {
                return m_Args;
            }

            set
            {
                m_Args = value;
            }
        }

        public string ContractMethod
        {
            get
            {
                return m_ContractMethod;
            }

            set
            {
                m_ContractMethod = value;
            }
        }

        public string Destination
        {
            get
            {
                return m_Destination;
            }

            set
            {
                m_Destination = value;
            }
        } 
        #endregion
        #endregion
    }

    public class Arg : JingtumObject
    {
        #region fields
        private string m_Parameter;
        #endregion

        #region properties
        public string Parameter
        {
            get
            {
                return m_Parameter;
            }

            set
            {
                m_Parameter = value;
            }
        }        
        #endregion
    }

    public class ContractState : JingtumObject
    {
        #region fields
        private string m_Account;
        private double m_Balance;
        private string m_Flags;
        private string m_LedgerEntryType;
        private int m_OwnerCount;
        private string m_PreviousTxnID;
        private int m_PreviousTxnLgrSeq;
        private int m_Sequence;
        private string m_Index;
        #endregion

        #region properties
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

        public double Balance
        {
            get
            {
                return m_Balance;
            }

            set
            {
                m_Balance = value;
            }
        }

        public string Flags
        {
            get
            {
                return m_Flags;
            }

            set
            {
                m_Flags = value;
            }
        }

        public string LedgerEntryType
        {
            get
            {
                return m_LedgerEntryType;
            }

            set
            {
                m_LedgerEntryType = value;
            }
        }

        public int OwnerCount
        {
            get
            {
                return m_OwnerCount;
            }

            set
            {
                m_OwnerCount = value;
            }
        }

        public string PreviousTxnID
        {
            get
            {
                return m_PreviousTxnID;
            }

            set
            {
                m_PreviousTxnID = value;
            }
        }

        public int PreviousTxnLgrSeq
        {
            get
            {
                return m_PreviousTxnLgrSeq;
            }

            set
            {
                m_PreviousTxnLgrSeq = value;
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

        public string Index
        {
            get
            {
                return m_Index;
            }

            set
            {
                m_Index = value;
            }
        }
        #endregion
    }
}
