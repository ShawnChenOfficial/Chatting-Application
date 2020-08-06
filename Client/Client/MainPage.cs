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
using System.Threading;
namespace Client
{
    public partial class MainPage : Form
    {
        private Dictionary<string, List<string>> Chats = new Dictionary<string, List<string>>();
        private Dictionary<Button, string> chatList = new Dictionary<Button, string>();
        private string accountThis = "";
        private string account_Talking_to = "";
        private string account_right_Clicked = "";
        private NetworkStream stream;
        private IFormatter formatter = new BinaryFormatter();
        private bool RECEIVE_DONE = false;
        private string message;
        private Awaiting awaiting = new Awaiting();

        public MainPage(string account, NetworkStream stream)
        {
            InitializeComponent();

            this.stream = stream;
            accountThis = account;
        }

        private void button_AddNewChater_Click(object sender, EventArgs e)
        {
            Form add_Chat = new Add_Chater(accountThis, stream);
            add_Chat.ShowDialog();
        }
        private void Received_Message_Invitation(string accountFrom, string message)
        {
            if (Chats.ContainsKey(accountFrom))
            {
                Chats[accountFrom].Add("R" + message);
            }
            else if (!Chats.ContainsKey(accountFrom))
            {
                Chats.Add(accountFrom, new List<string>());
                Chats[accountFrom].Add("R" + message);
                Refresh_Panel_ChatList();
                foreach (KeyValuePair<Button, string> pair in chatList)
                {
                    if (pair.Value == accountFrom)
                    {
                        Button_Perform_Click(pair.Key);
                    }
                }
            }
        }


        private void ShowMessage()
        {
            while (!RECEIVE_DONE)
            {
                object obj = formatter.Deserialize(stream);//????

                if (obj is MessageType.Message)
                {
                    MessageType.Message message = obj as MessageType.Message;
                    string messageFrom = message.AccountFrom;
                    if (message.Data is string)
                    {
                        string messageData = message.Data as string;
                        if (panel_Chat_Area.Visible == true && panel_Chat_Area.Enabled == true
                            && label_Chater_Account.Text == message.AccountFrom)
                        {
                            Update_RichBox(message.AccountFrom + ": \n"
                                + message.Data + "\n");
                        }
                        else
                        {
                            Received_Message_Invitation(messageFrom, messageData);
                        }
                    }


                }
                else if (obj is Message_Sent_Result)
                {
                    Message_Sent_Result message_Sent_Result = obj as Message_Sent_Result;
                    switch (message_Sent_Result.Result)
                    {
                        case true:
                            Chats[account_Talking_to].Add("U" + message);
                            Update_RichBox("You: \n"
                                        + message + "\n");
                            break;
                        case false:
                            MessageBox.Show(message_Sent_Result.ErrorMessage);
                            if (panel_Chat_Area.Visible == true && panel_Chat_Area.Enabled == true
                                && label_Chater_Account.Text == account_right_Clicked)
                            {
                                Button_Perform_Click(button_Close_Chat);
                            }
                            Chats.Remove(account_right_Clicked);
                            foreach (KeyValuePair<Button, string> pair in chatList)
                            {
                                if (pair.Value == account_right_Clicked)
                                {
                                    chatList.Remove(pair.Key);
                                    break;
                                }
                            }

                            Refresh_Panel_ChatList();
                            break;
                    }
                }

                else if (obj is AddChat_Result)
                {
                    AddChat_Result addChat_Result = obj as AddChat_Result;
                    switch (addChat_Result.Result)
                    {
                        case true:
                            if (!Chats.ContainsKey(addChat_Result.Addchat.TargetAccount) && addChat_Result.Addchat.TargetAccount == accountThis)
                            {
                                MessageBox.Show("You cannot add yourself as a chater...");
                            }
                            else if (Chats.ContainsKey(addChat_Result.Addchat.TargetAccount) && addChat_Result.Addchat.TargetAccount != accountThis)
                            {
                                MessageBox.Show("The user: " + addChat_Result.Addchat.TargetAccount + " has already in your Chat List...");
                            }
                            else
                            {
                                Chats.Add(addChat_Result.Addchat.TargetAccount, new List<string>());
                                Refresh_Panel_ChatList();
                            }
                            break;
                        case false:
                            MessageBox.Show(string.Format("You can't add an offline user {0} as chater...", addChat_Result.Addchat.TargetAccount));
                            break;
                    }
                }

                else if (obj is Game_Invitation)
                {
                    Game_Invitation game_invitation = obj as Game_Invitation;

                    Received_Message_Invitation(game_invitation.Inviter, string.Format("{0} invites you a game...", game_invitation.Inviter));

                    DialogResult result = MessageBox.Show(string.Format(game_invitation.Inviter + " invites you for a game."), "Accept", MessageBoxButtons.YesNoCancel);
                    if (result == DialogResult.Yes)
                    {
                        formatter.Serialize(stream, new Game_Request_Result(game_invitation.Inviter, game_invitation.Invitation_Reveiver, true, "", ""));
                    }
                    else if (result == DialogResult.No)
                    {
                        formatter.Serialize(stream, new Game_Request_Result(game_invitation.Inviter, game_invitation.Invitation_Reveiver, false, "", ""));
                    }
                }

                else if (obj is Game_Invitation_Result)
                {
                    Game_Invitation_Result game_invitation_Result = obj as Game_Invitation_Result;
                    if (game_invitation_Result.Result)
                    {
                        Show_Awaiting();
                    }
                    else if (!game_invitation_Result.Result)
                    {
                        MessageBox.Show(game_invitation_Result.Error);
                    }
                }
                else if (obj is Game_Request_Result)
                {
                    Game_Request_Result game_Request_Result = obj as Game_Request_Result;
                    if (game_Request_Result.Begin)
                    {
                        awaiting.Update_Content("Initializing Game Resources....");
                        Thread.Sleep(1000);
                        Close_Awaiting();
                        Game form = new Game(stream, accountThis, game_Request_Result.First, game_Request_Result.Second);
                        form.ShowDialog();
                        //form.Show();

                    }
                    else if (!game_Request_Result.Begin)
                    {
                        awaiting.Update_Content(game_Request_Result.Accepter + " refuses your game request.");
                        awaiting.Enable_Button();
                    }
                }
            }
        }

