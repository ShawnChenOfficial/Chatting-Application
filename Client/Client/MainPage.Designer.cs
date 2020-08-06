namespace Client
{
    partial class MainPage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel_Chat_Area = new System.Windows.Forms.Panel();
            this.button_Close_Chat = new System.Windows.Forms.Button();
            this.label_Chater_Account = new System.Windows.Forms.Label();
            this.button_Chat_Send = new System.Windows.Forms.Button();
            this.textBox_Chat_Input = new System.Windows.Forms.TextBox();
            this.panel_Chat = new System.Windows.Forms.Panel();
            this.richTextBox_chats = new System.Windows.Forms.RichTextBox();
            this.button_AddNewChater = new System.Windows.Forms.Button();
            this.label_UserAccount = new System.Windows.Forms.Label();
            this.button_LogOut = new System.Windows.Forms.Button();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.panel_ChatList = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel_Chat_Area.SuspendLayout();
            this.panel_Chat.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_Chat_Area
            // 
            this.panel_Chat_Area.BackColor = System.Drawing.SystemColors.Control;
            this.panel_Chat_Area.Controls.Add(this.button_Close_Chat);
            this.panel_Chat_Area.Controls.Add(this.label_Chater_Account);
            this.panel_Chat_Area.Controls.Add(this.button_Chat_Send);
            this.panel_Chat_Area.Controls.Add(this.textBox_Chat_Input);
            this.panel_Chat_Area.Controls.Add(this.panel_Chat);
            this.panel_Chat_Area.Location = new System.Drawing.Point(135, 12);
            this.panel_Chat_Area.Name = "panel_Chat_Area";
            this.panel_Chat_Area.Size = new System.Drawing.Size(345, 314);
            this.panel_Chat_Area.TabIndex = 5;
            // 
            // button_Close_Chat
            // 
            this.button_Close_Chat.Location = new System.Drawing.Point(320, 0);
            this.button_Close_Chat.Name = "button_Close_Chat";
            this.button_Close_Chat.Size = new System.Drawing.Size(22, 20);
            this.button_Close_Chat.TabIndex = 11;
            this.button_Close_Chat.Text = "X";
            this.button_Close_Chat.UseVisualStyleBackColor = true;
            this.button_Close_Chat.Click += new System.EventHandler(this.button_Close_Chat_Click);
            // 
            // label_Chater_Account
            // 
            this.label_Chater_Account.AutoSize = true;
            this.label_Chater_Account.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Chater_Account.Location = new System.Drawing.Point(3, 5);
            this.label_Chater_Account.Name = "label_Chater_Account";
            this.label_Chater_Account.Size = new System.Drawing.Size(95, 13);
            this.label_Chater_Account.TabIndex = 10;
            this.label_Chater_Account.Text = "Chater Account";
            // 
            // button_Chat_Send
            // 
            this.button_Chat_Send.Location = new System.Drawing.Point(267, 288);
            this.button_Chat_Send.Name = "button_Chat_Send";
            this.button_Chat_Send.Size = new System.Drawing.Size(75, 23);
            this.button_Chat_Send.TabIndex = 2;
            this.button_Chat_Send.Text = "Send";
            this.button_Chat_Send.UseVisualStyleBackColor = true;
            this.button_Chat_Send.Click += new System.EventHandler(this.button_Chat_Send_Click);
            // 
            // textBox_Chat_Input
            // 
            this.textBox_Chat_Input.BackColor = System.Drawing.Color.Lavender;
            this.textBox_Chat_Input.Location = new System.Drawing.Point(3, 211);
            this.textBox_Chat_Input.Multiline = true;
            this.textBox_Chat_Input.Name = "textBox_Chat_Input";
            this.textBox_Chat_Input.Size = new System.Drawing.Size(339, 72);
            this.textBox_Chat_Input.TabIndex = 1;
            this.textBox_Chat_Input.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_Chat_Input_KeyPress);
            // 
            // panel_Chat
            // 
            this.panel_Chat.BackColor = System.Drawing.SystemColors.Window;
            this.panel_Chat.Controls.Add(this.richTextBox_chats);
            this.panel_Chat.Location = new System.Drawing.Point(3, 21);
            this.panel_Chat.Name = "panel_Chat";
            this.panel_Chat.Size = new System.Drawing.Size(339, 184);
            this.panel_Chat.TabIndex = 0;
            // 
            // richTextBox_chats
            // 
            this.richTextBox_chats.BackColor = System.Drawing.Color.AntiqueWhite;
            this.richTextBox_chats.Location = new System.Drawing.Point(0, 3);
            this.richTextBox_chats.Name = "richTextBox_chats";
            this.richTextBox_chats.ReadOnly = true;
            this.richTextBox_chats.Size = new System.Drawing.Size(339, 181);
            this.richTextBox_chats.TabIndex = 0;
            this.richTextBox_chats.Text = "";
            // 
            // button_AddNewChater
            // 
            this.button_AddNewChater.Location = new System.Drawing.Point(12, 301);
            this.button_AddNewChater.Name = "button_AddNewChater";
            this.button_AddNewChater.Size = new System.Drawing.Size(117, 25);
            this.button_AddNewChater.TabIndex = 6;
            this.button_AddNewChater.Text = "Add new chater";
            this.button_AddNewChater.UseVisualStyleBackColor = true;
            this.button_AddNewChater.Click += new System.EventHandler(this.button_AddNewChater_Click);
            // 
            // label_UserAccount
            // 
            this.label_UserAccount.AutoSize = true;
            this.label_UserAccount.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_UserAccount.Location = new System.Drawing.Point(12, 12);
            this.label_UserAccount.Name = "label_UserAccount";
            this.label_UserAccount.Size = new System.Drawing.Size(117, 22);
            this.label_UserAccount.TabIndex = 7;
            this.label_UserAccount.Text = "User Account";
            // 
            // button_LogOut
            // 
            this.button_LogOut.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_LogOut.Location = new System.Drawing.Point(12, 36);
            this.button_LogOut.Name = "button_LogOut";
            this.button_LogOut.Size = new System.Drawing.Size(69, 21);
            this.button_LogOut.TabIndex = 8;
            this.button_LogOut.Text = "Log out";
            this.button_LogOut.UseVisualStyleBackColor = true;
            this.button_LogOut.Click += new System.EventHandler(this.button_LogOut_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(32, 19);
            // 
            // panel_ChatList
            // 
            this.panel_ChatList.BackColor = System.Drawing.Color.AntiqueWhite;
            this.panel_ChatList.Location = new System.Drawing.Point(4, 19);
            this.panel_ChatList.Name = "panel_ChatList";
            this.panel_ChatList.Size = new System.Drawing.Size(110, 201);
            this.panel_ChatList.TabIndex = 9;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.panel_ChatList);
            this.panel1.Location = new System.Drawing.Point(12, 72);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(117, 223);
            this.panel1.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Chaters";
            // 
            // MainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.button_LogOut;
            this.ClientSize = new System.Drawing.Size(492, 338);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button_LogOut);
            this.Controls.Add(this.panel_Chat_Area);
            this.Controls.Add(this.label_UserAccount);
            this.Controls.Add(this.button_AddNewChater);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainPage";
            this.Text = "Chat Life";
            this.Load += new System.EventHandler(this.MainPage_Load);
            this.Leave += new System.EventHandler(this.button_LogOut_Click);
            this.panel_Chat_Area.ResumeLayout(false);
            this.panel_Chat_Area.PerformLayout();
            this.panel_Chat.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel_Chat_Area;
        private System.Windows.Forms.Button button_AddNewChater;
        private System.Windows.Forms.Label label_UserAccount;
        private System.Windows.Forms.Button button_LogOut;
        private System.Windows.Forms.Button button_Chat_Send;
        private System.Windows.Forms.TextBox textBox_Chat_Input;
        private System.Windows.Forms.Panel panel_Chat;
        private System.Windows.Forms.Label label_Chater_Account;
        private System.Windows.Forms.RichTextBox richTextBox_chats;
        private System.Windows.Forms.Button button_Close_Chat;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.Panel panel_ChatList;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
    }
}