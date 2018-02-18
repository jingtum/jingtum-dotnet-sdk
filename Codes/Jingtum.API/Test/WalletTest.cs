using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jingtum.API.Core;
using NUnit.Framework;

namespace Jingtum.API.Test
{
    [TestFixture]
    public class Test_Wallet_Url
    {
        Wallet m_Wallet = new Wallet(@"jP189vbfqsByaxKY1UEdAtRXTwaBBkDgVJ", @"saESGGsyFnTEW9BPWQ6bCfqwNgESU");
        Wallet m_Wallet_WithoutSecret = new Wallet(@"jfdLqEWhfYje92gEaWixVWsYKjK5C6bMoi");

        string m_ValidCurrency = Utility.CURRENCY_CNY;
        string m_InvalidCurrency = "NES4";
        string m_ValidIssuer = "jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o9or";
        string m_InvalidIssuer = "jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o8or";
        string m_Hash = "E9C27C31B4F1C9C502713FA636D64BEFCF678CA4724D766AD46D88319347C52E";

        #region new
        [Test]
        public void TestUrl_WalletNew()
        {
            Assert.AreEqual(@"https://api.jingtum.com/v2/wallet/new", Wallet.GetUrl_WalletNew());
        }
        #endregion

        #region balances
        [Test]
        public void TestUrl_WalletBalances()
        {            
            //test balances urls.
            //no options.
            Assert.AreEqual(@"https://api.jingtum.com/v2/accounts/jP189vbfqsByaxKY1UEdAtRXTwaBBkDgVJ/Balances",
                m_Wallet.GetUrl_Balances());

            //with currecy and issuer.
            Assert.AreEqual(@"https://api.jingtum.com/v2/accounts/jP189vbfqsByaxKY1UEdAtRXTwaBBkDgVJ/Balances?currency=CNY&issuer=jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o9or",
                m_Wallet.GetUrl_Balances(m_ValidCurrency, m_ValidIssuer));

            //with only currency.
            Assert.AreEqual(@"https://api.jingtum.com/v2/accounts/jP189vbfqsByaxKY1UEdAtRXTwaBBkDgVJ/Balances?currency=CNY",
                m_Wallet.GetUrl_Balances(m_ValidCurrency, string.Empty));

            //with only issuer.
            Assert.AreEqual(@"https://api.jingtum.com/v2/accounts/jP189vbfqsByaxKY1UEdAtRXTwaBBkDgVJ/Balances?issuer=jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o9or",
                m_Wallet.GetUrl_Balances(string.Empty, m_ValidIssuer));

            //with 2 empty options.
            Assert.AreEqual(@"https://api.jingtum.com/v2/accounts/jP189vbfqsByaxKY1UEdAtRXTwaBBkDgVJ/Balances",
                m_Wallet.GetUrl_Balances(string.Empty, string.Empty));

            //with invalid currency.      
            InvalidParameterException expectException = null;
            try
            {
                m_Wallet.GetUrl_Balances(m_InvalidCurrency, m_ValidIssuer);
            }
            catch (InvalidParameterException ex)
            {
                expectException = ex;                
            }
            Assert.AreNotEqual(null, expectException);
            Assert.AreEqual(JingtumMessage.INVALID_CURRENCY, expectException.Message);
            Assert.AreEqual(m_InvalidCurrency, expectException.Param);


            //with invalid issuer.
            expectException = null;
            try
            {
                m_Wallet.GetUrl_Balances(m_ValidCurrency, m_InvalidIssuer);
            }
            catch (InvalidParameterException ex)
            {
                expectException = ex;                
            }
            Assert.AreNotEqual(null, expectException);
            Assert.AreEqual(JingtumMessage.INVALID_JINGTUM_ADDRESS, expectException.Message);
            Assert.AreEqual(m_InvalidIssuer, expectException.Param);
        }
        #endregion

