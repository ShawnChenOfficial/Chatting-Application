using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Net.Sockets;
using MessageType;

namespace Client
{
    public partial class Add_Chater : Form
    {
        private string accountThis = "";
        private IFormatter formatter = new BinaryFormatter();
        private NetworkStream stream;
        public Add_Chater(string accountThis,NetworkStream stream)
        {
            CenterToScreen();
            InitializeComponent();
            this.stream = stream;
            this.accountThis = accountThis;
        }


        private void button_OK_Click(object sender, EventArgs e)
        {
            string targetAccount = textBox1.Text;
            formatter.Serialize(stream, new AddChat(accountThis, targetAccount));
            this.Close();
        }
    }
}
