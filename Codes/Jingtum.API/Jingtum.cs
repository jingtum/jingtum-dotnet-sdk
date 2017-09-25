using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Jingtum.API
{
    public abstract class Jingtum
    {
        public const string SIGN_HASH_STRING = "Jingtum2016";  //used in building signature string
        public const string CURRENCY_SWT = "SWT";
        public const string CURRENCY_CNY = "CNY";
        public const string CURRENCY_USD = "USD";
        public const string CURRENCY_EUR = "EUR";
        public const string CURRENCY_JPY = "JPY";
        public const string CURRENCY_VCC = "VCC";
        public const string CURRENCY_SPC = "SPC";

        #region methods
        ///// <summary>
        ///// Get currency USD.
        ///// </summary>
        ///// <returns>Return currency USD.</returns>
        //public static string getCurrencyUSD()
        //{
        //    return CURRENCY_USD;
        //}

        ///// <summary>
        ///// Get currency EUR.
        ///// </summary>
        ///// <returns>Return currency EUR.</returns>
        //public static string getCurrencyEUR()
        //{
        //    return CURRENCY_EUR;
        //}

        ///// <summary>
        ///// Get currency JPY.
        ///// </summary>
        ///// <returns>Return currency JPY.</returns>
        //public static string getCurrencyJPY()
        //{
        //    return CURRENCY_JPY;
        //}

        ///// <summary>
        ///// Get currency JPY.
        ///// </summary>
        ///// <returns>Return currency JPY.</returns>
        //public static string getCurrencyCNY()
        //{
        //    return CURRENCY_CNY;
        //}

        ///// <summary>
        ///// Get currency JPY.
        ///// </summary>
        ///// <returns>Return currency JPY.</returns>
        //public static string getCurrencySWT()
        //{
        //    return CURRENCY_SWT;
        //}

        ///// <summary>
        ///// Get currency VCC.
        ///// </summary>
        ///// <returns>Return currency VCC.</returns>
        //public static string getCurrencyVCC()
        //{
        //    return CURRENCY_VCC;
        //}

        ///// <summary>
        ///// Get currency SPC.
        ///// </summary>
        ///// <returns>Return currency SPC.</returns>
        //public static string getCurrencySPC()
        //{
        //    return CURRENCY_SPC;
        //}

        ///// <summary>
        ///// Get sign string used in authentic check.
        ///// </summary>
        ///// <returns>Return string used in sign functionality.</returns>
        //public static string getSignString()
        //{
        //    return SIGN_HASH_STRING;
        //}
        #endregion
    }

    public abstract class JingtumMessage
    {
        public const string INVALID_JINGTUM_ADDRESS = "Invalid Jingtum address!";
        public const string INVALID_ORDER_NUMBER = "Order number cannot be empty!";
        public const string INVALID_PAGE_INFO = "Invalid paging option!";
        public const string INVALID_CURRENCY = "Invalid currency!";
        public const string INVALID_JINGTUM_CURRENCY = "Invalid Jingtum Tum code!";
        public const string INVALID_VALUE = "Invalid value!";
        public const string INVALID_LIMIT = "Invalid limit!";
        public const string SPECIFY_ORDER_TYPE = "Please specify an order type!";
        public const string ACCOUNT_NOT_FOUND = "Account not found.";
        public const string INACTIVATED_ACCOUNT = "Inactivated Account;";
        public const string INVALID_SECRET = "Invalid Jingtum account secret!";
        public const string INVALID_JINGTUM_AMOUNT = "Invalid JingtumAmount! Please make sure Currency and issuer are all valid.";
        public const string INVALID_TRUST_LINE = "Invalid trust line! Please make sure Currency and Counterparty are all valid.";
        public const string INVALID_JINGTUM_ADDRESS_OR_SECRET = "Invalid address or secret!";
        public const string INVALID_ID = "Invalid ID!";
        public const string GATEWAY_NOT_INITIALIZED = "Gateway address is not set";
        public const string CURRENCY_OTHER_THAN_SWT = "Please set currency other than ";
        public const string INVALID_RELATION_TYPE = "Invalid relation type!";
        public const string NOT_NULL_MESSAGE_HANDLER = "Message handler cannot be null!";
        public const string NO_CONNECTION_AVAIABLE = "Please set up connection first!";
        public const string ERROR_MESSAGE = "Error message: ";
        public const string ERROR_CODE = "Error code: ";
        public const string ERROR_INPUT = "Input value cannot be empty!";
        public const string ERROR_CLIENT_ID = "Client Id cannot be empty!";
        public const string EMPTY_TOKEN = "FinGate custom token is not set!";
        public const string EMPTY_KEY = "FinGate custom key is not set!";
        public const string UNRECOGNIZED_HTTP_METHOD = "Unrecognized HTTP method %s. ";
        public const string SERVER_ERROR = "IOException during API request to Jingtum (%s): %s "
                                    + "Please check your internet connection and try again. If this problem persists,"
                                    + "you should check Jingtum's service status at https://api.jingtum.com,"
                                    + " or let us know at support@jingtum.com.";
        public const string UNKNOWN_MODE = "FinGate mode is unknown!";
        public const string INVALID_TUM_PAIR = "Invalid Tum pair!";
    }
    
    public abstract class JingtumObject
    {
        //public virtual string JingtumTypeString
        //{
        //    get
        //    {
        //        return this.GetType().Name;
        //    }
        //}

        public virtual string APIMethodName
        {
            get
            {
                return this.GetType().Name + "s";
            }
        }

        public virtual List<string> ToStringList()
        {
            PropertyInfo[] properties = this.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            List<string> lines = new List<string>();

            foreach (PropertyInfo property in properties)
            {
                string line = property.Name + ": " + property.GetValue(this);
                lines.Add(line);
            }

            return lines;
        }
    }
}
