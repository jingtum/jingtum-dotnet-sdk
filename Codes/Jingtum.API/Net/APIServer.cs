using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Cache;
using System.IO;
using Newtonsoft.Json;

namespace Jingtum.API.Net
{
    class APIServer
    {
        #region consts
        public const string SIGN_BACKSLASH = "/";
        public const string SIGN_QUESTION = "?";
        public const string SIGN_AND = "&";
        public const string SIGN_ADD = "+";
        public const string SIGN_EQUAL = "=";

        public const string URL_SERVER_ADDRESS = "https://api.jingtum.com/";
        public const string URL_SERVER_VERSION = "v2";

        //https://api.jingtum.com/v2/
        public const string URL_SERVER_ADDRESS_VERSION =
            URL_SERVER_ADDRESS
            + URL_SERVER_VERSION
            + SIGN_BACKSLASH;
        
        //https://api.jingtum.com/v2/accounts/
        public const string URL_SERVER_ADDRESS_ACCOUNT =
            URL_SERVER_ADDRESS_VERSION
            + "accounts"
            + SIGN_BACKSLASH;

        //public const string PARAMETER_RESULTS_CURRENCY = "currency";
        //public const string PARAMETER_RESULTS_ISSUER = "issuer";
        //public const string PARAMETER_RESULTS_PER_PAGE = "results_per_page";
        //public const string PARAMETER_PAGE = "page";
        //public const string Request_Url_Marker = "marker={ledger:" + ledger + ",seq:" + seq + "};
        //public const string Request_Url_Marker = "marker={ledger:{0},seq:{1}}";

        public const string REQUEST_METHOD_GET = "GET";
        public const string REQUEST_METHOD_POST = "POST";
        public const string REQUEST_METHOD_DELETE = "DELETE";
        public const string REQUEST_METHOD_PUT = "PUT";
        public const string REQUEST_METHOD_HEAD = "HEAD";

        //public const string Response_NoMorePayments = "{\n  \"success\": true,\n  \"payments\": []\n}";
        //public const string Response_NoMoreTransactions = "{\n  \"success\": true,\n  \"transactions\": []\n}";
        #endregion

        #region fields
        private WebRequest m_Request = null;
        #endregion

        #region properties
        public WebRequest WebRequest
        {
            get
            {
                return this.m_Request;
            }
        }
        #endregion

        #region request methods
        public T Request_Get<T>(string url)
        {
            return this.ConvertResponse<T>(this.Request(APIServer.REQUEST_METHOD_GET, url, string.Empty));
        }

        public T Request_Post<T>(string url, string parameters)
        {
            return this.ConvertResponse<T>(this.Request(APIServer.REQUEST_METHOD_POST, url, parameters));
        }

        public T Request_Delete<T>(string url, string parameters)
        {
            return this.ConvertResponse<T>(this.Request(APIServer.REQUEST_METHOD_DELETE, url, parameters));
        }

        public T ConvertResponse<T>(string responseString)
        {
            BaseResponse response = JsonConvert.DeserializeObject<BaseResponse>(responseString);
            if (response.Success)
            {
                return JsonConvert.DeserializeObject<T>(responseString);
            }
            else
            {
                ErrorResponse error = JsonConvert.DeserializeObject<ErrorResponse>(responseString);
                throw new APIException(error.Error_Type, error.Error, error.Message);
            }
        }

        public string Request(string method, string url, string parameters)
        {
            m_Request = WebRequest.Create(url);

            m_Request.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore); 
            m_Request.ContentType = "application/json";
            m_Request.Method = method;
            m_Request.Timeout = 30000;

            if (m_Request.Method == REQUEST_METHOD_POST || m_Request.Method == REQUEST_METHOD_DELETE)
            {
                byte[] btBodys = Encoding.UTF8.GetBytes(parameters);
                m_Request.ContentLength = btBodys.Length;
                m_Request.GetRequestStream().Write(btBodys, 0, btBodys.Length);
            }

