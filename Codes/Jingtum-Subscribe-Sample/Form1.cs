using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebSocketSharp;

namespace Jingtum_Subscribe
{
    public partial class Form1 : Form
    {
        const string WSS_ADDRESS = @"wss://tapi.jingtum.com:5443";
        const string SUBSCRIPT = @"{"
            + "\"command\": \"subscribe\","
            + "\"type\": \"transactions\","
            + "\"account\": \"\","
            + "\"secret\": \"\""
            + "}";

        WebSocket m_WebSocket = new WebSocket(WSS_ADDRESS);

        public Form1()
        {
            InitializeComponent();
        }

        private void LayoutResize()
        {
            const int Width = 400;
            const int Height_Button = 40;

            this.button1.Location = new Point(0, 0);
            this.button1.Width = Width;
            this.button1.Height = Height_Button;

            this.button2.Location = new Point(0, Height_Button);
            this.button2.Width = Width;
            this.button2.Height = Height_Button;

            this.textBox1.Location = new Point(0, Height_Button * 2);
            this.textBox1.Width = Width;
            this.textBox1.Height = this.ClientRectangle.Height - Height_Button * 3;

            this.button3.Location = new Point(0, this.textBox1.Height + Height_Button * 2);
            this.button3.Width = Width;
            this.button3.Height = Height_Button;

            this.textBox2.Location = new Point(Width, 0);
            this.textBox2.Width = this.ClientRectangle.Width - Width;
            this.textBox2.Height = this.ClientRectangle.Height;
        }

        private void ConnectWSS()
        {
            m_WebSocket.OnMessage += new EventHandler<MessageEventArgs>(ws_OnMessage);
            m_WebSocket.Connect();            
        }

        private void ShowResponse(string text)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.textBox2.Text = text + "\r\n" + this.textBox2.Text;
            Control.CheckForIllegalCrossThreadCalls = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LayoutResize();
            this.textBox1.Text = SUBSCRIPT;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            LayoutResize();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConnectWSS();
        }

        private void ws_OnMessage(object sender, MessageEventArgs e)
        {
            ShowResponse("Sever response: " + e.Data);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            m_WebSocket.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            m_WebSocket.Send(this.textBox1.Text);
        }
    }
}
