using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LightAntiCheatUnityClient
{
    public class Util
    {
        #region Convertions

        public static string boolToString(bool input)
        {
            if (input == true)
            {
                return "true";
            }
            else if (input == false)
            {
                return "false";
            }
            return "false";
        }

        public static bool stringToBool(string input)
        {
            if (input == "true")
            {
                return true;
            }
            else if (input == "false")
            {
                return false;
            }
            return false;
        }

        public static string splitString(string input, int number)
        {
            string str_split = input;
            string[] broken_str = str_split.Split('.');

            return broken_str[number];
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

                using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
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
