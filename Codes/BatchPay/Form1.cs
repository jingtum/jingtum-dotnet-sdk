using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Jingtum.API;

namespace BatchPay
{
    public partial class Form1 : Form
    {
        List<string> m_AddressList = new List<string>();
        List<string> m_InvalidAddressList = new List<string>();
        int lineCount = 0;

        public Form1()
        {
            InitializeComponent();
        }

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
            this.SetLocationAndSize(this.label1, this.tabControl1.TabPages[0], index++);
            index = this.SetLocationAndSize(this.textBox1, this.tabControl1.TabPages[0], index++, 8);

            this.SetLocationAndSize(this.button1, this.tabControl1.TabPages[0], index++);
            this.SetLocationAndSize(this.label2, this.tabControl1.TabPages[0], index++);
            this.SetLocationAndSize(this.textBox2, this.tabControl1, index++);

            this.SetLocationAndSize(this.label3, this.tabControl1.TabPages[0], index++);
            this.SetLocationAndSize(this.textBox3, this.tabControl1.TabPages[0], index++);
            this.SetLocationAndSize(this.label4, this.tabControl1.TabPages[0], index++);
            this.SetLocationAndSize(this.textBox4, this.tabControl1.TabPages[0], index++);
            this.SetLocationAndSize(this.label5, this.tabControl1.TabPages[0], index++);
            this.SetLocationAndSize(this.textBox5, this.tabControl1.TabPages[0], index++);
            this.SetLocationAndSize(this.label6, this.tabControl1.TabPages[0], index++);
            this.SetLocationAndSize(this.textBox6, this.tabControl1.TabPages[0], index++);
            this.SetLocationAndSize(this.label7, this.tabControl1.TabPages[0], index++);
            this.SetLocationAndSize(this.textBox7, this.tabControl1.TabPages[0], index++);
            this.SetLocationAndSize(this.button2, this.tabControl1.TabPages[0], index++);
            #endregion
        }

        private int SetLocationAndSize(Control control, Control container, int index)
        {
            return this.SetLocationAndSize(control, container, index, 1);
        }

        private int SetLocationAndSize(Control control, Control container, int index, int heightLines)
        {
            const int StandardHeight = 30;
            const int StandardX = 0;
            const int WidthOffset = 0;

            control.Height = (heightLines != 0) ? StandardHeight * heightLines : container.Height - StandardHeight * index - 10;
            control.Width = container.Width - WidthOffset;

            int y = StandardHeight * index + 1;
            control.Location = new Point(StandardX, y);

            return index + heightLines;
        }
        #endregion     

        private void button1_Click(object sender, EventArgs e)
        {
            this.ReadAddressList(this.textBox1.Lines, out m_AddressList, out m_InvalidAddressList);            

            foreach (string address in m_InvalidAddressList)
            {
                string error = "搜索井通地址出错, 行： 【" + address + "】 长度为 " + address.Length;
                this.Print(error);               
            }

            string message = "一共找到" + m_AddressList.Count + "个井通地址";
            MessageBox.Show(message);

            this.Print(message);
            foreach (string address in m_AddressList)
            {
                this.Print(address);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int index = 0;
            int failCount = 0;
            foreach (string address in m_AddressList)
            {
                string result;
                index++;
                try 
                {
                    bool paySuccess = this.Pay(address);                    
                    result = address + ": 支付" + (paySuccess ? "成功" : "失败");
                      
                }
                catch (Exception ex)
                {
                    failCount++;
                    result = address + ": 支付失败，原因是" + ex.Message;

                }

                result += "，批处理完成度： " + (index / (float)m_AddressList.Count).ToString("#0.##%");
                this.Print(result); 
            }

            string message = "总共 " + m_AddressList.Count + " 个支付, " 
                + ((failCount == 0) ? "全部成功！" : failCount + " 个支付失败，请检查log记录！");
            MessageBox.Show(message);
        }

        private void ReadAddressList(string[] lines, out List<string> validList, out List<string> inValidList) 
        {
            validList = new List<string>();
            inValidList = new List<string>();

            int count = lines.Length;
            for (int i = 0; i < count; i++)
            {
                if(IsVaidAddress(lines[i]))
                {
                    validList.Add(lines[i]);
                }
                else
                {
                    inValidList.Add(lines[i]);                   
                }
            }
        }

        private bool IsVaidAddress(string address)
        {
            //if ((address.Length == 34 || address.Length == 33) && address[0] == 'j')
            //{
            //    return true;
            //}
            //else
            //{      
            //    return false;
            //}
            
            return Jingtum.API.Utility.IsValidAddress(address);            
        }

        private bool Pay(string distinationAddress) 
        {
            Wallet wallet = new Wallet();
            wallet.Address = this.textBox2.Text;
            wallet.Secret = this.textBox3.Text;

            if(!(Jingtum.API.Utility.IsValidAddress(wallet.Address) && Jingtum.API.Utility.IsValidSecret(wallet.Secret)))
            {
                return false;
            }

            Payment payment = new Payment();
            Amount amount = new Amount();
            amount.Value = double.Parse(this.textBox6.Text);
            amount.Currency = this.textBox4.Text;
            amount.Issuer = this.textBox5.Text;
            payment.Amount = amount;

            List<string> memos = new List<string>();
            memos.Add(this.textBox7.Text);

            SetPaymentResponse res = wallet.SetPayment(payment, distinationAddress, string.Empty, memos);
            return res.Success;
        }

        private void Print(string content)
        {
            this.m_Text_Result.Text += (++lineCount).ToString() + ". " + DateTime.Now.ToString("o") + ": " + content + "\r\n";
            this.m_Text_Result.Refresh();
        }
    }
}
