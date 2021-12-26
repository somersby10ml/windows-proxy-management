using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace windows_proxy_management
{
    internal class Service
    {
        [DllImport("advapi32.dll", EntryPoint = "OpenSCManagerW", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern IntPtr OpenSCManager(string? machineName, string? databaseName, ScmAccessRights dwAccess);


        [DllImport("advapi32.dll", EntryPoint = "OpenServiceW", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern IntPtr OpenService(IntPtr hSCManager, string lpServiceName, SERVICE_ACCESS dwDesiredAccess);

        [DllImport("kernel32")]
        private static extern void CloseHandle(IntPtr hObject);

        [DllImport("Advapi32")]
        private static extern void CloseServiceHandle(IntPtr hObject);

        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteService(IntPtr hService);

        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern IntPtr CreateService(IntPtr hSCManager, string lpServiceName,
            string lpDisplayName,
            SERVICE_ACCESS dwDesiredAccess,
            uint dwServiceType,
            uint dwStartType,
            uint dwErrorControl,
            string lpBinaryPathName,
            [Optional] string lpLoadOrderGroup,
            [Optional] string lpdwTagId,
            [Optional] string lpDependencies,
            [Optional] string lpServiceStartName,
            [Optional] string lpPassword
        );

        [Flags]
        public enum ScmAccessRights : uint
        {
            Connect = 0x0001,
            CreateService = 0x0002,
            EnumerateService = 0x0004,
            Lock = 0x0008,
            QueryLockStatus = 0x0010,
            ModifyBootConfig = 0x0020,
            StandardRightsRequired = 0xF0000,
            AllAccess = (StandardRightsRequired | Connect | CreateService |
                         EnumerateService | Lock | QueryLockStatus | ModifyBootConfig)
        }


        [Flags]
        public enum SERVICE_ACCESS : uint
        {
            STANDARD_RIGHTS_REQUIRED = 0xF0000,
            SERVICE_QUERY_CONFIG = 0x00001,
            SERVICE_CHANGE_CONFIG = 0x00002,
            SERVICE_QUERY_STATUS = 0x00004,
            SERVICE_ENUMERATE_DEPENDENTS = 0x00008,
            SERVICE_START = 0x00010,
            SERVICE_STOP = 0x00020,
            SERVICE_PAUSE_CONTINUE = 0x00040,
            SERVICE_INTERROGATE = 0x00080,
            SERVICE_USER_DEFINED_CONTROL = 0x00100,
            SERVICE_ALL_ACCESS = (STANDARD_RIGHTS_REQUIRED |
                              SERVICE_QUERY_CONFIG |
                              SERVICE_CHANGE_CONFIG |
                              SERVICE_QUERY_STATUS |
                              SERVICE_ENUMERATE_DEPENDENTS |
                              SERVICE_START |
                              SERVICE_STOP |
                              SERVICE_PAUSE_CONTINUE |
                              SERVICE_INTERROGATE |
                              SERVICE_USER_DEFINED_CONTROL)
        }


        [StructLayout(LayoutKind.Sequential)]
        public class QUERY_SERVICE_CONFIG
        {
            UInt32 dwBytesNeeded;
            [MarshalAs(System.Runtime.InteropServices.UnmanagedType.U4)]
            public UInt32 dwServiceType;
            [MarshalAs(System.Runtime.InteropServices.UnmanagedType.U4)]
            public UInt32 dwStartType;
            [MarshalAs(System.Runtime.InteropServices.UnmanagedType.U4)]
            public UInt32 dwErrorControl;
            [MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPWStr)]
            public String? lpBinaryPathName;
            [MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPWStr)]
            public String? lpLoadOrderGroup;
            [MarshalAs(System.Runtime.InteropServices.UnmanagedType.U4)]
            public UInt32 dwTagID;
            [MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPWStr)]
            public String? lpDependencies;
            [MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPWStr)]
            public String? lpServiceStartName;
            [MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPWStr)]
            public String? lpDisplayName;
        };

        [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true, EntryPoint = "QueryServiceConfig2W")]
        public static extern bool QueryServiceConfig2(IntPtr hService, UInt32 dwInfoLevel, IntPtr buffer, UInt32 cbBufSize, out UInt32 pcbBytesNeeded);
        [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool QueryServiceConfig(IntPtr hService, IntPtr intPtrQueryConfig, UInt32 cbBufSize, out UInt32 pcbBytesNeeded);

        private string serviceName;
        private string displayName;
        private string binaryPath;

        public Service(string serviceName, string displayName, string binaryPath)
        {
            this.serviceName = serviceName;
            this.displayName = displayName;
            this.binaryPath = binaryPath;
        }

        public bool Install()
        {
            // ProxyManagement
            // SERVICE_WIN32_OWN_PROCESS
            const uint SERVICE_WIN32_OWN_PROCESS = 0x00000010;
            const uint SERVICE_AUTO_START = 0x00000002;

            IntPtr hManager = OpenSCManager(null, null, ScmAccessRights.AllAccess); ;
            CreateService(hManager, serviceName, displayName, SERVICE_ACCESS.SERVICE_ALL_ACCESS, SERVICE_WIN32_OWN_PROCESS,
                SERVICE_AUTO_START, 0, binaryPath);

            //throw new Win32Exception();
            CloseServiceHandle(hManager);
            return false;
        }

        public bool Uninstall()
        {
            IntPtr hManager = IntPtr.Zero;
            IntPtr hService = IntPtr.Zero;
            try
            {
                hManager = OpenSCManager(null, null, 0);
                if (hManager == IntPtr.Zero)
                    return false;

                hService = OpenService(hManager, serviceName, SERVICE_ACCESS.SERVICE_ALL_ACCESS);
                if (hService == IntPtr.Zero)
                    return true;

                DeleteService(hService);
            }
            finally
            {
                CloseServiceHandle(hService);
                CloseServiceHandle(hManager);
            }
            return true;
        }

        public bool Exist()
        {
            IntPtr hManager = IntPtr.Zero;
            IntPtr hService = IntPtr.Zero;
            try
            {
                hManager = OpenSCManager(null, null, 0);
                if (hManager == IntPtr.Zero)
                    return false;

                hService = OpenService(hManager, serviceName, SERVICE_ACCESS.SERVICE_QUERY_CONFIG);
                if (hService != IntPtr.Zero)
                    return true;
            }
            finally
            {
                if (hService != IntPtr.Zero) CloseServiceHandle(hService);
                if (hManager != IntPtr.Zero) CloseServiceHandle(hManager);
            }

            return false;
        }

        public bool CheckBinaryPath()
        {
            QUERY_SERVICE_CONFIG SvcConfig = new();

            IntPtr hManager = IntPtr.Zero;
            IntPtr hService = IntPtr.Zero;
            try
            {
                hManager = OpenSCManager(null, null, 0);
                if (hManager == IntPtr.Zero)
                    throw new Win32Exception();

                hService = OpenService(hManager, serviceName, SERVICE_ACCESS.SERVICE_QUERY_CONFIG);
                if (hService == IntPtr.Zero)
                    throw new Win32Exception();

                IntPtr ptr = Marshal.AllocHGlobal(4096);
                if (!QueryServiceConfig(hService, ptr, 4096, out uint dummy)) return false;
                Marshal.PtrToStructure(ptr, SvcConfig);
                Marshal.FreeHGlobal(ptr);

                return binaryPath.Equals(SvcConfig.lpBinaryPathName, StringComparison.OrdinalIgnoreCase);
            }
            finally
            {
                if (hService != IntPtr.Zero) CloseServiceHandle(hService);
                if (hManager != IntPtr.Zero) CloseServiceHandle(hManager);
            }
        }


    }
}