        #region payments
        #region set payments
        [Test]
        public void TestUrl_WalletSetPayment()
        {
            //test set payment url
            string expectUrl = "https://api.jingtum.com/v2/accounts/jP189vbfqsByaxKY1UEdAtRXTwaBBkDgVJ/Payments";
            Assert.AreEqual(expectUrl, this.m_Wallet.GetUrl_Payments());

            //test set payment parameter
            Payment payment = new Payment();
            Amount amount = new Amount();
            amount.Value = 100000;
            amount.Currency = m_ValidCurrency;
            amount.Issuer = m_ValidIssuer;
            payment.Amount = amount;

            string destinationAddress = m_Wallet_WithoutSecret.Address;
            string choiceHash = "f53b09afcf9e1758a7b647f2f738c86426cabfc1";

            List<string> memos = new List<string>();
            memos.Add("Test: " + DateTime.Now.ToString("o"));  
     
            string clientID = m_Wallet.GenerateClientID();
            string expectParameter = "{\"secret\":\"saESGGsyFnTEW9BPWQ6bCfqwNgESU\",\"client_id\":\"" 
                + clientID
                + "\",\"payment\":{\"source\":\"jP189vbfqsByaxKY1UEdAtRXTwaBBkDgVJ\",\"destination\":\"jfdLqEWhfYje92gEaWixVWsYKjK5C6bMoi\","
                + "\"amount\":{\"value\":\"100000\",\"currency\":\"CNY\",\"issuer\":\"jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o9or\"},"
                + "\"choice\":\"f53b09afcf9e1758a7b647f2f738c86426cabfc1\",\"memos\":[\""
                + memos[0]
                + "\"]}}";

            string actualParameter = this.m_Wallet.GetParameters_SetPayment(payment, destinationAddress, choiceHash, memos);

            List<string> expectParameters = SplitSetPaymentParameter(expectParameter);
            List<string> actualParameters = SplitSetPaymentParameter(actualParameter);
            Assert.AreEqual(expectParameters[0], actualParameters[0]);
            Assert.AreEqual(expectParameters[1], actualParameters[1]);            
        }

        private List<string> SplitSetPaymentParameter(string parameter)
        {
            const string split1 = "\"client_id\"";
            int index = parameter.IndexOf(split1);
            string expectParameter1 = parameter.Substring(0, index + split1.Length);

            const string split2 = "\",\"payment\":";
            index = parameter.IndexOf(split2);
            string expectParameter2 = parameter.Substring(index, parameter.Length - index);
            
            List<string> result = new List<string>();
            result.Add(expectParameter1);
            result.Add(expectParameter2);

            return result;
        }
        #endregion

        #region get payments
        [Test]
        public void TestUrl_WalletPayment()
        {
            //test payment urls.
            Assert.AreEqual(@"https://api.jingtum.com/v2/accounts/jP189vbfqsByaxKY1UEdAtRXTwaBBkDgVJ/Payments/E9C27C31B4F1C9C502713FA636D64BEFCF678CA4724D766AD46D88319347C52E",
                m_Wallet.GetUrl_Payment(m_Hash));
        }

        [Test]
        public void TestUrl_WalletPayments()
        {
            int results_per_page = 20;
            int page = 1;
            //test payments urls.
            //no options.
            Assert.AreEqual(@"https://api.jingtum.com/v2/accounts/jP189vbfqsByaxKY1UEdAtRXTwaBBkDgVJ/Payments",
                m_Wallet.GetUrl_Payments());

            //with results_per_page and page.
            Assert.AreEqual(@"https://api.jingtum.com/v2/accounts/jP189vbfqsByaxKY1UEdAtRXTwaBBkDgVJ/Payments?results_per_page=20&page=1",
                m_Wallet.GetUrl_Payments(results_per_page, page));

            //with only results_per_page.
            results_per_page = 20;
            page = 0;
            Assert.AreEqual(@"https://api.jingtum.com/v2/accounts/jP189vbfqsByaxKY1UEdAtRXTwaBBkDgVJ/Payments?results_per_page=20",
                m_Wallet.GetUrl_Payments(results_per_page, page));

            ////with only page.
            results_per_page = 0;
            page = 1;
            Assert.AreEqual(@"https://api.jingtum.com/v2/accounts/jP189vbfqsByaxKY1UEdAtRXTwaBBkDgVJ/Payments?page=1",
                m_Wallet.GetUrl_Payments(results_per_page, page));

            ////with 2 empty options.
            results_per_page = 0;
            page = 0;
            Assert.AreEqual(@"https://api.jingtum.com/v2/accounts/jP189vbfqsByaxKY1UEdAtRXTwaBBkDgVJ/Payments",
                m_Wallet.GetUrl_Payments(results_per_page, page));
        }        
        #endregion

