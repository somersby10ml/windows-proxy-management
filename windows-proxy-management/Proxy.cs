using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

// [NotNull, DisallowNull]
namespace windows_proxy_management
{
    public enum ApplyType
    {
        NORMAL = 0,
        DELETE = 1,
        INSERT = 2,
        MODIFY = 3,
    }

    public class ProxyObject
    {
        public ProxyObject()
        {
        }
        public ProxyObject(Proxy? first)
        {
            this.first = first;
        }
        public Proxy? first = null;
        public Proxy? changed = null;
    }

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
            apply = ApplyType.NORMAL;
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
            this.apply = ApplyType.NORMAL;
        }

        public Proxy Clone()
        {
            Proxy proxy = new()
            {
                active = this.active,
                srcAddr = this.srcAddr,
                srcPort = this.srcPort,
                destAddr = this.destAddr,
                destPort = this.destPort,
                type = this.type,
                memo = this.memo,
                apply = this.apply
            };
            return proxy;
        }
        public bool Compare(object? obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (obj is not Proxy proxy)
            {
                return false;
            }

            return this.active == proxy.active &&
                 this.srcAddr == proxy.srcAddr &&
                 this.srcPort == proxy.srcPort &&
                 this.destAddr == proxy.destAddr &&
                 this.destPort == proxy.destPort &&
                 this.type == proxy.type &&
                 this.memo == proxy.memo &&
                 this.apply == proxy.apply;
        }


        public bool active { get; set; }
        public string srcAddr { get; set; }
        public string srcPort { get; set; }
        public string destAddr { get; set; }
        public string destPort { get; set; }
        public string type { get; set; }
        public string memo { get; set; }

        [JsonIgnore]
        public ApplyType apply { get; set; }
    }

    static class ProxyUtil
    {
        static public string ToJSON(List<Proxy> proxies)
        {
            JsonSerializerOptions jsonSerializerOptions = new();
            jsonSerializerOptions.WriteIndented = true;
            List<Proxy> save_proxies = proxies.Where(item => item.apply != ApplyType.DELETE).ToList();
            string jsonString = JsonSerializer.Serialize(save_proxies, jsonSerializerOptions);
            return jsonString;
        }

        static public List<Proxy> ToProxies(string jsonString)
        {
            List<Proxy> proxies = JsonSerializer.Deserialize<List<Proxy>>(jsonString) ?? new List<Proxy>();
            return proxies;
        }
    }

}
