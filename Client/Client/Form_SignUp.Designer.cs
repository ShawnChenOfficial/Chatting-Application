namespace Client
{
    partial class Form_SignUp
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
            this.label_Account = new System.Windows.Forms.Label();
            this.label_Password = new System.Windows.Forms.Label();
            this.label_R_Password = new System.Windows.Forms.Label();
            this.textBox_Account = new System.Windows.Forms.TextBox();
            this.textBox_Password1 = new System.Windows.Forms.TextBox();
            this.textBox_Password2 = new System.Windows.Forms.TextBox();
            this.button_OK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label_Account
            // 
            this.label_Account.AutoSize = true;
            this.label_Account.Location = new System.Drawing.Point(158, 93);
            this.label_Account.Name = "label_Account";
            this.label_Account.Size = new System.Drawing.Size(50, 13);
            this.label_Account.TabIndex = 0;
            this.label_Account.Text = "Account:";
            // 
            // label_Password
            // 
            this.label_Password.AutoSize = true;
            this.label_Password.Location = new System.Drawing.Point(152, 150);
            this.label_Password.Name = "label_Password";
            this.label_Password.Size = new System.Drawing.Size(56, 13);
            this.label_Password.TabIndex = 1;
            this.label_Password.Text = "Password:";
            // 
            // label_R_Password
            // 
            this.label_R_Password.AutoSize = true;
            this.label_R_Password.Location = new System.Drawing.Point(107, 204);
            this.label_R_Password.Name = "label_R_Password";
            this.label_R_Password.Size = new System.Drawing.Size(101, 13);
            this.label_R_Password.TabIndex = 2;
            this.label_R_Password.Text = "Re-Enter Password:";
            // 
            // textBox_Account
            // 
            this.textBox_Account.Location = new System.Drawing.Point(244, 90);
            this.textBox_Account.Name = "textBox_Account";
            this.textBox_Account.Size = new System.Drawing.Size(129, 20);
            this.textBox_Account.TabIndex = 3;
            // 
            // textBox_Password1
            // 
            this.textBox_Password1.Location = new System.Drawing.Point(244, 147);
            this.textBox_Password1.Name = "textBox_Password1";
            this.textBox_Password1.PasswordChar = '*';
            this.textBox_Password1.Size = new System.Drawing.Size(129, 20);
            this.textBox_Password1.TabIndex = 4;
            // 
            // textBox_Password2
            // 
            this.textBox_Password2.Location = new System.Drawing.Point(244, 201);
            this.textBox_Password2.Name = "textBox_Password2";
            this.textBox_Password2.PasswordChar = '*';
            this.textBox_Password2.Size = new System.Drawing.Size(129, 20);
            this.textBox_Password2.TabIndex = 5;
            // 
            // button_OK
            // 
            this.button_OK.Location = new System.Drawing.Point(208, 269);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(75, 23);
            this.button_OK.TabIndex = 6;
            this.button_OK.Text = "Ok";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // Form_SignUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 338);
            this.Controls.Add(this.button_OK);
            this.Controls.Add(this.textBox_Password2);
            this.Controls.Add(this.textBox_Password1);
            this.Controls.Add(this.textBox_Account);
            this.Controls.Add(this.label_R_Password);
            this.Controls.Add(this.label_Password);
            this.Controls.Add(this.label_Account);
            this.Name = "Form_SignUp";
            this.Text = "SignUp";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_Account;
        private System.Windows.Forms.Label label_Password;
        private System.Windows.Forms.Label label_R_Password;
        private System.Windows.Forms.TextBox textBox_Account;
        private System.Windows.Forms.TextBox textBox_Password1;
        private System.Windows.Forms.TextBox textBox_Password2;
        private System.Windows.Forms.Button button_OK;
    }
}