        #region get choices
        [Test]
        public void TestUrl_WalletPaymentChoices()
        {
            Amount amount = new Amount();
            amount.Value = 2;
            amount.Currency = m_ValidCurrency;
            amount.Issuer = m_ValidIssuer;

            //test choice urls.
            Assert.AreEqual(@"https://api.jingtum.com/v2/accounts/jP189vbfqsByaxKY1UEdAtRXTwaBBkDgVJ/Payments/choices/jfdLqEWhfYje92gEaWixVWsYKjK5C6bMoi/2+CNY+jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o9or",
                m_Wallet.GetUrl_PaymentChoices(m_Wallet_WithoutSecret.Address, amount.Value, amount.Currency, amount.Issuer));
        }
        #endregion
        #endregion

        #region orders
        #region get orders
        [Test]
        public void TestUrl_WalletOrder()
        {
            //test order urls.
            Assert.AreEqual(@"https://api.jingtum.com/v2/accounts/jP189vbfqsByaxKY1UEdAtRXTwaBBkDgVJ/Orders/E9C27C31B4F1C9C502713FA636D64BEFCF678CA4724D766AD46D88319347C52E",
                m_Wallet.GetUrl_Order(m_Hash));
        }

        [Test]
        public void TestUrl_WalletOrders()
        {
            int results_per_page = 20;
            int page = 1;
            //test payments urls.
            //no options.
            Assert.AreEqual(@"https://api.jingtum.com/v2/accounts/jP189vbfqsByaxKY1UEdAtRXTwaBBkDgVJ/Orders",
                m_Wallet.GetUrl_Orders());

            //with results_per_page and page.
            Assert.AreEqual(@"https://api.jingtum.com/v2/accounts/jP189vbfqsByaxKY1UEdAtRXTwaBBkDgVJ/Orders?results_per_page=20&page=1",
                m_Wallet.GetUrl_Orders(results_per_page, page));

            //with only results_per_page.
            results_per_page = 20;
            page = 0;
            Assert.AreEqual(@"https://api.jingtum.com/v2/accounts/jP189vbfqsByaxKY1UEdAtRXTwaBBkDgVJ/Orders?results_per_page=20",
                m_Wallet.GetUrl_Orders(results_per_page, page));

            ////with only page.
            results_per_page = 0;
            page = 1;
            Assert.AreEqual(@"https://api.jingtum.com/v2/accounts/jP189vbfqsByaxKY1UEdAtRXTwaBBkDgVJ/Orders?page=1",
                m_Wallet.GetUrl_Orders(results_per_page, page));

            ////with 2 empty options.
            results_per_page = 0;
            page = 0;
            Assert.AreEqual(@"https://api.jingtum.com/v2/accounts/jP189vbfqsByaxKY1UEdAtRXTwaBBkDgVJ/Orders",
                m_Wallet.GetUrl_Orders(results_per_page, page));
        }
        #endregion

        #region set and cancel order
        [Test]
        public void TestUrl_WalletSetOrder()
        {
            //test set order url
            string expectUrl = "https://api.jingtum.com/v2/accounts/jP189vbfqsByaxKY1UEdAtRXTwaBBkDgVJ/Orders";
            Assert.AreEqual(expectUrl, this.m_Wallet.GetUrl_Orders());

            //test set order parameter
            Order order = new Order();
            order.Type = "sell";
            order.Amount = 0.0002;
            order.Price = 10;
            order.Pair = "SWT/CNY:jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o9or";

            string expectParameter = "{\"secret\":\"saESGGsyFnTEW9BPWQ6bCfqwNgESU\",\"order\":{\"type\":\"sell\",\"pair\":\"SWT/CNY:jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o9or\",\"amount\":\"0.0002\",\"price\":10}}";
            Assert.AreEqual(expectParameter,
                this.m_Wallet.GetParameters_SetOrder(order));
        }

