using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace LightAnti_CheatWindows
{
    public static class Program
    {
        public static string GameName = "";
        public static string antiCheatLogPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\LightACLog.txt";

        public static int deltaTime;

        public static void Main(string[] args)
        {
            AntiCheatInitializer.Initialize();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LightAntiCheat());
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
