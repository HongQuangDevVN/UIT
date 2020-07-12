using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Clients
{
    public partial class Clients : Form
    {
        private TCPModel tcp;
        Button[] bt = new Button[4];
        public Clients()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            bt[0] = button1;
            bt[1] = button2;
        }

        public void clickHandling(int i)
        {
            tcp = new TCPModel("127.0.0.1", int.Parse("13000"));
            tcp.ConnectToServer();
            tcp.SendData("J" + i.ToString());
            string str = tcp.ReadData();
            if (str == "Join Room")
            {
                Form1 f = new Form1(tcp,i);
                this.Hide();
                f.ShowDialog();
                this.Show();
            }
            else if (str == "Full")
                MessageBox.Show("Room is FULL!");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            clickHandling(1);
        }
    }
}