        [Test]
        public void TestUrl_WalletCancelOrder()
        {
            //test set order url
            string expectUrl = "https://api.jingtum.com/v2/accounts/jP189vbfqsByaxKY1UEdAtRXTwaBBkDgVJ/Orders/28";
            Assert.AreEqual(expectUrl, this.m_Wallet.GetUrl_CancelOrder(28));

            //test set order parameter
            Order order = new Order();
            order.Type = "sell";
            order.Amount = 0.0002;
            order.Price = 10;
            order.Pair = "SWT/CNY:jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o9or";

            string expectParameter = "{\"secret\":\"saESGGsyFnTEW9BPWQ6bCfqwNgESU\"}";
            Assert.AreEqual(expectParameter, this.m_Wallet.GetParameters_CancelOrder());            
        }
        #endregion

        #region order books
        [Test]
        public void TestUrl_WalletOrderBooks()
        {
            string baseCurrency = Utility.CURRENCY_CNY;
            string baseCounterParty = m_ValidIssuer;
            string counterCurrency = Utility.CURRENCY_SWT;
            string counterCounterParty = m_ValidIssuer;
            int results_per_page = 0;
            int page = 0;

            //test choice urls.
            Assert.AreEqual(@"https://api.jingtum.com/v2/order_book/CNY+jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o9or/SWT+jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o9or",
                m_Wallet.GetUrl_OrderBooks(baseCurrency, baseCounterParty, counterCurrency, counterCounterParty, results_per_page, page));

            //with results_per_page and page.
            results_per_page = 20;
            page = 1;
            Assert.AreEqual(@"https://api.jingtum.com/v2/order_book/CNY+jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o9or/SWT+jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o9or?results_per_page=20&page=1",
                m_Wallet.GetUrl_OrderBooks(baseCurrency, baseCounterParty, counterCurrency, counterCounterParty, results_per_page, page));

            //test bids
            Assert.AreEqual(@"https://api.jingtum.com/v2/order_book/bids/CNY+jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o9or/SWT+jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o9or?results_per_page=20&page=1",
                m_Wallet.GetUrl_OrderBook_Bids(baseCurrency, baseCounterParty, counterCurrency, counterCounterParty, results_per_page, page));

            //test asks
            Assert.AreEqual(@"https://api.jingtum.com/v2/order_book/asks/CNY+jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o9or/SWT+jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o9or?results_per_page=20&page=1",
                m_Wallet.GetUrl_OrderBook_Asks(baseCurrency, baseCounterParty, counterCurrency, counterCounterParty, results_per_page, page));

            //with only results_per_page.
            results_per_page = 20;
            page = 0;
            Assert.AreEqual(@"https://api.jingtum.com/v2/order_book/CNY+jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o9or/SWT+jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o9or?results_per_page=20",
                m_Wallet.GetUrl_OrderBooks(baseCurrency, baseCounterParty, counterCurrency, counterCounterParty, results_per_page, page));

            //with only page.
            results_per_page = 0;
            page = 1;
            Assert.AreEqual(@"https://api.jingtum.com/v2/order_book/CNY+jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o9or/SWT+jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o9or?page=1",
                m_Wallet.GetUrl_OrderBooks(baseCurrency, baseCounterParty, counterCurrency, counterCounterParty, results_per_page, page));

            //with 2 empty options.
            results_per_page = 0;
            page = 0;
            Assert.AreEqual(@"https://api.jingtum.com/v2/order_book/CNY+jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o9or/SWT+jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o9or",
                m_Wallet.GetUrl_OrderBooks(baseCurrency, baseCounterParty, counterCurrency, counterCounterParty, results_per_page, page));

            //with invalid currency.      
            InvalidParameterException expectException = null;
            try
            {
                baseCurrency = m_InvalidCurrency;
                m_Wallet.GetUrl_OrderBooks(baseCurrency, baseCounterParty, counterCurrency, counterCounterParty, results_per_page, page);
            }
            catch (InvalidParameterException ex)
            {
                expectException = ex;                
            }
            Assert.AreNotEqual(null, expectException);
            Assert.AreEqual(JingtumMessage.INVALID_CURRENCY, expectException.Message);
            Assert.AreEqual(m_InvalidCurrency, expectException.Param);
            baseCurrency = m_ValidCurrency;

            try
            {
                counterCurrency = m_InvalidCurrency;
                m_Wallet.GetUrl_OrderBooks(baseCurrency, baseCounterParty, counterCurrency, counterCounterParty, results_per_page, page);
            }
            catch (InvalidParameterException ex)
            {
                expectException = ex;                
            }
            Assert.AreNotEqual(null, expectException);
            Assert.AreEqual(JingtumMessage.INVALID_CURRENCY, expectException.Message);
            Assert.AreEqual(m_InvalidCurrency, expectException.Param);
            counterCurrency = m_ValidCurrency;

            //with invalid issuer.
            expectException = null;
            try
            {
                baseCounterParty = m_InvalidIssuer;
                m_Wallet.GetUrl_OrderBooks(baseCurrency, baseCounterParty, counterCurrency, counterCounterParty, results_per_page, page);
            }
            catch (InvalidParameterException ex)
            {
                expectException = ex;                
            }
            Assert.AreNotEqual(null, expectException);
            Assert.AreEqual(JingtumMessage.INVALID_JINGTUM_ADDRESS, expectException.Message);
            Assert.AreEqual(m_InvalidIssuer, expectException.Param);
            baseCounterParty = m_ValidIssuer;

            expectException = null;
            try
            {
                counterCounterParty = m_InvalidIssuer;
                m_Wallet.GetUrl_OrderBooks(baseCurrency, baseCounterParty, counterCurrency, counterCounterParty, results_per_page, page);
            }
            catch (InvalidParameterException ex)
            {
                expectException = ex;
            }
            Assert.AreNotEqual(null, expectException);
            Assert.AreEqual(JingtumMessage.INVALID_JINGTUM_ADDRESS, expectException.Message);
            Assert.AreEqual(m_InvalidIssuer, expectException.Param);
            counterCounterParty = m_ValidIssuer;
        } 
        #endregion
        #endregion

