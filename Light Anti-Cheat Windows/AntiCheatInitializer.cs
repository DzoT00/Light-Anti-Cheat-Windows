using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BasicAntiCheatUnityWindows
{
    public class AntiCheatInitializer
    {
        public bool AntiRuntimeDump = false;
        public bool AntiVM = false;
        public bool AntiDebug = false;
        public bool SpeedHackDetection = false;

        public static void Initialize()
        {
            InitThread();

            Program.clearLogFile();
            Program.createLogFile();
            Program.writeLineToLogFile("Started at " + DateTime.Now.ToString());

            AntiDebugger.Initialize();
            ProcessScanner.Initialize();
            ProcessStringScan.Initialize();
            AntiDump.Initialize();
        }

        public static void UpdateDetections()
        {
            CheckDetections.convertToRightBool();

            AntiDebugger.UpdateAntiDebug();
            ProcessScanner.UpdateProcessList();

            CheckDetections.SendIfDetected();
        }

        public static void UpdateConsole()
        {
            Console.WriteLine("");
            Console.WriteLine("Debugger Active: " + CheckDetections.DebuggerDetected);
            Console.WriteLine("Virtual Machine: " + CheckDetections.VMDetected);
            Console.WriteLine("Invalid Signature: " + CheckDetections.InvalidSignatureDetected);
            Console.WriteLine("Process Detection: " + ProcessScanner.detected + " " + ProcessScanner.detectedApplicationPath);
            Console.WriteLine("Found Suspicious String: " + CheckDetections.StringInProcessDetected);
        }

        #region Threading

        private static bool isRunning = false;
        private const float TicksPerSec = 0.1f;
        private const float MsPerTick = 1000 / TicksPerSec;

        public static void UpdateThread()
        {
            UpdateDetections();
            UpdateConsole();
        }

        public static void InitThread()
        {
            isRunning = true;

            Thread mainThread = new Thread(new ThreadStart(MainThread));
            mainThread.Start();
        }

        private static void MainThread()
        {
            Console.WriteLine("LIGHT ANTI-CHEAT");
            Console.WriteLine($"Started at {TicksPerSec} ticks per second.");
            DateTime _nextLoop = DateTime.Now;

            while (isRunning)
            {
                while (_nextLoop < DateTime.Now)
                {
                    UpdateThread();
                    _nextLoop = _nextLoop.AddMilliseconds(MsPerTick);

                    if (_nextLoop > DateTime.Now)
                    {
                        Thread.Sleep(_nextLoop - DateTime.Now);
                    }
                }
            }
        }

        #endregion
    }
}
