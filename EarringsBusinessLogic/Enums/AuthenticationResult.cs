using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EarringsBusinessLogic.Enums
{
    public enum AuthenticationResult
    {
        Fail = 0,
        EmailTaken = 1,
        UsernameTaken = 2,
        Success = 4
    }
}
