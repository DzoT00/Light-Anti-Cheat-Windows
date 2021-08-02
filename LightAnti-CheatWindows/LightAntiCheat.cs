using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LightAnti_CheatWindows
{
    public partial class LightAntiCheat : Form
    {
        private const int WM_COPYDATA = 0x4A;

        public LightAntiCheat()
        {
            InitializeComponent();
        }

        private void LightAntiCheat_Load(object sender, EventArgs e)
        {

        }

        public static void SendMessageToGame(string message)
        {
            foreach (Process clsProcess in Process.GetProcesses())
            {
                Process[] pname = Process.GetProcessesByName(Program.GameName);
                if (pname.Length == 0)
                {
                    NativeImport.COPYDATASTRUCT cds;
                    cds.dwData = 0;
                    cds.lpData = (int)Marshal.StringToHGlobalAnsi(message);
                    cds.cbData = message.Length;
                    NativeImport.SendMessage(clsProcess.MainWindowHandle, (int)WM_COPYDATA, 0, ref cds);
                }
            }
        }
    }
}
