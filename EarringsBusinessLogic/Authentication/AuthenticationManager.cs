using EarringsBusinessLogic.Authentication.Contracts;
using EarringsDbProj;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EarringsBusinessLogic.Authentication
{
    public class AuthenticationManager : IAuthenticatable
    {
        public string FetchToken(string username)
        {
            string token;
            using(EarringsDatabaseEntities context = new EarringsDatabaseEntities())
            {
                token = context.UsersCredentials.Where(x => x.Username == username).Select(y => y.UserToken).FirstOrDefault();
            }
            return token;
        }
    }
}
