using EarringsBusinessLogic.Authentication.Contracts;
using EarringsDbProj;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace EarringsBusinessLogic.Authentication
{
    public class AuthenticationManager : IAuthenticatable
    {
        public void LogIn(string username, string token)
        {
            IIdentity identity = new GenericIdentity(username);
            //IPrincipal principal = new GenericPrincipal(identity, new string[] { "user" });

            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, username, DateTime.Now, DateTime.MinValue, false, token);
            IPrincipal principal = new GenericPrincipal(new GenericIdentity(""), new string[0]);
            //FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, "username", DateTime.UtcNow, DateTime.MinValue, false, token); )
            //HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(new FormsIdentity(ticket), new string[] { "user" });
        }

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
