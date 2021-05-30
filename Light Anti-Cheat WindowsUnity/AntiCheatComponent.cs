using System;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

namespace LightAntiCheatUnityClient
{
    public class AntiCheatComponent : MonoBehaviour
    {
        public delegate void EventListener();
        public Action CheatDetectedAction;
        public UnityEvent CheatDetectedEvent;

        private string cheatReason;
        private string[] cheatReasons = { "vm", "debugger", "invalidSignature", "suspiciousProcess", "suspiciousProcess" };

        private void FixedUpdate()
        {
            ReceiveDetected();
        }

        public void ReceiveDetected()
        {
            string[] lines = File.ReadAllLines(getAntiCheatLogPath());

            if (isTextFileEmpty() == false)
            {
                for (int i = 0; i < lines.Length; i++)
                {
                    for (int j = 0; j < cheatReasons.Length; j++)
                    {
                        if (lines[i].Contains(cheatReasons[j]))
                        {
                            cheatReason = Util.splitString(lines[i], 1);
                            CheatDetectedEvent.Invoke();
                        }
                    }
                }
            }
        }

        public string getDetectedReason()
        {
            return cheatReason;
        }

        public string getAntiCheatLogPath()
        {
            return Environment.CurrentDirectory + "\\LightACLog.txt"; //Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)
        }

        public bool isTextFileEmpty()
        {
            string fileName = getAntiCheatLogPath();

            FileInfo info = new FileInfo(fileName);
            if (info.Length == 0)
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
