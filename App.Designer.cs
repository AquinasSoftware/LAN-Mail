namespace LAN_Mail
{
    partial class App
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            step1Lbl = new Label();
            nameLbl = new Label();
            ipLbl = new Label();
            nameTbx = new TextBox();
            ipTbx = new TextBox();
            startBtn = new Button();
            recipientLbl = new Label();
            recipientTbx = new TextBox();
            sendBtn = new Button();
            convoTbx = new RichTextBox();
            messageTbx = new TextBox();
            connectBtn = new Button();
            SuspendLayout();
            // 
            // step1Lbl
            // 
            step1Lbl.AutoSize = true;
            step1Lbl.Font = new Font("Segoe UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            step1Lbl.Location = new Point(12, 9);
            step1Lbl.Name = "step1Lbl";
            step1Lbl.Size = new Size(262, 38);
            step1Lbl.TabIndex = 0;
            step1Lbl.Text = "Sender Information:";
            // 
            // nameLbl
            // 
            nameLbl.AutoSize = true;
            nameLbl.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            nameLbl.Location = new Point(12, 57);
            nameLbl.Name = "nameLbl";
            nameLbl.Size = new Size(80, 31);
            nameLbl.TabIndex = 1;
            nameLbl.Text = "Name:";
            // 
            // ipLbl
            // 
            ipLbl.AutoSize = true;
            ipLbl.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ipLbl.Location = new Point(252, 58);
            ipLbl.Name = "ipLbl";
            ipLbl.Size = new Size(150, 31);
            ipLbl.TabIndex = 2;
            ipLbl.Text = "IPv4 Address:";
            // 
            // nameTbx
            // 
            nameTbx.Location = new Point(98, 60);
            nameTbx.MaxLength = 10;
            nameTbx.Name = "nameTbx";
            nameTbx.Size = new Size(148, 27);
            nameTbx.TabIndex = 3;
            // 
            // ipTbx
            // 
            ipTbx.Location = new Point(399, 62);
            ipTbx.MaxLength = 16;
            ipTbx.Name = "ipTbx";
            ipTbx.Size = new Size(168, 27);
            ipTbx.TabIndex = 4;
            // 
            // startBtn
            // 
            startBtn.Location = new Point(573, 61);
            startBtn.Name = "startBtn";
            startBtn.Size = new Size(94, 29);
            startBtn.TabIndex = 5;
            startBtn.Text = "Start";
            startBtn.UseVisualStyleBackColor = true;
            startBtn.Click += startBtn_Clicked;
            // 
            // recipientLbl
            // 
            recipientLbl.AutoSize = true;
            recipientLbl.Enabled = false;
            recipientLbl.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            recipientLbl.Location = new Point(12, 107);
            recipientLbl.Name = "recipientLbl";
            recipientLbl.Size = new Size(139, 31);
            recipientLbl.TabIndex = 7;
            recipientLbl.Text = "Recipient IP:";
            // 
            // recipientTbx
            // 
            recipientTbx.Enabled = false;
            recipientTbx.Location = new Point(157, 111);
            recipientTbx.MaxLength = 16;
            recipientTbx.Name = "recipientTbx";
            recipientTbx.Size = new Size(168, 27);
            recipientTbx.TabIndex = 8;
            // 
            // sendBtn
            // 
            sendBtn.Enabled = false;
            sendBtn.Location = new Point(573, 420);
            sendBtn.Name = "sendBtn";
            sendBtn.Size = new Size(94, 29);
            sendBtn.TabIndex = 10;
            sendBtn.Text = "Send";
            sendBtn.UseVisualStyleBackColor = true;
            sendBtn.Click += sendBtn_Click;
            // 
            // convoTbx
            // 
            convoTbx.Enabled = false;
            convoTbx.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            convoTbx.Location = new Point(12, 161);
            convoTbx.Name = "convoTbx";
            convoTbx.ReadOnly = true;
            convoTbx.ScrollBars = RichTextBoxScrollBars.Vertical;
            convoTbx.Size = new Size(655, 241);
            convoTbx.TabIndex = 11;
            convoTbx.Text = "";
            // 
            // messageTbx
            // 
            messageTbx.Enabled = false;
            messageTbx.Location = new Point(12, 422);
            messageTbx.MaxLength = 120;
            messageTbx.Name = "messageTbx";
            messageTbx.Size = new Size(555, 27);
            messageTbx.TabIndex = 12;
            // 
            // connectBtn
            // 
            connectBtn.Location = new Point(344, 110);
            connectBtn.Name = "connectBtn";
            connectBtn.Size = new Size(94, 29);
            connectBtn.TabIndex = 13;
            connectBtn.Text = "Connect";
            connectBtn.UseVisualStyleBackColor = true;
            connectBtn.Click += connectBtn_Click;
            // 
            // App
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(685, 470);
            Controls.Add(connectBtn);
            Controls.Add(messageTbx);
            Controls.Add(convoTbx);
            Controls.Add(sendBtn);
            Controls.Add(recipientTbx);
            Controls.Add(recipientLbl);
            Controls.Add(startBtn);
            Controls.Add(ipTbx);
            Controls.Add(nameTbx);
            Controls.Add(ipLbl);
            Controls.Add(nameLbl);
            Controls.Add(step1Lbl);
            Name = "App";
            Text = "LAN Messenger";
            FormClosed += App_FormClosed;
            Load += App_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label step1Lbl;
        private Label nameLbl;
        private Label ipLbl;
        private TextBox nameTbx;
        private TextBox ipTbx;
        private Button startBtn;
        private Label recipientLbl;
        private TextBox recipientTbx;
        private Button sendBtn;
        private RichTextBox convoTbx;
        private TextBox messageTbx;
        private Button connectBtn;
    }
}
