using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LightAnti_CheatWindows
{
    public class CheckProgramSignature
    {
        public static bool Detected { get; set; }

        public static List<Process> currentlyRunningProcesses = new List<Process>();
        public static List<string> signaturesOfRunningProcs = new List<string>();

        public static string[] knownCheatSignaturesArray =
        {
            "2ce992abd25f22de9a9b737bc608cf89", //CheatEngine
            "36730c5c7ccb78a132209d4ead01b649", //CheatEngine
            "56b684a54088439ce969e27a4d1ae4f2", //CheatEngine
            "f8c759f9a0b69169b84422cb2da1b984", //CheatEngine
            "8197748b35288c0aa212d93d36a5cc4a", //dnSpy
            "ef2e5767ffac947d678779e5b58f459a", //dnSpy.Console
            "45c797440241ec7886e2ecc75c4274d4", //GHInjector x64
            "155a71b72f71a16f2f813af1243b1e5f", //GHInjector x86
        };

        public static void UpdateProcessList()
        {
            Process[] processCollection = Process.GetProcesses();
            foreach (Process p in processCollection)
            {
                currentlyRunningProcesses.Add(p);
            }
        }

        public static void ScanAllRunningProcesses()
        {
            try
            {
                foreach (Process proc in currentlyRunningProcesses)
                {
                    string path = GetExecutablePathAboveVista(proc.Id);
                    string signature = getMD5SignatureFromFile(path);
                    signaturesOfRunningProcs.Add(signature);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static void DetectSignature()
        {
            foreach (string s in signaturesOfRunningProcs)
            {
                foreach (string sus in knownCheatSignaturesArray)
                {
                    if (s == sus)
                    {
                        Detected = true;
                    }
                }
            }
        }

        private static string GetExecutablePath(Process Process)
        {
            if (Environment.OSVersion.Version.Major >= 6)
            {
                return GetExecutablePathAboveVista(Process.Id);
            }

            return Process.MainModule.FileName;
        }

        private static string GetExecutablePathAboveVista(int ProcessId)
        {
            var buffer = new StringBuilder(1024);
            IntPtr hprocess = NativeImport.OpenProcess(NativeImport.ProcessAccess.QueryLimitedInformation, false, ProcessId);
            if (hprocess != IntPtr.Zero)
            {
                try
                {
                    int size = buffer.Capacity;
                    if (NativeImport.QueryFullProcessImageName(hprocess, 0, buffer, out size))
                    {
                        return buffer.ToString();
                    }
                }
                finally
                {
                    NativeImport.CloseHandle(hprocess);
                }
            }
            var process = Process.GetCurrentProcess();
            return process.MainModule.FileName;
            //throw new Win32Exception(Marshal.GetLastWin32Error());
        }

        public static string getMD5SignatureFromFile(string path)
        {
            using(var md5 = MD5.Create())
            {
                using(var stream = File.OpenRead(path))
                {
                    return BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", string.Empty).ToLower();
                }
            }
        }
    }
}
