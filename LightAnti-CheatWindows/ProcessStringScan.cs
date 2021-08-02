using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LightAnti_CheatWindows
{
    public class ProcessStringScan
    {
        public static string filePath;

        public static string[] suspiciousNames = { "Aim", "Hack", "Cheat", "OpenProcess", "ReadProcess" };

        private static int detections;
        private static bool detected;

        public static void Initialize()
        {
            foreach(Process s in ProcessScanner.currentlyRunningProcesses)
            {
                filePath = s.MainModule.FileName;
                Detect();
            }
        }

        public static void Detect()
        {
            string line;

            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    line = sr.ReadLine();

                    while(line != null)
                    {
                        for (int i = 0; i < suspiciousNames.Length; i++)
                        {
                            if (line.Contains(ProcessScanner.ignore))
                            {
                                break;
                            }

                            if(line.Contains(suspiciousNames[i]))
                            {
                                detections++;
                                detected = true;
                                break;
                            }
                        }
                        line = sr.ReadLine();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static int getDetections()
        {
            return detections;
        }

        public static bool isDetected()
        {
            if(detected)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