        #region transactions
        [Test]
        public void TestUrl_WalletTransaction()
        {
            //test transaction urls.
            Assert.AreEqual(@"https://api.jingtum.com/v2/accounts/jP189vbfqsByaxKY1UEdAtRXTwaBBkDgVJ/Transactions/E9C27C31B4F1C9C502713FA636D64BEFCF678CA4724D766AD46D88319347C52E",
                m_Wallet.GetUrl_Transaction(m_Hash));
        }

        [Test]
        public void TestUrl_WalletTransactions()
        {
            int results_per_page = 20;
            int page = 1;
            int ledger = 7674508;
            int seq = 0;
            //test transactions urls.
            //with 4 parameters.
            Assert.AreEqual(@"https://api.jingtum.com/v2/accounts/jP189vbfqsByaxKY1UEdAtRXTwaBBkDgVJ/Transactions?results_per_page=20&page=1&marker={ledger:7674508,seq:0}",
                m_Wallet.GetUrl_Transactions(results_per_page, page, ledger, seq));

            //with out results_per_page.
            results_per_page = 0;
            page = 1;
            ledger = 7674508;
            seq = 0;
            Assert.AreEqual(@"https://api.jingtum.com/v2/accounts/jP189vbfqsByaxKY1UEdAtRXTwaBBkDgVJ/Transactions?page=1&marker={ledger:7674508,seq:0}",
                m_Wallet.GetUrl_Transactions(results_per_page, page, ledger, seq));

            //with out page.
            results_per_page = 20;
            page = 0;
            ledger = 7674508;
            seq = 0;
            Assert.AreEqual(@"https://api.jingtum.com/v2/accounts/jP189vbfqsByaxKY1UEdAtRXTwaBBkDgVJ/Transactions?results_per_page=20&marker={ledger:7674508,seq:0}",
                m_Wallet.GetUrl_Transactions(results_per_page, page, ledger, seq));

            //with out ledger.
            results_per_page = 20;
            page = 1;
            ledger = 0;
            seq = 0;
            Assert.AreEqual(@"https://api.jingtum.com/v2/accounts/jP189vbfqsByaxKY1UEdAtRXTwaBBkDgVJ/Transactions?results_per_page=20&page=1",
                m_Wallet.GetUrl_Transactions(results_per_page, page, ledger, seq));

            ////with 4 empty options.
            results_per_page = 0;
            page = 0;
            Assert.AreEqual(@"https://api.jingtum.com/v2/accounts/jP189vbfqsByaxKY1UEdAtRXTwaBBkDgVJ/Transactions",
                m_Wallet.GetUrl_Transactions(0, 0, 0, 0));
        }        
        #endregion

