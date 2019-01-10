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
            //bool s = AuthorizeCustomAttribute.SkipAuthorization(HttpContext.Current.Request.)
            GetUsername();
            IAuthenticatable mgr = new AuthenticationManager();
            this.token = mgr.FetchToken(this.username);
        }

        private void GetUsername()
        {
            string[] cookieValues = HttpContext.Current.Request.Cookies["authtkn"].Value.Split(':');
            if(cookieValues.Length == 2)
            {
                this.username = cookieValues[0];
                this.requestToken = cookieValues[1];
            }           
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

        //private static bool SkipAuthorization(ControllerContext context)
        //{
        //    context.RouteData.GetRequiredString("AllowAnonymous");
        //    return true;
        //}
    }
}