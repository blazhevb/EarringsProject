using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EarringsBusinessLogic.Authentication.Contracts;
using EarringsBusinessLogic.Authentication;
using System.Collections.Specialized;
using System.Web.Security;
using EarringsBusinessLogic.Authentication.Abstractions;

namespace Earrings.Attributes
{
    public class AuthorizeCustomAttribute : AuthorizeAttribute
    {
        private string username;
        private string token;
        private string requestToken;

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if(SkipAuthorization(filterContext.ActionDescriptor))
            {
                return;
            }
            GetUsername();
            AuthenticationManager mgr = new AuthenticationManager();
            if(this.token == null)
            {
                this.token = mgr.GetTokenFromDb(this.username);
            }
            base.OnAuthorization(filterContext);
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {

            if(this.token != null && this.token.Equals(this.requestToken))
            {
                return true;
            }
            else
            {
                return false;
            }
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

        private static bool SkipAuthorization(ActionDescriptor actions)
        {
            if(actions.IsDefined(typeof(AllowAnonymousAttribute), true) || 
                actions.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
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