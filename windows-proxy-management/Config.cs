using System.Text;
using System.Text.Json;

namespace windows_proxy_management
{
    static class Config
    {
        static public string Dir = "";
        static public string FilePath = "";

        static public void Init()
        {
            // %appdata%\Proxy Management
            Dir = Directory.GetCurrentDirectory();
            FilePath = Path.Combine(Dir, "default.json");
        }

        /// <summary>
        /// Load proxy data
        /// </summary>
        /// <returns>proxy data list</returns>
        static public List<Proxy> Load()
        {
            if (!Directory.Exists(Dir))
            {
                Directory.CreateDirectory(Dir);
            }

            List<Proxy> proxies = new();
            if (File.Exists(FilePath))
            {
                proxies = LoadData(FilePath) ?? new List<Proxy>();
            }
            return proxies;
        }

        /// <summary>
        /// Save proxy data
        /// </summary>
        /// <param name="proxies">proxy data list</param>
        /// <returns>File saving result</returns>
        static public void Save(List<Proxy> proxies)
        {
            if (!Directory.Exists(Dir))
            {
                Directory.CreateDirectory(Dir);
            }
            string jsonString = ProxyUtil.ToJSON(proxies);
            File.WriteAllText(FilePath, jsonString, Encoding.UTF8);
        }

        /// <summary>
        /// Load proxy data
        /// </summary>
        /// <param name="FilePath">Proxy data filepath</param>
        /// <returns></returns>
        private static List<Proxy> LoadData(string FilePath)
        {
            string jsonString = File.ReadAllText(FilePath, Encoding.UTF8);
            List<Proxy> proxies = ProxyUtil.ToProxies(jsonString);
            return proxies;
        }


        //static public bool Save(ListView lv)
        //{
        //    proxies = lv.Items.Cast<ListViewItem>().Select(item => (Proxy)item.Tag).ToList();
        //    return Save();
        //}
        //static public bool Save()
        //{
        //    try
        //    {
        //        if (!Directory.Exists(Dir))
        //        {
        //            Directory.CreateDirectory(Dir);
        //        }

        //        //List<Proxy> save_proxies = new List<Proxy>();
        //        //save_proxies = proxies.Where(item => !(String.IsNullOrEmpty(item.type) && String.IsNullOrEmpty(item.memo))).ToList();
        //        string jsonString = JsonSerializer.Serialize(proxies);
        //        File.WriteAllText(FilePath, jsonString, Encoding.UTF8);
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }

        //}
        //static public bool SaveAllAs(ListView lv)
        //{
        //    proxies = lv.Items.Cast<ListViewItem>().Select(item => (Proxy)item.Tag).ToList();
        //    return SaveAllAs();
        //}
        //static public bool SaveAllAs()
        //{
        //    try
        //    {
        //        SaveFileDialog saveFileDialog = new SaveFileDialog();
        //        saveFileDialog.Filter = "json|*.json|All Files|*.*";
        //        saveFileDialog.Title = "Save Proxy Data";
        //        saveFileDialog.InitialDirectory = FilePath;
        //        saveFileDialog.DefaultExt = "json";
        //        saveFileDialog.FileName = DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".json";
        //        if (saveFileDialog.ShowDialog() == DialogResult.OK)
        //        {
        //            string fileName = saveFileDialog.FileName;
        //            string jsonString = JsonSerializer.Serialize(proxies);
        //            File.WriteAllText(fileName, jsonString, Encoding.UTF8);
        //        }
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }

        //}



    }
}
