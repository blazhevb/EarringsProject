using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EarringsBusinessLogic.Authentication.Contracts
{
    public interface IAuthenticatable
    {
        void Login(string username);

        void LoginWithPassword(string username, string password);
    }
}
