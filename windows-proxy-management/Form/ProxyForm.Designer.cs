namespace windows_proxy_management
{
    partial class ProxyForm
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
            this.txtSrcport = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDestaddr = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDestport = new System.Windows.Forms.TextBox();
            this.cbType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtMemo = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txtSrcaddr = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtSrcport
            // 
            this.txtSrcport.Location = new System.Drawing.Point(78, 48);
            this.txtSrcport.Name = "txtSrcport";
            this.txtSrcport.Size = new System.Drawing.Size(161, 23);
            this.txtSrcport.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "src port";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(280, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "dest addr";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(253, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(19, 15);
            this.label3.TabIndex = 3;
            this.label3.Text = "➔";
            // 
            // txtDestaddr
            // 
            this.txtDestaddr.Location = new System.Drawing.Point(343, 17);
            this.txtDestaddr.Name = "txtDestaddr";
            this.txtDestaddr.Size = new System.Drawing.Size(161, 23);
            this.txtDestaddr.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(282, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 15);
            this.label4.TabIndex = 5;
            this.label4.Text = "dest port";
            // 
            // txtDestport
            // 
            this.txtDestport.Location = new System.Drawing.Point(343, 48);
            this.txtDestport.Name = "txtDestport";
            this.txtDestport.Size = new System.Drawing.Size(161, 23);
            this.txtDestport.TabIndex = 7;
            // 
            // cbType
            // 
            this.cbType.FormattingEnabled = true;
            this.cbType.Items.AddRange(new object[] {
            "WSL2",
            "SandBox",
            "Vmware"});
            this.cbType.Location = new System.Drawing.Point(78, 132);
            this.cbType.Name = "cbType";
            this.cbType.Size = new System.Drawing.Size(161, 23);
            this.cbType.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 106);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 15);
            this.label5.TabIndex = 9;
            this.label5.Text = "Memo";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(24, 135);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(32, 15);
            this.label6.TabIndex = 10;
            this.label6.Text = "Type";
            // 
            // txtMemo
            // 
            this.txtMemo.Location = new System.Drawing.Point(78, 103);
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.Size = new System.Drawing.Size(161, 23);
            this.txtMemo.TabIndex = 8;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(429, 111);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 44);
            this.button1.TabIndex = 12;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(24, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 15);
            this.label7.TabIndex = 14;
            this.label7.Text = "src addr";
            // 
            // txtSrcaddr
            // 
            this.txtSrcaddr.Location = new System.Drawing.Point(78, 17);
            this.txtSrcaddr.Name = "txtSrcaddr";
            this.txtSrcaddr.Size = new System.Drawing.Size(161, 23);
            this.txtSrcaddr.TabIndex = 13;
            // 
            // ProxyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.ClientSize = new System.Drawing.Size(517, 168);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtSrcaddr);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtMemo);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbType);
            this.Controls.Add(this.txtDestport);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtDestaddr);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSrcport);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProxyForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Proxy";
            this.Load += new System.EventHandler(this.ProxyForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox txtSrcport;
        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox txtDestaddr;
        private Label label4;
        private TextBox txtDestport;
        private ComboBox cbType;
        private Label label5;
        private Label label6;
        private TextBox txtMemo;
        private Button button1;
        private Label label7;
        private TextBox txtSrcaddr;
    }
}