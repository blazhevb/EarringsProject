using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EarringsBusinessLogic.Authentication.Contracts;
using EarringsBusinessLogic.Authentication;
using System.Collections.Specialized;

namespace Earrings.Attributes
{
    public class AuthorizeCustomAttribute : AuthorizeAttribute
    {
        private string username;
        private string token;
        private string requestToken;

        public AuthorizeCustomAttribute()
        {
            
            IAuthenticatable mgr = new AuthenticationManager();
            this.token = mgr.FetchToken(this.username);
        }

        private void GetUsername()
        {
            NameValueCollection collection = HttpContext.Current.Request.Cookies["authtkn"].Values;            
            this.username = collection.Keys[0];
            this.requestToken = collection[username];
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if(this.token.Equals(this.requestToken))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}