        #region smart contract
        #region deploy
        [Test]
        public void TestUrl_WalletDeploy()
        {
            //test deploy url
            string expectUrl = "https://api.jingtum.com/v2/accounts/jP189vbfqsByaxKY1UEdAtRXTwaBBkDgVJ/contract/deploy";
            Assert.AreEqual(expectUrl, this.m_Wallet.GetUrl_Deploy());

            //test deploy parameter
            double amount = 10;
            string payload = "function Init(...) a={} for k,v in ipairs({...}) do a[k]=v end b=a[1] return accountinfo(b) end;  function foo(...) a={} for k,v in ipairs({...}) do a[k]=v end b=a[1] return accountinfo(b) end";

            string expectParameter = "{\"secret\":\"saESGGsyFnTEW9BPWQ6bCfqwNgESU\",\"amount\":10,\"payload\":\"function Init(...) a={} for k,v in ipairs({...}) do a[k]=v end b=a[1] return accountinfo(b) end;  function foo(...) a={} for k,v in ipairs({...}) do a[k]=v end b=a[1] return accountinfo(b) end\"}";
            Assert.AreEqual(expectParameter, this.m_Wallet.GetParameters_Deploy(amount, payload));
        }
        #endregion

        #region call
        [Test]
        public void TestUrl_WalletCall()
        {
            //test call url
            string expectUrl = "https://api.jingtum.com/v2/accounts/jP189vbfqsByaxKY1UEdAtRXTwaBBkDgVJ/contract/call";
            Assert.AreEqual(expectUrl, this.m_Wallet.GetUrl_Call());

            //test call parameter
            string destination = "jKotgzRHyoa7dywd7vf6LgFBXnv3K66zEg";
            List<string> parameters = new List<string>();
            parameters.Add("jsqRs9BDCjyTuRWEPZk3yHa4MFmRi9D834");

            string expectParameter = "{\"secret\":\"saESGGsyFnTEW9BPWQ6bCfqwNgESU\",\"destination\":\"jKotgzRHyoa7dywd7vf6LgFBXnv3K66zEg\",\"params\":[\"jsqRs9BDCjyTuRWEPZk3yHa4MFmRi9D834\"]}";
            Assert.AreEqual(expectParameter, this.m_Wallet.GetParameters_Call(destination, parameters));
        }
        #endregion
        #endregion