        delegate void Show_AwaitingDEL();
        private void Show_Awaiting()
        {
            if (InvokeRequired)
            {
                Show_AwaitingDEL del = Show_Awaiting;
                awaiting.Invoke(del);
            }
            else
            {
                awaiting.Show();
            }
        }
        delegate void Close_AwaitingDEL();
        private void Close_Awaiting()
        {
            if (InvokeRequired)
            {
                Close_AwaitingDEL del = Close_Awaiting;
                awaiting.Invoke(del);
            }
            else
            {
                awaiting.Close();
            }
        }

        private delegate void Update_RichBox_DEL(string message);
        private void Update_RichBox(string message)
        {
            if (InvokeRequired)
            {
                Update_RichBox_DEL del = Update_RichBox;
                richTextBox_chats.Invoke(del, message);
            }
            else
            {
                richTextBox_chats.AppendText(message);
                richTextBox_chats.ScrollToCaret();
            }
        }
        private void button_LogOut_Click(object sender, EventArgs e)
        {
            formatter.Serialize(stream, new Quit(accountThis));
            RECEIVE_DONE = true;
            this.Close();
        }

        private void Refresh_Panel_ChatList()
        {
            panel_ChatList.Controls.Clear();
            List<string> list = new List<string>(Chats.Keys);
            int W = 90;
            int H = 30;
            int MARGIN = 10;
            for (int i = 0; i < list.Count; i++)
            {
                Button button = new Button();
                button.Text = list[i];
                button.Size = new Size(W, H);
                button.BackColor = Color.White;///
                button.Location = new Point(MARGIN, i * H + (i + 1) * MARGIN);
                button.MouseDown += Button_MouseDown;
                button.Click += Button_Click;
                chatList[button] = list[i];
                AddButton(button);
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            account_Talking_to = chatList[button];
            panel_Chat_Area.Visible = true;
            panel_Chat_Area.Enabled = true;

            label_Chater_Account.Text = chatList[button];
            richTextBox_chats.Clear();
            for (int i = 0; i < Chats[chatList[button]].Count; i++)
            {
                string line = Chats[chatList[button]][i];
                if (line[0] == 'R')
                {
                    richTextBox_chats.AppendText(chatList[button] + ": \n"
                        + Chats[chatList[button]][i].Substring(1) + "\n");
                }
                else if (line[0] == 'U')
                {
                    richTextBox_chats.AppendText("You: \n"
                        + Chats[chatList[button]][i].Substring(1) + "\n");
                }
            }
        }

        private delegate void Clear_Panel_del();
        private void Clear_Panel()
        {
            if (InvokeRequired)
            {
                Clear_Panel_del del = Clear_Panel;
                panel_ChatList.Invoke(del);
            }
            else
            {
                panel_ChatList.Controls.Clear();
            }

        }

        private void Button_MouseDown(object sender, MouseEventArgs e)
        {
            Button button = sender as Button;
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu menu = new ContextMenu();
                account_right_Clicked = chatList[button];
                menu.MenuItems.Add("Delect", new EventHandler(Delete_Chaters));
                menu.MenuItems.Add("Invite Game", new EventHandler(Invite_Game));
                button.ContextMenu = menu;
            }
        }


