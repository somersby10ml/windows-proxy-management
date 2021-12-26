namespace windows_proxy_management
{
    partial class Setting
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkWSL2 = new System.Windows.Forms.CheckBox();
            this.chkRunNormal = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkWSL2);
            this.groupBox1.Controls.Add(this.chkRunNormal);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(164, 90);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "AutoRun";
            // 
            // chkWSL2
            // 
            this.chkWSL2.AutoSize = true;
            this.chkWSL2.Location = new System.Drawing.Point(15, 46);
            this.chkWSL2.Name = "chkWSL2";
            this.chkWSL2.Size = new System.Drawing.Size(57, 19);
            this.chkWSL2.TabIndex = 0;
            this.chkWSL2.Text = "WSL2";
            this.chkWSL2.UseVisualStyleBackColor = true;
            this.chkWSL2.Click += new System.EventHandler(this.chkWSL2_Click);
            // 
            // chkRunNormal
            // 
            this.chkRunNormal.AutoSize = true;
            this.chkRunNormal.Location = new System.Drawing.Point(15, 21);
            this.chkRunNormal.Name = "chkRunNormal";
            this.chkRunNormal.Size = new System.Drawing.Size(66, 19);
            this.chkRunNormal.TabIndex = 3;
            this.chkRunNormal.Text = "Normal";
            this.chkRunNormal.UseVisualStyleBackColor = true;
            this.chkRunNormal.Click += new System.EventHandler(this.chkRunNormal_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(99, 108);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(77, 45);
            this.button1.TabIndex = 1;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(16, 108);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(77, 45);
            this.button2.TabIndex = 2;
            this.button2.Text = "Help";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Setting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(200, 164);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Setting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Setting";
            this.Load += new System.EventHandler(this.Setting_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private GroupBox groupBox1;
        private CheckBox chkWSL2;
        private Button button1;
        private CheckBox chkRunNormal;
        private Button button2;
    }
}