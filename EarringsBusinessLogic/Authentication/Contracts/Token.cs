using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EarringsBusinessLogic.Authentication.Contracts
{
    public class Token : IToken
    {
        public string GetToken()
        {
            Guid token = Guid.NewGuid();

            return token.ToString("N");
        }
    }
}
