using EarringsBusinessLogic.Authentication.Contracts;
using EarringsDbProj;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EarringsBusinessLogic.Authentication.Abstractions
{
    public abstract class AuthenticationManagerBase : IAuthenticatable
    {
        public virtual void Login(string username)
        {
            throw new NotImplementedException();
        }

        public virtual void LoginWithPassword(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
