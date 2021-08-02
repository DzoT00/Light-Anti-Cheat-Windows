using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightAnti_CheatWindows
{
    public class ProcessScanner
    {
        public static bool detected;

        public static List<Process> currentlyRunningProcesses = new List<Process>();
        public static List<string> suspiciousProcessNames = new List<string>();

        public static string[] suspiciousProcessNamesArray = { "OLLYDBG", "cheatengine-x86_64", "ReClassEx", "ReClassEx64", "x64dbg", "x32dbg", "IDA Pro", "Immunity Debugger", "Ghidra", "de4dot", "de4dot-x64", "ida", "ida64", "dotPeek64", "dotPeek32", "Fiddler", "dnSpy", "dnSpy-x86", "dnSpy.Console" };
        public static string ignore = "Light Anti-Cheat";

        public static string detectedApplication;
        public static string detectedApplicationPath;

        public static void Initialize()
        {
            for (int i = 0; i < suspiciousProcessNamesArray.Length; i++)
            {
                suspiciousProcessNames.Add(suspiciousProcessNamesArray[i]);
            }
        }

        public static void UpdateProcessList()
        {
            Process[] processCollection = Process.GetProcesses();
            foreach (Process p in processCollection)
            {
                currentlyRunningProcesses.Add(p);
            }

            FindSuspiciousProcess();
        }

        public static void FindSuspiciousProcess()
        {
            foreach(Process running in currentlyRunningProcesses)
            {
                for (int i = 0; i < suspiciousProcessNamesArray.Length; i++)
                {
                    if (running.ProcessName.Contains(suspiciousProcessNamesArray[i]))
                    {
                        if (running.ProcessName == ignore)
                        {
                            return;
                        }
                        else
                        {
                            detected = true;
                            detectedApplication = running.ProcessName;
                            //detectedApplicationPath = GetProcessPath(running.ProcessName);
                            return;
                        }
                    }
                }
            }
        }

        public static string GetProcessPath(string name)
        {
            Process[] processes = Process.GetProcessesByName(name);

            if (processes.Length > 0)
            {
                return processes[0].MainModule.FileName;
            }
            else
            {
                return string.Empty;
            }
        }

        public static bool detectedProcess()
        {
            return detected;
        }
    }
}
