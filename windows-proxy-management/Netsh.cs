using System.Diagnostics;

namespace windows_proxy_management
{
    internal class Netsh
    {
        Process p = new Process();
        int LastExitCode = 0;

        public Netsh()
        {
            // Netsh load (add speed up)
            p.StartInfo.FileName = "netsh.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            p.StandardInput.WriteLine("interface");
            p.StandardInput.WriteLine("portproxy");
        }

        ~Netsh()
        {
            p.Close();
        }
        public void Reset()
        {
            p.StandardInput.WriteLine("reset");
        }

        public void Insert(Proxy proxy)
        {
            p.StandardInput.WriteLine($"add v4tov4 listenport={proxy.srcPort} connectaddress={proxy.destAddr} connectport={proxy.destPort}");
        }

        public List<Proxy>? GetProxy()
        {
            List<Proxy> proxies = new List<Proxy>();
            Process p = new Process();
            p.StartInfo.FileName = "netsh.exe";
            p.StartInfo.Arguments = "interface portproxy show v4tov4";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            p.WaitForExit();
            string output = p.StandardOutput.ReadToEnd();
            if (p.ExitCode != 0)
            {
                LastExitCode = p.ExitCode;
                return null;
            }
            p.Close();

            string[] lines = output.Split("\n");
            bool isSeparator = false;
            foreach (string aa in lines)
            {
                if (isSeparator && aa.Trim().Length > 5)
                {
                    string[] word = aa.Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                    string srcAddr = word[0];
                    string srcPort = word[1];
                    string destAddr = word[2];
                    string destPort = word[3];
                    proxies.Add(new Proxy(true, srcAddr, srcPort, destAddr, destPort));
                }

                if (!isSeparator)
                    if (aa.Length > 2 && aa.Substring(0, 2) == "--")
                        isSeparator = true;
            }
            return proxies;
        }

        public string GetWSLIP()
        {
            /*
                wsl.exe hostname -I
                wsl -- ip -o -4 -json address list eth0
                bash.exe -c "ifconfig eth0 | grep 'inet '
            */
            string output = "";
            List<Proxy> proxies = new();
            Process p = new Process();
            p.StartInfo.FileName = "wsl.exe";
            p.StartInfo.Arguments = "hostname -I";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            p.WaitForExit();
            output = p.StandardOutput.ReadToEnd();
            if (p.ExitCode != 0)
            {
                LastExitCode = p.ExitCode;
                return "";
            }
            p.Close();

            return output.Trim();
        }


        public int GetLastExitCode()
        {
            return LastExitCode;
        }

    }
}
