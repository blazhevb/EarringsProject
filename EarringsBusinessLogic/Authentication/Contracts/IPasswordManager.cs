using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EarringsBusinessLogic.Authentication.Contracts
{
    public interface IPasswordManager
    {
        byte[] GetEncryptedPassword(string username, string password);

        string GetDecryptedPassword(string username, byte[] encryptedData);
    }
}
