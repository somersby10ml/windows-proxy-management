using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace windows_proxy_management
{
    internal class Util
    {
        static public bool IsAdministrator()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            if (null != identity)
            {
                WindowsPrincipal principal = new(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            return false;
        }

        static public string GetCurrentPath()
        {
            ProcessModule? processModule = Process.GetCurrentProcess().MainModule;
            if (processModule != null)
            {
                if (processModule.FileName != null)
                    return processModule.FileName;
            }

            if (Environment.ProcessPath != null)
                return Environment.ProcessPath;

            throw new Exception("can't find my file path");
        }

        [DllImport("shell32", CharSet = CharSet.Unicode)]
        public static extern IntPtr CommandLineToArgv([MarshalAs(UnmanagedType.LPWStr)] String lpCommandLine, out Int32 lpNumArgs);


        /// <summary>
        /// Check if my file exists in the file address
        /// ex) {{"C:\aa\bb.exe" -commandline}} check C:\aa.bb.exe
        /// </summary>
        /// <param name="fullPath"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        static public bool CheckCommandLine(string fullPath, string path)
        {
            IntPtr pArgs = CommandLineToArgv(fullPath, out int nArgs);
            if (pArgs == IntPtr.Zero)
                return false;

            try
            {
                if (nArgs <= 0)
                    return false;

                IntPtr lpArg = Marshal.ReadIntPtr(pArgs, 0);
                string? normalizedPath = Marshal.PtrToStringUni(lpArg);
                if (normalizedPath != null)
                {
                    if (normalizedPath.Equals(path, StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }
            }
            finally
            {
                Marshal.FreeHGlobal(pArgs);
            }
            return false;
        }


    }
}
