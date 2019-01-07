using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Earrings.Attributes
{
    public class AuthorizeCustomAttribute : AuthorizeAttribute
    {
        private string token;

        public AuthorizeCustomAttribute(string token)
        {
            this.token = token;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if(httpContext.Request.Cookies.Get("tknck").Value.Equals(this.token))
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