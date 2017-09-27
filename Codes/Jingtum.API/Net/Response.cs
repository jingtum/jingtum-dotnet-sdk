using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jingtum.API.Net
{
    class Response
    {
        #region fields
        private bool m_Success;
        private string m_StatusCode;
        private Marker m_Marker;
        private Wallet m_Wallet;
        private List<Balance> m_Balances = new List<Balance>();
        private Order m_Order;
        private List<Order> m_Orders = new List<Order>();
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

        public List<Balance> Balances
        {
            get
            {
                return m_Balances;
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

        public List<Order> Orders
        {
            get
            {
                return m_Orders;
            }
        }
        #endregion
    }

    public class Marker
    {
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

        private int m_Ledger;
        private int m_Seq;

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

        //public static Marker Parse(string json)
        //{
        //    Marker marker = JsonConvert.DeserializeObject<Marker>(json);

        //    return marker;
        //}

        //public static Marker Parse(List<string> lines)
        //{
        //    Marker marker = new Marker();

        //    marker.Ledger = Marker.ParseLedger(lines[1]);
        //    marker.Seq = Marker.ParseSeq(lines[2]);

        //    return marker;
        //}

        //private static int ParseLedger(string line)
        //{
        //    Regex regex = new Regex("\"ledger\": (?<ledger>[\\s\\S]*?),", RegexOptions.IgnoreCase);
        //    Match match = regex.Match(line);
        //    if (match.Success)
        //    {
        //        return int.Parse(match.Groups["ledger"].Value);
        //    }
        //    else
        //    {
        //        return -1;
        //    }
        //}

        //private static int ParseSeq(string line)
        //{
        //    //"    \"seq\": 0"
        //    string seq = line.Remove(0, 11);
        //    return int.Parse(seq);
        //}
    }
}
