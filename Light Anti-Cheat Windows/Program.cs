using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BasicAntiCheatUnityWindows
{
    public class Program
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        const int SW_HIDE = 0;

        public static string GameName = "";
        public static string antiCheatLogPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\LightACLog.txt";

        public static void Main(string[] args)
        {
            var handle = GetConsoleWindow();
            ShowWindow(handle, SW_HIDE);

            AntiCheatInitializer.Initialize();
            Console.ReadKey();
        }

        public static void createLogFile()
        {
            string path = antiCheatLogPath;
            if (!File.Exists(path))
            {
                File.Create(path);
            }
            else if (File.Exists(path))
            {
                Console.WriteLine("Anti-Cheat LogFile already exists.");
            }
        }

        public static void clearLogFile()
        {
            File.WriteAllText(antiCheatLogPath, String.Empty);
        }

        public static bool stringExistsInFile(string input)
        {
            if (File.ReadAllText(antiCheatLogPath).Contains(input))
            {
                return true;
            }
            return false;
        }

        public static void writeLineToLogFile(string message)
        {
            File.AppendAllText(antiCheatLogPath, Util.encryptedString(message) + Environment.NewLine);
        }

        public static string getApplicationProcessName()
        {
            return AppDomain.CurrentDomain.FriendlyName;
        }
    }
}
