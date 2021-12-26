using System.Diagnostics;
using System.Reflection;

namespace windows_proxy_management
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        private void About_Load(object sender, EventArgs e)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            if (fvi.FileVersion != null)
            {
                string version = fvi.FileVersion;
                lblVersion.Text = version;
            }
        }

        private void linkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel? link = sender as LinkLabel;
            if (link != null)
                Process.Start("explorer.exe", link.Text);
        }
    }
}
