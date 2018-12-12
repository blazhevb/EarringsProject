﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EarringsBusinessLogic.Authentication.Contracts
{
    public interface IUserFactory
    {
        string CreateUser(string email, string username, string password, string token);
    }
}
