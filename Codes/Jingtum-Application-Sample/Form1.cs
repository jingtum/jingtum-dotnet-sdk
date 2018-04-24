using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using Jingtum.API;

namespace Jingtum_Application_Sample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region new and balance
        //new
        private void button1_Click(object sender, EventArgs e)
        {
            Jingtum.API.Wallet newWallet = Jingtum.API.Wallet.New();
            this.ShowPropertyValue<Jingtum.API.Wallet>(newWallet);
        }

        //balance
        private void button2_Click(object sender, EventArgs e)
        {
            String address = this.textBox1.Text;
            Jingtum.API.Wallet wallet = new Jingtum.API.Wallet(address);
            List<Jingtum.API.Balance> balances = wallet.GetBalanceList();
            this.ShowPropertyValueList<Jingtum.API.Balance>(balances);
        }
        #endregion

        #region payment
        //get choices
        private void button5_Click(object sender, EventArgs e)
        {
            string address = this.textBox7.Text;
            Jingtum.API.Wallet wallet = new Jingtum.API.Wallet(address);
            string distinationAddress = this.textBox8.Text;
            int amount = int.Parse(this.textBox9.Text);
            string currency = this.textBox10.Text;
            string issue = "jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o9or";
            List<Jingtum.API.PaymentChoice> choices = wallet.GetPaymentChoices(distinationAddress, amount, currency, issue);
            this.ShowPropertyValue<List<Jingtum.API.PaymentChoice>>(choices);
        }

        //set payment
        private void button6_Click(object sender, EventArgs e)
        {
            //Jingtum.API.Wallet wallet = new Jingtum.API.Wallet(this.textBox12.Text, this.textBox13.Text);

            //Jingtum.API.Payment payment = new Jingtum.API.Payment();
            //Jingtum.API.Amount amount = new Jingtum.API.Amount();
            //amount.Value = int.Parse(this.textBox14.Text);
            //amount.Currency = this.textBox11.Text;
            //amount.Issuer = string.Empty;
            //payment.Amount = amount;

            //List<string> memos = new List<string>();
            //memos.Add("Test: " + DateTime.Now.ToString("o"));

            //string distinationAddress = this.textBox15.Text;

            //Jingtum.API.SetPaymentResponse res = wallet.SetPayment(payment, distinationAddress, string.Empty, memos);
            //this.ShowPropertyValue<Jingtum.API.SetPaymentResponse>(res);


            string address = this.textBox12.Text;
            string secret = this.textBox13.Text;
            Jingtum.API.Wallet wallet = new Jingtum.API.Wallet(address, secret);
            Jingtum.API.Payment payment = new Jingtum.API.Payment();
            Jingtum.API.Amount amount = new Jingtum.API.Amount();
            amount.Value = int.Parse(this.textBox14.Text);
            amount.Currency = this.textBox11.Text;
            amount.Issuer = string.Empty;
            payment.Amount = amount;
            List<string> memos = new List<string>();
            memos.Add("Test: " + DateTime.Now.ToString("o"));
            string distinationAddress = this.textBox15.Text;
            Jingtum.API.SetPaymentResponse res = wallet.SetPayment(payment, distinationAddress, string.Empty, memos);
            this.ShowPropertyValue<Jingtum.API.SetPaymentResponse>(res);
        }

        //get payment
        private void button3_Click(object sender, EventArgs e)
        {
            string address = this.textBox2.Text;
            Jingtum.API.Wallet wallet = new Jingtum.API.Wallet(address);
            string hash = this.textBox3.Text;
            Jingtum.API.Payment payment = wallet.GetPayment(hash);
            this.ShowPropertyValue<Jingtum.API.Payment>(payment);
        }

        //get payments
        private void button4_Click(object sender, EventArgs e)
        {
            string address = this.textBox4.Text;
            Jingtum.API.Wallet wallet = new Jingtum.API.Wallet(address);   
            List<Jingtum.API.Payment> payments = new List<Jingtum.API.Payment>();
            payments = wallet.GetPaymentList(int.Parse(this.textBox5.Text), int.Parse(this.textBox6.Text));
            this.ShowPropertyValueList<Jingtum.API.Payment>(payments);
        }
        #endregion

        #region transaction
        private void button7_Click(object sender, EventArgs e)
        {
            string address = this.textBox16.Text;            
            string hash = this.textBox17.Text;
            Jingtum.API.Wallet wallet = new Jingtum.API.Wallet(address);
            Jingtum.API.Transaction transaction = wallet.GetTransaction(hash);
            this.ShowPropertyValue<Jingtum.API.Transaction>(transaction);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string address = this.textBox18.Text;
            Jingtum.API.Wallet wallet = new Jingtum.API.Wallet(address);
            int pageSize = int.Parse(this.textBox19.Text);
            int page = int.Parse(this.textBox20.Text);
            int ledger = this.textBox21.Text == string.Empty ? 0 : int.Parse(this.textBox21.Text);
            int seq = this.textBox22.Text == string.Empty ? 0 : int.Parse(this.textBox22.Text);
            List<Jingtum.API.Transaction> transactions = wallet.GetTransactions(pageSize, page, ledger, seq);
            this.ShowPropertyValue<List<Jingtum.API.Transaction>>(transactions);
        }
        #endregion

        #region order
        //get order
        private void button9_Click(object sender, EventArgs e)
        {
            string address = this.textBox23.Text;
            Jingtum.API.Wallet wallet = new Jingtum.API.Wallet(address);
            string hash = this.textBox24.Text;
            Jingtum.API.Order order = wallet.GetOrder(hash);
            this.ShowPropertyValue<Jingtum.API.Order>(order);
        }

        //get orders
        private void button10_Click(object sender, EventArgs e)
        {
            string address = this.textBox25.Text;
            Jingtum.API.Wallet wallet = new Jingtum.API.Wallet(address);
            int pageSize = int.Parse(this.textBox26.Text);
            int page = int.Parse(this.textBox27.Text);
            List<Jingtum.API.Order> orders = wallet.GetOrderList(pageSize, page);
            this.ShowPropertyValue<List<Jingtum.API.Order>>(orders);
        }

        //get order books
        private void button11_Click(object sender, EventArgs e)
        {
            string address = this.textBox28.Text;
            Jingtum.API.Wallet wallet = new Jingtum.API.Wallet(address);
            int pageSize = int.Parse(this.textBox29.Text);
            int page = int.Parse(this.textBox30.Text);
            string baseCurrency = "SWT";
            string baseCounterParty = "jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o9or";
            string counterCurrency = "CNY";
            string counterCounterParty = "jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o9or";
            Jingtum.API.OrderBookResponse response = null;
            if(this.radioButton1.Checked)
            {
                response = wallet.GetOrderBooks(baseCurrency, baseCounterParty, counterCurrency, counterCounterParty, pageSize, page);
            }
            else if (this.radioButton2.Checked)
            {
                response = wallet.GetOrderBookBids(baseCurrency, baseCounterParty, counterCurrency, counterCounterParty, pageSize, page);
            }
            else if (this.radioButton3.Checked)
            {
                response = wallet.GetOrderBookAsks(baseCurrency, baseCounterParty, counterCurrency, counterCounterParty, pageSize, page);
            }

            this.ShowPropertyValue<Jingtum.API.OrderBookResponse>(response);            
        }

        //set order
        private void button12_Click(object sender, EventArgs e)
        {
            string address = this.textBox31.Text;
            string secret = this.textBox32.Text;
            Jingtum.API.Wallet wallet = new Jingtum.API.Wallet(address, secret);
            Jingtum.API.Order order = new Jingtum.API.Order();
            order.Type = this.radioButton6.Checked ? "sell" : "buy";
            order.Amount = double.Parse(this.textBox33.Text);
            order.Price = double.Parse(this.textBox34.Text);
            order.Pair = (this.radioButton6.Checked ? "SWT/CNY" : "CNY/SWT") + ":jGa9J9TkqtBcUoHe2zqhVFFbgUVED6o9or";

            this.ShowPropertyValue<Jingtum.API.SetOrderResponse>(wallet.SetOrder(order));

        }

        //cancel order
        private void button13_Click(object sender, EventArgs e)
        {
            string address = this.textBox35.Text;
            string secret = this.textBox36.Text;
            Jingtum.API.Wallet wallet = new Jingtum.API.Wallet(address, secret);
            int order = int.Parse(this.textBox37.Text);

            this.ShowPropertyValue<Jingtum.API.SetOrderResponse>(wallet.CancelOrder(order));
        }
        #endregion

        #region ShowPropertyValue
        private void ShowPropertyValue<T>(T item)
        {
            this.m_Text_Result.Text += Newtonsoft.Json.JsonConvert.SerializeObject(item);
            this.m_Text_Result.Text += "\r\n\r\n\r\n";
        }

        private void ShowPropertyValueList<T>(List<T> list)
        {
            foreach (T item in list)
            {
                this.ShowPropertyValue<T>(item);
            }
        }
        #endregion

        #region layout
        private void tabControl1_Resize(object sender, EventArgs e)
        {
            this.LayoutAllControls();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.LayoutAllControls();
        }

        private void LayoutAllControls()
        {
            int index = 0;

            #region balance tab
            this.SetLocationAndSize(this.label1, this.tabControl1, index++);
            this.SetLocationAndSize(this.textBox1, this.tabControl1, index++);
            this.SetLocationAndSize(this.button2, this.tabControl1, index++);
            #endregion

            #region payment tab
            //get payment
            index = 0;
            this.SetLocationAndSize(this.label2, this.tabControl2, index++);
            this.SetLocationAndSize(this.textBox2, this.tabControl2, index++);
            this.SetLocationAndSize(this.label3, this.tabControl2, index++);
            this.SetLocationAndSize(this.textBox3, this.tabControl2, index++);
            this.SetLocationAndSize(this.button3, this.tabControl2, index++);

            //get payment
            index = 0;
            this.SetLocationAndSize(this.label4, this.tabControl2, index++);
            this.SetLocationAndSize(this.textBox4, this.tabControl2, index++);
            this.SetLocationAndSize(this.label5, this.tabControl2, index++);
            this.SetLocationAndSize(this.textBox5, this.tabControl2, index++);
            this.SetLocationAndSize(this.label6, this.tabControl2, index++);
            this.SetLocationAndSize(this.textBox6, this.tabControl2, index++);
            this.SetLocationAndSize(this.button4, this.tabControl2, index++);

            //get choices
            index = 0;
            this.SetLocationAndSize(this.label7, this.tabControl2, index++);
            this.SetLocationAndSize(this.textBox7, this.tabControl2, index++);
            this.SetLocationAndSize(this.label8, this.tabControl2, index++);
            this.SetLocationAndSize(this.textBox8, this.tabControl2, index++);
            this.SetLocationAndSize(this.label9, this.tabControl2, index++);
            this.SetLocationAndSize(this.textBox9, this.tabControl2, index++);
            this.SetLocationAndSize(this.label10, this.tabControl2, index++);
            this.SetLocationAndSize(this.textBox10, this.tabControl2, index++);
            this.SetLocationAndSize(this.button5, this.tabControl2, index++);

            //set payment
            index = 0;            
            this.SetLocationAndSize(this.label12, this.tabControl2, index++);
            this.SetLocationAndSize(this.textBox12, this.tabControl2, index++);
            this.SetLocationAndSize(this.label13, this.tabControl2, index++);
            this.SetLocationAndSize(this.textBox13, this.tabControl2, index++);
            this.SetLocationAndSize(this.label14, this.tabControl2, index++);
            this.SetLocationAndSize(this.textBox14, this.tabControl2, index++);
            this.SetLocationAndSize(this.label11, this.tabControl2, index++);
            this.SetLocationAndSize(this.textBox11, this.tabControl2, index++);
            this.SetLocationAndSize(this.label15, this.tabControl2, index++);
            this.SetLocationAndSize(this.textBox15, this.tabControl2, index++);
            this.SetLocationAndSize(this.button6, this.tabControl2, index++);
            #endregion

            #region transaction tab
            //get transaction
            index = 0;
            this.SetLocationAndSize(this.label16, this.tabControl3, index++);
            this.SetLocationAndSize(this.textBox16, this.tabControl3, index++);
            this.SetLocationAndSize(this.label17, this.tabControl3, index++);
            this.SetLocationAndSize(this.textBox17, this.tabControl3, index++);
            this.SetLocationAndSize(this.button7, this.tabControl3, index++);

            //get transactions
            index = 0;
            this.SetLocationAndSize(this.label18, this.tabControl3, index++);
            this.SetLocationAndSize(this.textBox18, this.tabControl3, index++);
            this.SetLocationAndSize(this.label19, this.tabControl3, index++);
            this.SetLocationAndSize(this.textBox19, this.tabControl3, index++);
            this.SetLocationAndSize(this.label20, this.tabControl3, index++);
            this.SetLocationAndSize(this.textBox20, this.tabControl3, index++);
            this.SetLocationAndSize(this.label21, this.tabControl3, index++);
            this.SetLocationAndSize(this.textBox21, this.tabControl3, index++);
            this.SetLocationAndSize(this.label22, this.tabControl3, index++);
            this.SetLocationAndSize(this.textBox22, this.tabControl3, index++);
            this.SetLocationAndSize(this.button8, this.tabControl3, index++);
            #endregion

            #region order tab
            //get order
            index = 0;
            this.SetLocationAndSize(this.label23, this.tabControl4, index++);
            this.SetLocationAndSize(this.textBox23, this.tabControl4, index++);
            this.SetLocationAndSize(this.label24, this.tabControl4, index++);
            this.SetLocationAndSize(this.textBox24, this.tabControl4, index++);
            this.SetLocationAndSize(this.button9, this.tabControl4, index++);

            //get orders
            index = 0;
            this.SetLocationAndSize(this.label25, this.tabControl4, index++);
            this.SetLocationAndSize(this.textBox25, this.tabControl4, index++);
            this.SetLocationAndSize(this.label26, this.tabControl4, index++);
            this.SetLocationAndSize(this.textBox26, this.tabControl4, index++);
            this.SetLocationAndSize(this.label27, this.tabControl4, index++);
            this.SetLocationAndSize(this.textBox27, this.tabControl4, index++);
            this.SetLocationAndSize(this.button10, this.tabControl4, index++);

            //get order books
            this.groupBox1.Location = new Point(0, 0);
            this.groupBox1.Width = this.tabControl4.Width - 10;
            this.groupBox1.Height = 30 * 3;
            index = 3;
            this.SetLocationAndSize(this.label28, this.tabControl4, index++);
            this.SetLocationAndSize(this.textBox28, this.tabControl4, index++);
            this.SetLocationAndSize(this.label29, this.tabControl4, index++);
            this.SetLocationAndSize(this.textBox29, this.tabControl4, index++);
            this.SetLocationAndSize(this.label30, this.tabControl4, index++);
            this.SetLocationAndSize(this.textBox30, this.tabControl4, index++);
            this.SetLocationAndSize(this.button11, this.tabControl4, index++);

            //set order
            this.groupBox2.Location = new Point(0, 0);
            this.groupBox2.Width = this.tabControl4.Width - 10;
            this.groupBox2.Height = 30 * 2;
            index = 2;
            this.SetLocationAndSize(this.label31, this.tabControl4, index++);
            this.SetLocationAndSize(this.textBox31, this.tabControl4, index++);
            this.SetLocationAndSize(this.label32, this.tabControl4, index++);
            this.SetLocationAndSize(this.textBox32, this.tabControl4, index++);
            this.SetLocationAndSize(this.label33, this.tabControl4, index++);
            this.SetLocationAndSize(this.textBox33, this.tabControl4, index++);
            this.SetLocationAndSize(this.label34, this.tabControl4, index++);
            this.SetLocationAndSize(this.textBox34, this.tabControl4, index++);
            this.SetLocationAndSize(this.button12, this.tabControl4, index++);


            //cancel order
            index = 0;
            this.SetLocationAndSize(this.label35, this.tabControl4, index++);
            this.SetLocationAndSize(this.textBox35, this.tabControl4, index++);
            this.SetLocationAndSize(this.label36, this.tabControl4, index++);
            this.SetLocationAndSize(this.textBox36, this.tabControl4, index++);
            this.SetLocationAndSize(this.label37, this.tabControl4, index++);
            this.SetLocationAndSize(this.textBox37, this.tabControl4, index++);
            this.SetLocationAndSize(this.button13, this.tabControl4, index++);
            #endregion
        }

        private void SetLocationAndSize(Control control, Control container, int index)
        {
            const int StandardHeight = 30;
            const int StandardX = 0;
            const int WidthOffset = 10;

            control.Height = StandardHeight;
            control.Width = container.Width - WidthOffset;

            int y = StandardHeight * index + 1;
            control.Location = new Point(StandardX, y);
        }
        #endregion        

        

        

        

        

        

        

        

    }
}