            WebResponse response = m_Request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            string responseFromServer = reader.ReadToEnd();

            // Cleanup the streams and the response.            
            reader.Close();
            dataStream.Close();
            response.Close();
            m_Request.Abort();

            return responseFromServer;
        }
        #endregion

        #region format url methods
        public string FormatURL(string serverURL, string address, string type, string parameters)
        {
            return serverURL
                + (!(serverURL.EndsWith(SIGN_BACKSLASH)) ? SIGN_BACKSLASH : string.Empty) 
                + address
                + SIGN_BACKSLASH 
                + type 
                + parameters;
        }

        public string FormatURL(string address, string type, string parameters)
        {
            return FormatURL(URL_SERVER_ADDRESS_ACCOUNT, address, type, parameters);
        }

        public string FormatURL(string address, string type)
        {
            return FormatURL(address, type, string.Empty);
        }

        private static string FormatParameter(string existingParameter, string parameterName, string value)
        {
            if(value != string.Empty)
            {
                return FormatParameter(existingParameter, parameterName + APIServer.SIGN_EQUAL + value);
            }

            return existingParameter;
        }

        private static string FormatParameter(string existingParameter, string newParameter)
        {
            if (newParameter != string.Empty)
            {
                if (existingParameter == string.Empty)//mean this is the first parameter, need start from "?".
                {
                    existingParameter += APIServer.SIGN_QUESTION;
                }
                else//not the  first parameter, need start from "&".
                {
                    existingParameter += APIServer.SIGN_AND;
                }

                existingParameter += newParameter;
            }

            return existingParameter;
        }

        public static string FormatParameter_Currency(string existingParameter, string currency)
        {
            if (currency != string.Empty)
            {
                if (Utility.IsValidCurrency(currency))
                {
                    existingParameter = APIServer.FormatParameter(existingParameter, "currency", currency);
                }
                else
                {
                    throw new InvalidParameterException(JingtumMessage.INVALID_CURRENCY, currency);
                }
            }

            return existingParameter;
        }

        public static string FormatParameter_Issuer(string existingParameter, string issuer)
        {
            if (issuer != string.Empty)
            {
                if (Utility.IsValidAddress(issuer))
                {
                    existingParameter = APIServer.FormatParameter(existingParameter, "issuer", issuer);
                }
                else
                {
                    throw new InvalidParameterException(JingtumMessage.INVALID_JINGTUM_ADDRESS, issuer);
                }
            }

            return existingParameter;
        }

        public static string FormatParameter_Currency_CounterParty(string currency, string counterParty)
        {
            if (Utility.IsValidCurrency(currency))
            {
                if (counterParty == string.Empty)
                {
                    return currency;
                }
                else if (Utility.IsValidAddress(counterParty))
                {
                    return currency + Net.APIServer.SIGN_ADD + counterParty;
                }
                else
                {
                    throw new InvalidParameterException(JingtumMessage.INVALID_JINGTUM_ADDRESS, counterParty);
                }                
            }
            else
            {
                throw new InvalidParameterException(JingtumMessage.INVALID_CURRENCY, currency);
            }
        }

        public static string FormatParameter_Result_Per_Page(string existingParameter, int results_per_page, int page)
        {
            if (results_per_page > 0)
            {
                existingParameter = APIServer.FormatParameter(existingParameter, "results_per_page", results_per_page.ToString());
            }

            if (page > 0)
            {
                existingParameter = APIServer.FormatParameter(existingParameter, "page", page.ToString());
            }

            return existingParameter;
        }

        public static string FormatParameter_Hash(string hash)
        {
            return Net.APIServer.SIGN_BACKSLASH + hash;
        }

        public static string FormatParameter_Marker(string existingParameter, int ledger, int seq)
        {
            if (ledger > 0)
            {
                string parameter = "marker={ledger:" + ledger + ",seq:" + seq + "}";
                existingParameter = APIServer.FormatParameter(existingParameter, parameter);
            }

            return existingParameter;
        }
        #endregion
    }
}
