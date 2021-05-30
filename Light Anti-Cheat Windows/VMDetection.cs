using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace BasicAntiCheatUnityWindows
{
    public class VMDetection
    {
        public static bool Detect()
        {
            return new ManagementObjectSearcher("SELECT * FROM Win32_PortConnector").Get().Count == 0;
        }
    }
}
