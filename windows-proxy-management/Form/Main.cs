/*
    Windows Proxy Management Tool
    git: https://github.com/somersby10ml/windows-proxy-management
 */

using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace windows_proxy_management
{
    public partial class Main : Form
    {
        Netsh netsh = new();
        bool IsElevated;
        int listview_mr_padding;
        int listview_mb_padding;
        int btnDelete_mr_padding;
        int btnGetStatus_mr_padding;

        private ListViewColumnSorter lvwColumnSorter = new ListViewColumnSorter();

        public Main()
        {
            InitializeComponent();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                // Control + S
                case (Keys.Control | Keys.S):
                    SaveProxyData();
                    MessageBox.Show(Config.FilePath + " save successful.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                case (Keys.Control | Keys.R):
                    listView1.Items.Clear();
                    List<Proxy> proxies = Config.Load();
                    AddListView(proxies);
                    break;
                case Keys.Insert:
                    ProxyForm insertForm = new()
                    {
                        Text = "Insert Proxy",
                        Owner = this
                    };
                    if (insertForm.ShowDialog() == DialogResult.OK)
                    {
                        Proxy proxy = new(true,
                            insertForm._proxy.srcAddr, insertForm._proxy.srcPort,
                            insertForm._proxy.destAddr, insertForm._proxy.destPort,
                            insertForm._proxy.type, insertForm._proxy.memo);
                        AddListView(proxy, true);
                        listView1.Items[listView1.Items.Count - 1].Focused = true;
                        listView1.Items[listView1.Items.Count - 1].Selected = true;
                        listView1.EnsureVisible(listView1.Items.Count - 1);
                    }
                    return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            if (fvi.FileVersion != null)
            {
                string version = fvi.FileVersion;
                this.Text = this.Text + " v" + version;
            }

            IsElevated = Util.IsAdministrator();
            if (IsElevated)
                this.Text = "[Elevated] " + this.Text;
            else
            {
                DialogResult msgbox = MessageBox.Show(
                    "Changes will not be saved unless you have administrator privileges (add/modify/delete).\n" +
                    "Are you sure you want to run it again with administrator privileges ?", "Notice", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msgbox == DialogResult.Yes)
                {
                    ProcessStartInfo proc = new();
                    proc.UseShellExecute = true;
                    proc.WorkingDirectory = Environment.CurrentDirectory;
                    proc.FileName = Application.ExecutablePath;
                    proc.Verb = "runas";
                    Process.Start(proc);
                    Environment.Exit(0);
                }
                btnApply.Enabled = false;
            }

            // Resize
            listview_mr_padding = this.Width - listView1.Width;
            listview_mb_padding = this.Height - listView1.Height;
            btnDelete_mr_padding = this.Width - btnApply.Left;
            btnGetStatus_mr_padding = this.Width - btnGetStatus.Left;
            Config.Init();
            List<Proxy> proxies = Config.Load();
            AddListView(proxies);
            listView1.ListViewItemSorter = lvwColumnSorter;
            ListViewAutoResizeColumns();
        }

        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == lvwColumnSorter.SortColumn)
            {
                lvwColumnSorter.Order =
                    lvwColumnSorter.Order == SortOrder.Ascending ?
                    SortOrder.Descending : SortOrder.Ascending;
            }
            else
            {
                lvwColumnSorter.SortColumn = e.Column;
                lvwColumnSorter.Order = SortOrder.Ascending;
            }

            listView1.Sort();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            netsh.Reset();
            SaveProxyData();
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                ProxyObject proxyObject = (ProxyObject)listView1.Items[i].Tag;
                if (proxyObject.first != null)
                {
                    if (proxyObject.first.active)
                        netsh.Insert(proxyObject.first);
                }
            }
            MessageBox.Show("Proxy settings have been applied.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            listView1.Width = this.Width - listview_mr_padding;
            listView1.Height = this.Height - listview_mb_padding;
            btnApply.Left = this.Width - btnDelete_mr_padding;
            btnGetStatus.Left = this.Width - btnGetStatus_mr_padding;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveProxyData();
            MessageBox.Show(Config.FilePath + " save successful.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void restoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Menu::Load
            OpenFileDialog openFileDialog = new()
            {
                Filter = "json|*.json|All Files|*.*",
                Title = "Save Proxy Data",
                InitialDirectory = Config.Dir,
                DefaultExt = "json"
            };
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;

            string fileName = openFileDialog.FileName;
            string jsonString = File.ReadAllText(fileName, System.Text.Encoding.UTF8);
            if (jsonString.Length == 0)
                return;

            List<Proxy>? proxies = JsonSerializer.Deserialize<List<Proxy>>(jsonString);
            if (proxies == null)
                return;

            if (listView1.Items.Count != 0)
            {
                DialogResult yesno = MessageBox.Show("The list item is deleted.\nDo you want to continue?",
                    "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (yesno == DialogResult.No)
                {
                    return;
                }
                listView1.Items.Clear();
            }

            listView1.BeginUpdate();
            foreach (Proxy proxy in proxies)
            {
                AddListView(proxy, true);
            }
            listView1.EndUpdate();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Menu::Save As...
            List<Proxy> proxies = GetProxyData();
            string jsonString = ProxyUtil.ToJSON(proxies);

            SaveFileDialog saveFileDialog = new()
            {
                Filter = "json|*.json|All Files|*.*",
                Title = "Save Proxy Data",
                InitialDirectory = Config.Dir,
                DefaultExt = "json",
                FileName = DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".json"
            };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = saveFileDialog.FileName;
                File.WriteAllText(fileName, jsonString, Encoding.UTF8);
            }
        }



        private void listView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
                return;

            switch (e.KeyData)
            {
                case (Keys.Control | Keys.A):
                    foreach (ListViewItem lvItem in listView1.Items)
                        lvItem.Selected = true;
                    break;

                case Keys.Delete:
                    ListviewSelectDelete();
                    break;

                case (Keys.Control | Keys.E):
                    if (listView1.SelectedItems.Count == 1)
                        ListviewSingleModify();
                    else
                        ListviewMultiModify();
                    break;
            }

        }

        private void btnGetStatus_Click(object sender, EventArgs e)
        {
            List<Proxy>? current_proxy = netsh.GetProxy();
            if (current_proxy == null)
            {
                MessageBox.Show(
                    "netsh.exe getProxy Error:" + netsh.GetLastExitCode().ToString(),
                    "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (listView1.Items.Count != 0)
            {
                DialogResult yesno = MessageBox.Show("The list item is deleted.\nDo you want to continue?",
                    "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (yesno == DialogResult.No)
                {
                    return;
                }
                listView1.Items.Clear();
            }

            listView1.BeginUpdate();
            foreach (Proxy proxy in current_proxy)
                AddListView(proxy);
            listView1.EndUpdate();
        }

        private void listView1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            ListViewItem item = listView1.Items[e.Index];
            if (item != null)
            {
                ProxyObject proxyObject = (ProxyObject)item.Tag;
                if (proxyObject.changed == null)
                {
                    if (proxyObject.first != null)
                        proxyObject.changed = proxyObject.first.Clone();
                }

                if (proxyObject.changed != null)
                    proxyObject.changed.active = e.NewValue == CheckState.Checked;

                ChangeProxyData(item, proxyObject);
            }
        }


        private void reloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Menu::Reload
            listView1.Items.Clear();
            List<Proxy> proxies = Config.Load();
            AddListView(proxies);
        }

        private void settingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Setting frmSetting = new();
            frmSetting.ShowDialog();
        }

        private void wSLToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                string wsl_ip = netsh.GetWSLIP();

                for (int i = 0; i < listView1.Items.Count; i++)
                {
                    if (listView1.Items[i].Tag is not ProxyObject proxyObject)
                    {
                        continue;
                    }

                    if (proxyObject.changed == null)
                    {
                        if (proxyObject.first is not null)
                            proxyObject.changed = proxyObject.first.Clone();
                    }

                    if (proxyObject.changed is not null)
                    {
                        if (proxyObject.changed.type != "WSL2")
                            continue;

                        proxyObject.changed.destAddr = wsl_ip;
                        continue;
                    }
                }
                ChangeProxyData();
            }
            catch
            {
                MessageBox.Show("wsl is not installed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About about = new();
            about.Owner = this;
            about.ShowDialog();
        }

        private void insertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProxyForm insertForm = new()
            {
                Text = "Insert Proxy",
                Owner = this
            };
            if (insertForm.ShowDialog() == DialogResult.OK)
            {
                Proxy proxy = new(true,
                    insertForm._proxy.srcAddr, insertForm._proxy.srcPort,
                    insertForm._proxy.destAddr, insertForm._proxy.destPort,
                    insertForm._proxy.type, insertForm._proxy.memo);
                AddListView(proxy, true);
                listView1.Items[listView1.Items.Count - 1].Focused = true;
                listView1.Items[listView1.Items.Count - 1].Selected = true;
                listView1.EnsureVisible(listView1.Items.Count - 1);
            }
        }

        private void modifyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 1)
                ListviewSingleModify();
            else
                ListviewMultiModify();
        }

        private void deletToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListviewSelectDelete();
        }
    }
}