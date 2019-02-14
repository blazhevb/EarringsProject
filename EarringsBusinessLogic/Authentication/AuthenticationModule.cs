using EarringsBusinessLogic.Authentication.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace EarringsBusinessLogic.Authentication
{
    public class AuthenticationModule : IAuthenticator
    {
        private string username;
        private string token;
        private string requestToken;

        public bool Authenticate()
        {
            GetUsername();
            AuthenticationManager mgr = new AuthenticationManager(HttpContext.Current);
            if(this.token == null)
            {
                this.token = mgr.GetTokenFromDb(this.username);
                if(this.token.Equals(this.requestToken))
                {
                    UpdatePrincipal(this.username, new string[] { "user" });
                    return true;
                }

            }
            return false;
        }

        private void GetUsername()
        {
            string[] cookieValues = HttpContext.Current.Request.Cookies["authtcn"]?.Value.Split(':');
            if(cookieValues != null && cookieValues.Length == 2)
            {
                this.username = cookieValues[0];
                this.requestToken = cookieValues[1];
            }
        }

        public void UpdatePrincipal(string username, string[] roles)
        {
            IIdentity identity = new GenericIdentity(username);
            IPrincipal principal = new GenericPrincipal(identity, roles);
            HttpContext.Current.User = principal;
        }
    }
}
