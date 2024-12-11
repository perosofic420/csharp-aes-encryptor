using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Encryptor
{
    public class Encryption
    {
        public static byte[] GenerateKey(string key)
        {
            return Encoding.UTF8.GetBytes(key);
        }

        public static byte[] GenerateIV(string iv)
        {
            return Encoding.UTF8.GetBytes(iv);
        }

        public static string EncryptString(string plainText, byte[] key, byte[] iv)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                    }
                    return BitConverter.ToString(msEncrypt.ToArray()).Replace("-", "").ToLower();
                }
            }
        }

        public static string DecryptString(string cipherText, byte[] key, byte[] iv)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream())
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Write))
                    {
                        byte[] cipherBytes = new byte[cipherText.Length / 2];
                        for (int i = 0; i < cipherText.Length; i += 2)
                        {
                            cipherBytes[i / 2] = Convert.ToByte(cipherText.Substring(i, 2), 16);
                        }
                        csDecrypt.Write(cipherBytes, 0, cipherBytes.Length);
                    }

                    return Encoding.UTF8.GetString(msDecrypt.ToArray());
                }
            }
        }
    }
}
