using EarringsBusinessLogic.Authentication.Abstractions;
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
    public class AuthenticationManager : AuthenticationManagerBase
    {
        private HttpContext httpContext;

        private IToken token;

        public AuthenticationManager(HttpContext httpContext)
        {
            this.token = new Token();
            this.httpContext = httpContext;
        }

        public override void Login(string username)
        {
            string tkn = token.GetToken();
            try
            {
                using(EarringsDatabaseEntities context = new EarringsDatabaseEntities())
                {
                    UsersCredentials credentials = context.UsersCredentials.Where(x => x.Username == username).SingleOrDefault();
                    if(credentials != null)
                    {
                        credentials.UserToken = tkn;
                        context.SaveChanges(); 

                        ReturnTokenCookie(username, tkn);
                    }
                }
            }
            catch(Exception e)
            {
                //some logging
            }           
        }

        public string GetTokenFromDb(string username)
        {
            string token = null;
            using(EarringsDatabaseEntities context = new EarringsDatabaseEntities())
            {
                token = context.UsersCredentials.Where(x => x.Username == username).Select(t => t.UserToken).SingleOrDefault();
            }
            return token;
        }
        public void LogIn(string username, string token)
        {
            
            
            IIdentity identity = new GenericIdentity(username);

            IPrincipal principal = new GenericPrincipal(identity, new string[] { "user" });

            //FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, username, DateTime.Now, DateTime.MinValue, false, token);
            //IPrincipal principal = new GenericPrincipal(new GenericIdentity(""), new string[0]);
            //FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, "username", DateTime.UtcNow, DateTime.MinValue, false, token); )
            //HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(new FormsIdentity(ticket), new string[] { "user" });
        }

        private void ReturnTokenCookie(string username, string token)
        {
            var cookie = new HttpCookie("authtcn");

            string usernameToken = String.Join(":", username, token);
            cookie.Value = usernameToken;
            HttpContext.Current.Response.Cookies.Add(cookie);

        }

    }
}
