using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BasicAntiCheatUnityWindows
{
    public class Util
    {
        #region Convertions

        public static string boolToString(bool input)
        {
            if(input == true)
            {
                return "true";
            }
            else if(input == false)
            {
                return "false";
            }
            return "false";
        }

        public static bool stringToBool(string input)
        {
            if(input == "true")
            {
                return true;
            }
            else if(input == "false")
            {
                return false;
            }
            return false;
        }

        #endregion

        #region Processes

        public static void SuspendProcess()
        {
            var process = Process.GetCurrentProcess();

            foreach (ProcessThread pT in process.Threads)
            {
                IntPtr pOpenThread = NativeImport.OpenThread(Enums.ThreadAccess.SUSPEND_RESUME, false, (uint)pT.Id);

                if (pOpenThread == IntPtr.Zero)
                {
                    continue;
                }

                NativeImport.SuspendThread(pOpenThread);

                NativeImport.CloseHandle(pOpenThread);
            }
        }

        public static void ResumeProcess()
        {
            var process = Process.GetCurrentProcess();

            foreach (ProcessThread pT in process.Threads)
            {
                IntPtr pOpenThread = NativeImport.OpenThread(Enums.ThreadAccess.SUSPEND_RESUME, false, (uint)pT.Id);

                if (pOpenThread == IntPtr.Zero)
                {
                    continue;
                }

                var suspendCount = 0;
                do
                {
                    suspendCount = NativeImport.ResumeThread(pOpenThread);
                } while (suspendCount > 0);

                NativeImport.CloseHandle(pOpenThread);
            }
        }

        #endregion

        #region Encryption

        public static string generateHash()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!$%&/()=?@€~*'#";

            var random = new Random();
            var randomString = new string(Enumerable.Repeat(chars, 256).Select(s => s[random.Next(s.Length)]).ToArray());
            return randomString;
        }

        public static string encryptedString(string input)
        {
            byte[] data = UTF8Encoding.UTF8.GetBytes(input);

            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(generateHash()));
                
                using(TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripDes.CreateEncryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    return Convert.ToBase64String(results, 0, results.Length);
                }
            }
        }

        public static string decryptString(string input)
        {
            byte[] data = Convert.FromBase64String(input);

            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(generateHash()));

                using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripDes.CreateDecryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    return UTF8Encoding.UTF8.GetString(results);
                }
            }
        }

        #endregion
    }
}
