﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Cache;
using System.IO;

namespace Jingtum.API.Net
{
    class APIServer
    {
        #region consts
        public const string SIGN_BACK_SLASH = "/";
        public const string SIGN_QUESTION_MARK = "?";
        public const string SIGN_AND = "&";

        public const string URL_SERVER_ADDRESS = "https://api.jingtum.com/";
        public const string URL_SERVER_VERSION = "v2";
        //https://api.jingtum.com/v2/wallet/
        public const string URL_SERVER_ADDRESS_WALLET = 
            URL_SERVER_ADDRESS 
            + URL_SERVER_VERSION
            + SIGN_BACK_SLASH
            + "wallet"
            + SIGN_BACK_SLASH;
        //https://api.jingtum.com/v2/accounts/
        public const string URL_SERVER_ADDRESS_ACCOUNT = 
            URL_SERVER_ADDRESS 
            + URL_SERVER_VERSION
            + SIGN_BACK_SLASH
            + "accounts"
            + SIGN_BACK_SLASH;
        
        public const string Request_Url_ResultsPerPage = "results_per_page";
        //public const string Request_Url_Marker = "marker={ledger:" + ledger + ",seq:" + seq + "};
        //public const string Request_Url_Marker = "marker={ledger:{0},seq:{1}}";

        

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

        #region methods
        public string GetResponse(string url)
        {
            m_Request = WebRequest.Create(url);

            m_Request.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore); ;
            WebResponse response = m_Request.GetResponse();

            if (response is HttpWebResponse && ((HttpWebResponse)response).StatusDescription != "OK")
            {
            }

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

        public string Request(string method, string url, string parameters)
        {
            m_Request = WebRequest.Create(url);

            m_Request.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore); 
            m_Request.ContentType = "application/json";
            //m_Request.Accept = "application/json";
            m_Request.Method = method;
            m_Request.Timeout = 30000;

            if (m_Request.Method == "POST" || m_Request.Method == "DELETE")
            {
                byte[] btBodys = Encoding.UTF8.GetBytes(parameters);
                m_Request.ContentLength = btBodys.Length;
                //httpWebRequest.KeepAlive = false;
                //m_Request.ServicePoint.Expect100Continue = false;
                m_Request.GetRequestStream().Write(btBodys, 0, btBodys.Length);
            }

            WebResponse response = m_Request.GetResponse();

            if (response is HttpWebResponse && ((HttpWebResponse)response).StatusDescription != "OK")
            {
            }

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

        public string FormatURL(string serverURL, string address, string type, string parameters)
        {
            return serverURL 
                + (!(serverURL.EndsWith(SIGN_BACK_SLASH)) ? SIGN_BACK_SLASH : string.Empty) 
                + address 
                + SIGN_BACK_SLASH 
                + type 
                //+ SIGN_QUESTION_MARK 
                + parameters;
        }

        public string FormatURL(string address, string type, string parameters)
        {
            return URL_SERVER_ADDRESS_ACCOUNT 
                + address 
                + SIGN_BACK_SLASH 
                + type 
                //+ SIGN_QUESTION_MARK 
                + parameters;
        }

        public string FormatURL(string address, string type)
        {
            return URL_SERVER_ADDRESS_ACCOUNT 
                + address 
                + SIGN_BACK_SLASH 
                + type;
        }
        #endregion
    }
}
