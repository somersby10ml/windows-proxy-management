namespace windows_proxy_management
{
    partial class Main
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
            this.components = new System.ComponentModel.Container();
            this.btnApply = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader_Active = new System.Windows.Forms.ColumnHeader();
            this.columnHeader_Src = new System.Windows.Forms.ColumnHeader();
            this.columnHeader_arrow = new System.Windows.Forms.ColumnHeader();
            this.columnHeader_Dest = new System.Windows.Forms.ColumnHeader();
            this.columnHeader_Type = new System.Windows.Forms.ColumnHeader();
            this.columnHeader_Memo = new System.Windows.Forms.ColumnHeader();
            this.columnHeader_Noti = new System.Windows.Forms.ColumnHeader();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.insertToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modifyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deletToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RestoreStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wSLToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.homepageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnGetStatus = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(573, 77);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(113, 43);
            this.btnApply.TabIndex = 0;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // listView1
            // 
            this.listView1.CheckBoxes = true;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader_Active,
            this.columnHeader_Src,
            this.columnHeader_arrow,
            this.columnHeader_Dest,
            this.columnHeader_Type,
            this.columnHeader_Memo,
            this.columnHeader_Noti});
            this.listView1.ContextMenuStrip = this.contextMenuStrip1;
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(12, 28);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(555, 305);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView1_ColumnClick);
            this.listView1.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.listView1_ItemCheck);
            this.listView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listView1_KeyDown);
            // 
            // columnHeader_Active
            // 
            this.columnHeader_Active.Text = "Active";
            this.columnHeader_Active.Width = 50;
            // 
            // columnHeader_Src
            // 
            this.columnHeader_Src.Text = "Src";
            this.columnHeader_Src.Width = 70;
            // 
            // columnHeader_arrow
            // 
            this.columnHeader_arrow.Text = " ";
            this.columnHeader_arrow.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader_arrow.Width = 30;
            // 
            // columnHeader_Dest
            // 
            this.columnHeader_Dest.Text = "Dest";
            this.columnHeader_Dest.Width = 160;
            // 
            // columnHeader_Type
            // 
            this.columnHeader_Type.Text = "Type";
            // 
            // columnHeader_Memo
            // 
            this.columnHeader_Memo.Text = "Memo";
            this.columnHeader_Memo.Width = 90;
            // 
            // columnHeader_Noti
            // 
            this.columnHeader_Noti.Text = "Notice";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.insertToolStripMenuItem,
            this.modifyToolStripMenuItem,
            this.deletToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 70);
            // 
            // insertToolStripMenuItem
            // 
            this.insertToolStripMenuItem.Name = "insertToolStripMenuItem";
            this.insertToolStripMenuItem.ShortcutKeyDisplayString = "INS";
            this.insertToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.insertToolStripMenuItem.Text = "Insert";
            this.insertToolStripMenuItem.Click += new System.EventHandler(this.insertToolStripMenuItem_Click);
            // 
            // modifyToolStripMenuItem
            // 
            this.modifyToolStripMenuItem.Name = "modifyToolStripMenuItem";
            this.modifyToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+E";
            this.modifyToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.modifyToolStripMenuItem.Text = "Modify";
            this.modifyToolStripMenuItem.Click += new System.EventHandler(this.modifyToolStripMenuItem_Click);
            // 
            // deletToolStripMenuItem
            // 
            this.deletToolStripMenuItem.Name = "deletToolStripMenuItem";
            this.deletToolStripMenuItem.ShortcutKeyDisplayString = "DEL";
            this.deletToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.deletToolStripMenuItem.Text = "Delete";
            this.deletToolStripMenuItem.Click += new System.EventHandler(this.deletToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.homepageToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(695, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reloadToolStripMenuItem,
            this.RestoreStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.settingToolStripMenuItem});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.menuToolStripMenuItem.Text = "&File";
            // 
            // reloadToolStripMenuItem
            // 
            this.reloadToolStripMenuItem.Name = "reloadToolStripMenuItem";
            this.reloadToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+R";
            this.reloadToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.reloadToolStripMenuItem.Text = "Reload";
            this.reloadToolStripMenuItem.Click += new System.EventHandler(this.reloadToolStripMenuItem_Click);
            // 
            // RestoreStripMenuItem
            // 
            this.RestoreStripMenuItem.Name = "RestoreStripMenuItem";
            this.RestoreStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.RestoreStripMenuItem.Text = "Restore";
            this.RestoreStripMenuItem.Click += new System.EventHandler(this.restoreToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+S";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveAsToolStripMenuItem.Text = "Save as ...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // settingToolStripMenuItem
            // 
            this.settingToolStripMenuItem.Name = "settingToolStripMenuItem";
            this.settingToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.settingToolStripMenuItem.Text = "Setting";
            this.settingToolStripMenuItem.Click += new System.EventHandler(this.settingToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.wSLToolStripMenuItem1});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(42, 20);
            this.toolsToolStripMenuItem.Text = "&Tool";
            // 
            // wSLToolStripMenuItem1
            // 
            this.wSLToolStripMenuItem1.Name = "wSLToolStripMenuItem1";
            this.wSLToolStripMenuItem1.Size = new System.Drawing.Size(98, 22);
            this.wSLToolStripMenuItem1.Text = "WSL";
            this.wSLToolStripMenuItem1.Click += new System.EventHandler(this.wSLToolStripMenuItem1_Click);
            // 
            // homepageToolStripMenuItem
            // 
            this.homepageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.homepageToolStripMenuItem.Name = "homepageToolStripMenuItem";
            this.homepageToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.homepageToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // btnGetStatus
            // 
            this.btnGetStatus.Location = new System.Drawing.Point(573, 28);
            this.btnGetStatus.Name = "btnGetStatus";
            this.btnGetStatus.Size = new System.Drawing.Size(113, 43);
            this.btnGetStatus.TabIndex = 5;
            this.btnGetStatus.Text = "Current Status";
            this.btnGetStatus.UseVisualStyleBackColor = true;
            this.btnGetStatus.Click += new System.EventHandler(this.btnGetStatus_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(695, 346);
            this.Controls.Add(this.btnGetStatus);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.menuStrip1);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Windows Proxy Mangement";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.contextMenuStrip1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button btnApply;
        private ListView listView1;
        private ColumnHeader columnHeader_Src;
        private ColumnHeader columnHeader_Dest;
        private ColumnHeader columnHeader_Memo;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem menuToolStripMenuItem;
        private ColumnHeader columnHeader_Active;
        private ColumnHeader columnHeader_arrow;
        private ColumnHeader columnHeader_Type;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem RestoreStripMenuItem;
        private ColumnHeader columnHeader_Noti;
        private ToolStripMenuItem saveAsToolStripMenuItem;
        private ToolStripMenuItem settingToolStripMenuItem;
        private Button btnGetStatus;
        private ToolStripMenuItem reloadToolStripMenuItem;
        private ToolStripMenuItem toolsToolStripMenuItem;
        private ToolStripMenuItem wSLToolStripMenuItem1;
        private ToolStripMenuItem homepageToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem insertToolStripMenuItem;
        private ToolStripMenuItem modifyToolStripMenuItem;
        private ToolStripMenuItem deletToolStripMenuItem;
    }
}