        #region sign
        [Test]
        public void TestUrl_WalletSign()
        {
            //test sign url
            string expectUrl = "https://api.jingtum.com/v2/sign";
            Assert.AreEqual(expectUrl, this.m_Wallet.GetUrl_Sign());

            //test sign parameter
            string blob = "1200002200000000240000028461400000000007A12068400000000000000A73210224445F6980BBC7F34F5042893C419E536468F92A9034177C0CB786CC7836025B74473045022100FC7EA9B7200CA4D3F2C4948E86140F14D5C1FA1CE68682288B51928A7C7256ED02204D3993571B4EEA50A64A3CDB2A38D61EA28F46C3563CF64B48A211E44456738D81147A44B90BCADB1F585D590DC31AB83245E049BB668314B9DFBBDC029B81C608497CE3D61C70D79BCCA955";

            string expectParameter = "{\"blob\":\"1200002200000000240000028461400000000007A12068400000000000000A73210224445F6980BBC7F34F5042893C419E536468F92A9034177C0CB786CC7836025B74473045022100FC7EA9B7200CA4D3F2C4948E86140F14D5C1FA1CE68682288B51928A7C7256ED02204D3993571B4EEA50A64A3CDB2A38D61EA28F46C3563CF64B48A211E44456738D81147A44B90BCADB1F585D590DC31AB83245E049BB668314B9DFBBDC029B81C608497CE3D61C70D79BCCA955\"}";
            Assert.AreEqual(expectParameter,
                this.m_Wallet.GetParameters_Sign(blob));
        }
        #endregion
    }

    [TestFixture]
    public class Test_Wallet_API
    {
        Wallet m_Wallet = new Wallet(@"jP189vbfqsByaxKY1UEdAtRXTwaBBkDgVJ", @"saESGGsyFnTEW9BPWQ6bCfqwNgESU");
        Wallet m_Wallet_WithoutSecret = new Wallet(@"jfdLqEWhfYje92gEaWixVWsYKjK5C6bMoi");

        string m_ValidCurrency = Utility.CURRENCY_CNY;
        string m_InvalidCurrency = "NES";
        string m_ValidIssuer = "jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o9or";
        string m_InvalidIssuer = "jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o8or";
        //string m_Hash = "E9C27C31B4F1C9C502713FA636D64BEFCF678CA4724D766AD46D88319347C52E";

        #region new
        //[Test]
        public void Test_Wallet_New()
        {
            Wallet newWallet = Wallet.New();
            Assert.AreEqual('j', newWallet.Address[0]); // test if the address starts with 'j".
            Assert.AreEqual(34, newWallet.Address.Length); //test if the address length is 34.
            Assert.AreEqual('s', newWallet.Secret[0]); // test if the secret starts with 's". 
            Assert.AreEqual(29, newWallet.Secret.Length); //test if the secret length is 29.
        }
        #endregion

        #region IsActived
        //[Test]
        public void Test_Wallet_IsActived()
        {
            Assert.IsFalse(m_Wallet.IsActived);
            Assert.IsTrue(m_Wallet_WithoutSecret.IsActived);            
        }
        #endregion

        #region balances
        //[Test]
        public void Test_Wallet_Balances()
        {
            //test balances urls.
            //no options.
            Assert.Greater(m_Wallet_WithoutSecret.GetBalanceList().Count, 1);

            //with currecy and issuer.
            Assert.AreEqual(1, m_Wallet_WithoutSecret.GetBalanceList(m_ValidCurrency, m_ValidIssuer).Count);

            //with only currency.
            Assert.AreEqual(1, m_Wallet_WithoutSecret.GetBalanceList(m_ValidCurrency, string.Empty).Count);

            //with only issuer.
            Assert.Greater(m_Wallet_WithoutSecret.GetBalanceList(string.Empty, m_ValidIssuer).Count, 1);

            //with 2 empty options.
            Assert.Greater(m_Wallet_WithoutSecret.GetBalanceList(string.Empty, string.Empty).Count, 1);

            //with invalid currency.            
            try
            {
                m_Wallet_WithoutSecret.GetBalanceList(m_InvalidCurrency, m_ValidIssuer);
            }
            catch (InvalidParameterException ex)
            {
                Assert.AreEqual(JingtumMessage.INVALID_CURRENCY, ex.Message);
                Assert.AreEqual(m_InvalidCurrency, ex.Param);
            }

            //with invalid issuer.
            try
            {
                m_Wallet_WithoutSecret.GetBalanceList(m_ValidCurrency, m_InvalidIssuer);
            }
            catch (InvalidParameterException ex)
            {
                Assert.AreEqual(JingtumMessage.INVALID_JINGTUM_ADDRESS, ex.Message);
                Assert.AreEqual(m_InvalidIssuer, ex.Param);
            }
        }
        #endregion
    }
}
