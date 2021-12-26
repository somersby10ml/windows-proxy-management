using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace windows_proxy_management
{
    public partial class ProxyForm : Form
    {
        public Proxy _proxy;

        public ProxyForm()
        {
            InitializeComponent();
            _proxy = new Proxy();
        }

        public ProxyForm(Proxy proxy)
        {
            InitializeComponent();
            _proxy = proxy;
        }

        public void SetProxyData(Proxy proxy)
        {
            _proxy = proxy;
        }

        public void SetMultiMode(bool isMultiMode = false)
        {
            if (isMultiMode)
            {
                txtSrcaddr.Enabled = false;
                txtSrcport.Enabled = false;
                txtDestport.Enabled = false;
                txtDestaddr.Enabled = false;
                txtMemo.Enabled = false;
            }
        }

        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case (Keys.Enter):
                    button1.PerformClick();
                    return true;

                case (Keys.Escape):
                    DialogResult = DialogResult.Cancel;
                    Close();
                    return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void ProxyForm_Load(object sender, EventArgs e)
        {
            txtSrcaddr.Text = _proxy.srcAddr;
            txtSrcport.Text = _proxy.srcPort;
            txtDestaddr.Text = _proxy.destAddr;
            txtDestport.Text = _proxy.destPort;
            cbType.Text = _proxy.type;
            txtMemo.Text = _proxy.memo;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _proxy.srcAddr = txtSrcaddr.Text;
            _proxy.srcPort = txtSrcport.Text;
            _proxy.destAddr = txtDestaddr.Text;
            _proxy.destPort = txtDestport.Text;
            _proxy.type = cbType.Text;
            _proxy.memo = txtMemo.Text;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
