using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LightAnti_CheatWindows
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
                IntPtr pOpenThread = NativeImport.OpenThread(NativeImport.ThreadAccess.SUSPEND_RESUME, false, (uint)pT.Id);

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
                IntPtr pOpenThread = NativeImport.OpenThread(NativeImport.ThreadAccess.SUSPEND_RESUME, false, (uint)pT.Id);

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

        private static string hash = "DN!€%E67#)/0/XX5O9O(DJ=TW)I%H(J(?8D/GO~E$'KIWPR/A(9P@€Z?VRX€€@0I7#7FT7YA&JDE#8~GYT$RG?~*JKS/(AJ=XC0X7MURQ919)U@%URW1HJC&C9~JP8J%AL6~IYYM'UAVQMG&P5UY369N#$V*PKC7Z/84&*3~=K2$J5X(2H?8O!JS0S~WC0~%RS~120ASCX5TD*ETO5TIQTS&5QEVAH?QN(U(9€MCH05)E(O2N2HWG&V'#H~MW3S€";

        public static string encryptedString(string input)
        {
            byte[] data = UTF8Encoding.UTF8.GetBytes(input);

            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                
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
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));

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
