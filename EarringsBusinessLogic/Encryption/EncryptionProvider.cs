using EarringsBusinessLogic.Encryption.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace EarringsBusinessLogic.Encryption
{
    public class EncryptionProvider : IEncryption
    {
        private const string KEY = "ab3916ba6c8c4df7b28xceab11bc91bw";
        private const int IV_SIZE = 16;

        public byte[] Encrypt(string data)
        {
            byte[] result;
            using(AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
            {
                aes.Key = Encoding.UTF8.GetBytes(KEY);
                aes.GenerateIV();
                var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using(MemoryStream ms = new MemoryStream())
                {
                    using(CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using(StreamWriter writer = new StreamWriter(cs))
                        {
                            writer.Write(data);
                        }
                    }
                    byte[] encryptedData = ms.ToArray();
                    result = new byte[IV_SIZE + encryptedData.Length];
                    System.Buffer.BlockCopy(aes.IV, 0, result, 0, IV_SIZE);
                    System.Buffer.BlockCopy(encryptedData, 0, result, IV_SIZE, encryptedData.Length);
                }
                return result;
            }
        }

        public string Decrypt(byte[] encryptedData)
        {
            byte[] ivFromData = new byte[IV_SIZE];
            int dataExtractLength = encryptedData.Length - IV_SIZE;
            byte[] encryptedDataSubarray = new byte[dataExtractLength];
            System.Buffer.BlockCopy(encryptedData, 0, ivFromData, 0, IV_SIZE);
            System.Buffer.BlockCopy(encryptedData, IV_SIZE, encryptedDataSubarray, 0, dataExtractLength);
            string result = null;

            using(AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
            {
                aes.Key = Encoding.UTF8.GetBytes(KEY);
                aes.IV = ivFromData;
                var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using(MemoryStream ms = new MemoryStream(encryptedDataSubarray))
                {
                    using(CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using(StreamReader reader = new StreamReader(cs))
                        {
                            result = reader.ReadToEnd();
                        }
                    }
                }
            }
            return result;
        }
    }
}