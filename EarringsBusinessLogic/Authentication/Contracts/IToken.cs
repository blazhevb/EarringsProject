using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EarringsBusinessLogic.Authentication.Contracts
{
    public interface IToken
    {
        string GetToken();
    }
}
