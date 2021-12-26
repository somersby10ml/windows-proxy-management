using System.Runtime.InteropServices;

// WSL 아이피 바꼈다는 팝업창 시작시에 뛰우기

namespace windows_proxy_management
{
    public partial class Setting : Form
    {

        Service service;

        public Setting()
        {
            InitializeComponent();
            service = new("ProxyManagement", "Proxy Management", Path.Combine(Directory.GetCurrentDirectory(), "proxy-service.exe"));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("[Autorun]\n" +
                "Normal - Show Window\n\n" +
                "WSL2 - silent & auto update wsl ip\n" +
                "  Registry: Show UAC(run as admin)\n" +
                "  Service: UAC window not showing", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Setting_Load(object sender, EventArgs e)
        {
            // 프로그램 시작시 경로체크
            chkRunNormal.Checked = Reg.Exist();

            // se.Install();
            chkWSL2.Checked = service.Exist();
            return;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (chkRunNormal.Checked)
                Reg.Register();
            else
                Reg.UnRegister();

            if (chkWSL2.Checked)
                service.Install();
            else
                service.Uninstall();


            MessageBox.Show("The settings have been applied.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }



        private void chkRunNormal_Click(object sender, EventArgs e)
        {


        }

        private void chkWSL2_Click(object sender, EventArgs e)
        {

        }
    }
}
