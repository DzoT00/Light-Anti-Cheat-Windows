using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LightAnti_CheatWindows
{
    public class AntiDebugger
    {
        private static bool detected;
        public static string[] debuggers = { "ida64", "dbg64" };

        public static void Initialize()
        {
            UpdateAntiDebug();
        }

        public static void UpdateAntiDebug()
        {
            //Looping through an array of known debuggers
            for (int i = 0; i < debuggers.Length; i++)
            {
                Process[] debuggerName = Process.GetProcessesByName(debuggers[i]);
                if (debuggerName != null && debuggerName.Length > 0)
                {
                    detected = true;
                }
            }

            //Block Debugger Atatching to the Game
            bool isDebuggerPresent = false;
            NativeImport.CheckRemoteDebuggerPresent(Process.GetCurrentProcess().Handle, ref isDebuggerPresent);
            if (isDebuggerPresent)
            {
                //Console.WriteLine("Debugger Attached: " + isDebuggerPresent + "\n");
                detected = true;
            }

            //Only Used for .Net debuggers
            if (Debugger.IsAttached)
            {
                //Console.Write(".Net debugger attatched\n");
                detected = true;
            }
        }

        public static bool isDetected()
        {
            return detected;
        }
    }
}
