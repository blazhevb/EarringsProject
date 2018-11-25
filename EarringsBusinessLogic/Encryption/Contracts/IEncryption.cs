using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EarringsBusinessLogic.Encryption.Contracts
{
    public interface IEncryption
    {
        byte[] Encrypt(string data);

        string Decrypt(byte[] encryptedData);
    }
}
