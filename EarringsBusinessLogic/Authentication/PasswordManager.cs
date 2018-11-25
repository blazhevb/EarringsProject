using EarringsBusinessLogic.Authentication.Contracts;
using EarringsBusinessLogic.Encryption;
using EarringsBusinessLogic.Encryption.Contracts;
using System;
using System.Security.Cryptography;
using System.Text;

namespace EarringsBusinessLogic.Authentication
{
    internal class PasswordManager : IPasswordManager
    {
        public string GetDecryptedPassword(string username, byte[] encryptedData)
        {
            IEncryption encryption = new EncryptionProvider();
            string usernamePassword = encryption.Decrypt(encryptedData);

            return usernamePassword.Replace(username, String.Empty);
        }

        public byte[] GetEncryptedPassword(string username, string password)
        {
            string usernamePassword = String.Concat(username, password);
            IEncryption encryption = new EncryptionProvider();

            return encryption.Encrypt(usernamePassword);
        }
    }
}
