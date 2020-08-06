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
using System.Net;
using System.Threading;
using MessageType;
using MySql.Data.MySqlClient;

namespace Server
{
    partial class Form1 : Form
    {
        private TcpClient client;
        private TcpListener listener;
        private bool listening = false;

        public List<LogIn> exists_Users = new List<LogIn>();
        public Dictionary<string,User_Availability> user_Availability = new Dictionary<string, User_Availability>();
        public Dictionary<string, MessageHandler> online_Users = new Dictionary<string, MessageHandler>();
        public Dictionary<Game_Ready, Game_Ready> game_Pair = new Dictionary<Game_Ready, Game_Ready>();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Button_Change(true, false, true);
            listener = new TcpListener(IPAddress.Any, 2099);
        }

        private void button_Start_Click(object sender, EventArgs e)
        {
            listening = true;

            Thread thread = new Thread(new ThreadStart(Start_Server));
            thread.Start();
            LoadUsers();
            Button_Change(false, true, false);
        }

        private void button_Stop_Click(object sender, EventArgs e)
        {
            listening = false;
            foreach (KeyValuePair<string, MessageHandler> user in online_Users)
            {
                try
                {
                    user.Value.check = false;
                    user.Value.Quit();
                }
                catch (Exception ex)
                {
                    DisplayMessage("Error killing client\n");
                }
            }
            online_Users.Clear();
            Button_Change(true, false, true);
            label_Value.Text = "Please Start Server";
        }

        private void button_Close_Click(object sender, EventArgs e)
        {
            listening = false;
            this.Close();
        }
        private void Button_Change(bool start, bool stop, bool close)
        {
            button_Start.Enabled = start;
            button_Stop.Enabled = stop;
            button_Close.Enabled = close;
        }

        private void Start_Server()
        {
            listener.Start();

            while (listening)
            {
                while (!listener.Pending() && listening)
                {
                    Thread.Sleep(500);
                }
                if (listening)
                {
                    client = listener.AcceptTcpClient();
                    DisplayMessage("Received a connection request from " + client.Client.RemoteEndPoint
                        + "\nAt: " + DateTime.Now + "\n");
                    MessageHandler messageHandler = new MessageHandler(this,client);
                    Thread thread = new Thread(new ThreadStart(messageHandler.run));
                    thread.IsBackground = true;
                    thread.Start();
                }
            }

            listener.Stop();
        }

        public void LoadUsers()
        {
            exists_Users.Clear();

            string connString = "server=127.0.0.1;database=chatlifedb;Uid=root;Pwd=";
            MySqlConnection con = new MySqlConnection(connString);
            con.Open();
            MySqlCommand comd = new MySqlCommand("select * from userpassword", con);
            MySqlDataReader reader = comd.ExecuteReader();

            while (reader.Read())
            {
                exists_Users.Add(new LogIn(reader.GetString(1), reader.GetString(2)));
            }

            con.Close();

            DisplayNumberOfOnlineUsers();
        }

        delegate void DisplayNumberOfOnlineUsersDEL();
        public void DisplayNumberOfOnlineUsers()
        {
            if (InvokeRequired)
            {
                DisplayNumberOfOnlineUsersDEL del = DisplayNumberOfOnlineUsers;
                label_Value.Invoke(del);
            }
            else
            {
                label_Value.Text = online_Users.Count.ToString();
            }
        }
        delegate void DisplayMessageDEL(string message);
        public void DisplayMessage(string message)
        {
            if (InvokeRequired)
            {
                DisplayMessageDEL del = DisplayMessage;
                richTextBox.Invoke(del, message);
            }
            else
            {
                richTextBox.AppendText(message);
                richTextBox.ScrollToCaret();
            }
        }
    }
}
