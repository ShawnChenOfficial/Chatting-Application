using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Awaiting : Form
    {
        public Awaiting()
        {
            CenterToParent();
            InitializeComponent();
            button_Close.Enabled = false;
            button_Close.DialogResult = DialogResult.Cancel;
        }

        delegate void Update_ContentDEL(string message);
        public void Update_Content(string message)
        {
            if (InvokeRequired)
            {
                Update_ContentDEL del = Update_Content;
                label1.Invoke(del, message);
            }
            else
            {
                label1.Text = message;
            }
        }

        delegate void Enable_ButtonDEL();
        public void Enable_Button()
        {
            if (InvokeRequired)
            {
                Enable_ButtonDEL del = Enable_Button;
                button_Close.Invoke(del);
            }
            else
            {
                button_Close.Enabled = true;
            }
        }


        private void button_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
