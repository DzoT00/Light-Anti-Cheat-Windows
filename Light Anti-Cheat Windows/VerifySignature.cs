using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BasicAntiCheatUnityWindows
{
    public class VerifySignature
    {
        public static bool detected;

        public static void IsSignatureValid()
        {
            try
            {
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
                {
                    byte[] hash;
                    using (SHA256 sha256 = SHA256.Create())
                    {
                        byte[] data = new byte[] { 61, 6, 250, 104, 79, 99, 144, 203, 212, 14, 226, 95, 27, 43, 102, 199, 215, 136, 132, 137 };
                        hash = sha256.ComputeHash(data);
                    }

                    RSAPKCS1SignatureFormatter RSAFormatter = new RSAPKCS1SignatureFormatter(rsa);
                    RSAFormatter.SetHashAlgorithm("SHA256");

                    byte[] signedHash = RSAFormatter.CreateSignature(hash);

                    RSAPKCS1SignatureDeformatter RSADeformatter = new RSAPKCS1SignatureDeformatter(rsa);
                    RSADeformatter.SetHashAlgorithm("SHA256");

                    if (RSADeformatter.VerifySignature(hash, signedHash))
                    {
                        detected = false;
                    }
                    else
                    {
                        detected = true;
                    }
                }
            }
            catch (CryptographicException cx)
            {
                Console.WriteLine(cx.Message);
            }
        }

        public static bool InvalidSignature()
        {
            return detected;
        }
    }
}