        private delegate void AddButtonDEL(Button button);
        private void AddButton(Button button)
        {
            if (InvokeRequired)
            {
                AddButtonDEL del = AddButton;
                panel_ChatList.Invoke(del, button);
            }
            else
            {
                panel_ChatList.Controls.Add(button);
            }

        }

        private delegate void Button_Perform_Click_del(Button button);
        private void Button_Perform_Click(Button button)
        {
            if (InvokeRequired)
            {
                Button_Perform_Click_del del = Button_Perform_Click;
                button.Invoke(del, button);
            }
            else
            {
                button.PerformClick();
            }
        }
        private void Invite_Game(object sender, EventArgs e)
        {
            formatter.Serialize(stream, new Game_Invitation(accountThis, account_right_Clicked));
            //waiting
        }
        private void Delete_Chaters(object sender, EventArgs e)
        {
            Button button = null;
            if (panel_Chat_Area.Visible == true && panel_Chat_Area.Enabled == true
                            && label_Chater_Account.Text == account_right_Clicked)
            {
                Button_Perform_Click(button_Close_Chat);
            }
            Chats.Remove(account_right_Clicked);
            foreach (KeyValuePair<Button, string> pair in chatList)
            {
                if (pair.Value == account_right_Clicked)
                {
                    button = pair.Key;
                }
            }

            chatList.Remove(button);

            Refresh_Panel_ChatList();
        }

        private void button_Chat_Send_Click(object sender, EventArgs e)
        {
            message = textBox_Chat_Input.Text;
            formatter.Serialize(stream, new MessageType.Message(message, accountThis, account_Talking_to));
            textBox_Chat_Input.Clear();
        }

        private void button_Close_Chat_Click(object sender, EventArgs e)
        {
            panel_Chat_Area.Enabled = false;
            panel_Chat_Area.Visible = false;

            richTextBox_chats.Clear();
            label_Chater_Account.Text = "";
        }

        private void MainPage_Load(object sender, EventArgs e)
        {
            CenterToScreen();
            panel_Chat_Area.Visible = false;
            panel_Chat_Area.Enabled = false;
            label_UserAccount.Text = accountThis;
            button_LogOut.DialogResult = DialogResult.Cancel;
            Thread thread = new Thread(new ThreadStart(ShowMessage));
            thread.Start();

            this.FormClosing += MainPage_FormClosing;

        }

        private void MainPage_FormClosing(object sender, FormClosingEventArgs e)
        {
            formatter.Serialize(stream, new Quit(accountThis));
            RECEIVE_DONE = true;
        }

        private void textBox_Chat_Input_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                Button_Perform_Click(button_Chat_Send);
            }
        }
    }
}
