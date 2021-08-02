using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LightAnti_CheatWindows
{
    public class CheckDetections
    {
        public static string detection;

        public static bool DumpDetected;
        public static bool VMDetected;
        public static bool DebuggerDetected;
        public static bool SpeedHackDetected;
        public static bool InvalidSignatureDetected;
        public static bool ProcessDetected;
        public static bool StringInProcessDetected;
        public static bool SignatureDetected;

        public static void convertToRightBool()
        {
            if (VMDetection.Detect())
                VMDetected = true;

            if (AntiDebugger.isDetected())
                DebuggerDetected = true;

            if (VerifySignature.InvalidSignature())
                InvalidSignatureDetected = true;

            if (ProcessScanner.detectedProcess())
                ProcessDetected = true;

            if (ProcessStringScan.isDetected())
                StringInProcessDetected = true;

            if (CheckProgramSignature.Detected)
                SignatureDetected = true;
        }

        public static bool detectedSomething()
        {
            if (VMDetected)
            {
                detection = "vm";
                return true;
            }
            else if (DebuggerDetected)
            {
                detection = "debugger";
                return true;
            }
            else if (InvalidSignatureDetected)
            {
                detection = "invalidSignature";
                return true;
            }
            else if (ProcessDetected)
            {
                detection = "suspiciousProcess";
                return true;
            }
            else if (StringInProcessDetected)
            {
                detection = "suspiciousProcess";
                return true;
            }
            else if (SignatureDetected)
            {
                detection = "suspiciousSignature";
                return true;
            }

            detection = "";
            return false;
        }

        public static void SendIfDetected()
        {
            if(detectedSomething())
            {
                SendCheatDetected.Send(detection);
            }
            else
            {
                return;
            }
        }
    }
}
