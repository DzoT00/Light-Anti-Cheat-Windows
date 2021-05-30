using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BasicAntiCheatUnityWindows
{
    public class SendCheatDetected
    {
        public static void Send(string detection)
        {
            string textToWrite = "Detected" + "." + detection + "." + Util.boolToString(true) + "." + "(" + DateTime.Now.ToString() + ")"; // 0, 1, 2
            if(!Program.stringExistsInFile(textToWrite))
            {
                Program.writeLineToLogFile(textToWrite);
            }
        }
    }
}
