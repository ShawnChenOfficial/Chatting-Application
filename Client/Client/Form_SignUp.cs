using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MessageType;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace Client
{
    public partial class Form_SignUp : Form
    {
        private NetworkStream stream;
        private IFormatter formatter = new BinaryFormatter();
        public Form_SignUp(NetworkStream stream)
        {
            this.stream = stream;
            CenterToScreen();
            InitializeComponent();
            button_OK.DialogResult = DialogResult.Yes;
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            string account = textBox_Account.Text;
            string password1 = textBox_Password1.Text;
            string password2 = textBox_Password2.Text;
            if (account == null || account == "")
            {
                MessageBox.Show("Account cannot be empty...");
            }
            else if (password1 == null || password1 == "")
            {
                MessageBox.Show("Password cannot be empty...","Ok",MessageBoxButtons.OK);
            }
            else if (password2 == null || password2 == "")
            {
                MessageBox.Show("Please enter your password again...", "Ok", MessageBoxButtons.OK);
            }
            else if (password1 == password2)
            {
                formatter.Serialize(stream, new SignUp(account, password1));
                MessageBox.Show("Checking availability of your account...", "Ok", MessageBoxButtons.OK);

                object obj = formatter.Deserialize(stream);

                if (obj is SignUp_Result)
                {
                    SignUp_Result signUp_Result = obj as SignUp_Result;
                    switch (signUp_Result.Result)
                    {
                        case true:
                            MessageBox.Show("Success...", "Ok", MessageBoxButtons.OK);
                            this.Close();
                            break;
                        case false:
                            MessageBox.Show("Account is existing...", "Ok", MessageBoxButtons.OK);
                            break;
                    }
                }
            }
            else
            {
                MessageBox.Show("Passwords are not matching.", "Ok", MessageBoxButtons.OK);
            }
        }
    }
}
