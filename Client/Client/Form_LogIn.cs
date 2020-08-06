using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using MessageType;


namespace Client
{
    public partial class Form_LogIn : Form
    {
        private TcpClient tcpClient;
        private NetworkStream networkStream;
        private IFormatter formatter = new BinaryFormatter();
        public Form_LogIn()
        {
            CenterToScreen();
            InitializeComponent();
        }

        private void Form_LogIn_Load(object sender, EventArgs e)
        {

            this.FormClosing += Form_LogIn_FormClosing;
        }

        private void Form_LogIn_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void button_SignUp_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form sign_Up = new Form_SignUp(networkStream);
            if (sign_Up.ShowDialog() == DialogResult.Yes)
            {
                this.Show();
            }
        }

        private void button_LogIn_Click(object sender, EventArgs e)
        {
            try
            {
                tcpClient = new TcpClient("192.168.80.61", 2099);
                networkStream = tcpClient.GetStream();

                string account = textBox_Account.Text;
                string password = textBox_Password.Text;

                formatter.Serialize(networkStream, new LogIn(account, password));

                object obj = formatter.Deserialize(networkStream);

                if (obj is LogIn_Result)
                {
                    LogIn_Result result = obj as LogIn_Result;
                    switch (result.Result)
                    {
                        case true:
                            MainPage mainPage = new MainPage(account, networkStream);
                            this.Hide();
                            mainPage.ShowDialog();
                            this.Show();
                            break;
                        case false:
                            MessageBox.Show("Incorrect account or password...");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cannot connect to server, due to" + ex.Message);
            }
        }

    }
}
