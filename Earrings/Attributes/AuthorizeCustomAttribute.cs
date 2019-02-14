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
using System.Security.Principal;

namespace Earrings.Attributes
{
    public class AuthorizeCustomAttribute : AuthorizeAttribute
    {


        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if(SkipAuthorization(filterContext.ActionDescriptor))
            {
                return;
            }
            AuthenticationModule module = new AuthenticationModule();
            module.Authenticate();
            
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