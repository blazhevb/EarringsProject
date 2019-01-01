/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Earrings.Attributes
{
    public class AuthorizeWithTokenAttribute : FilterAttribute, IAuthorizationFilter
    {
        private string cookieName;
        private string token;

        public AuthorizeWithTokenAttribute(string cookieName, string token)
        {
            this.cookieName = cookieName;
            this.token = token;
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            HttpCookie cookie = filterContext.RequestContext.HttpContext.Request.Cookies.Get(cookieName);
            if(cookie == null || cookie.Value != token)
            {
                
            }
        }
    }
}*/