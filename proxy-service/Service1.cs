using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text;
namespace proxy_service
{
    public partial class Service1 : ServiceBase
    {

        public enum ServiceState
        {
            SERVICE_STOPPED = 0x00000001,
            SERVICE_START_PENDING = 0x00000002,
            SERVICE_STOP_PENDING = 0x00000003,
            SERVICE_RUNNING = 0x00000004,
            SERVICE_CONTINUE_PENDING = 0x00000005,
            SERVICE_PAUSE_PENDING = 0x00000006,
            SERVICE_PAUSED = 0x00000007,
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ServiceStatus
        {
            public int dwServiceType;
            public ServiceState dwCurrentState;
            public int dwControlsAccepted;
            public int dwWin32ExitCode;
            public int dwServiceSpecificExitCode;
            public int dwCheckPoint;
            public int dwWaitHint;
        };


        public class Proxy
        {
            public Proxy()
            {
                srcAddr = "";
                srcPort = "";
                destAddr = "";
                destPort = "";
                type = "";
                memo = "";
                active = false;
            }


            public Proxy(bool active, string srcAddr, string srcPort, string destAddr, string destPort, string type = "", string memo = "")
            {
                this.active = active;
                this.srcAddr = srcAddr;
                this.srcPort = srcPort;
                this.destAddr = destAddr;
                this.destPort = destPort;
                this.type = type;
                this.memo = memo;
            }

            public Proxy Clone()
            {
                Proxy proxy = new Proxy()
                {
                    active = this.active,
                    srcAddr = this.srcAddr,
                    srcPort = this.srcPort,
                    destAddr = this.destAddr,
                    destPort = this.destPort,
                    type = this.type,
                    memo = this.memo,
                };
                return proxy;
            }

            public bool active { get; set; }
            public string srcAddr { get; set; }
            public string srcPort { get; set; }
            public string destAddr { get; set; }
            public string destPort { get; set; }
            public string type { get; set; }
            public string memo { get; set; }

        }

        static class ProxyUtil
        {
            static public List<Proxy> ToProxies(string jsonString)
            {
                List<Proxy> proxies = JsonConvert.DeserializeObject<List<Proxy>>(jsonString);
                return proxies;
            }
        }

        [DllImport("advapi32.dll", SetLastError = true)]

        private static extern bool SetServiceStatus(System.IntPtr handle, ref ServiceStatus serviceStatus);
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            //base.OnStart(args);
            ServiceStatus serviceStatus = new ServiceStatus();
            serviceStatus.dwCurrentState = ServiceState.SERVICE_START_PENDING;
            serviceStatus.dwWaitHint = 100000;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);

            string Dir = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            string FilePath = Path.Combine(Dir, "default.json");

            serviceStatus.dwCurrentState = ServiceState.SERVICE_RUNNING;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);

            string jsonString = File.ReadAllText(FilePath, Encoding.UTF8);
            List<Proxy> proxies = ProxyUtil.ToProxies(jsonString);

            Process p = new Process();
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
            p.StandardInput.WriteLine("reset");

            foreach (Proxy proxy in proxies)
            {
                p.StandardInput.WriteLine($"add v4tov4 listenport={proxy.srcPort} connectaddress={proxy.destAddr} connectport={proxy.destPort}");
            }

            serviceStatus.dwCurrentState = ServiceState.SERVICE_STOP_PENDING;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);

            p.Close();

            serviceStatus.dwCurrentState = ServiceState.SERVICE_STOPPED;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);

            this.Stop();
        }

        protected override void OnStop()
        {

        }
    }
}
