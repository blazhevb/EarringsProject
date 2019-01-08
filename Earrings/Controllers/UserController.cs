using Earrings.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EarringsBusinessLogic;
using EarringsBusinessLogic.Authentication;
using EarringsBusinessLogic.Authentication.Contracts;
using System.Web.Security;
using Earrings.Attributes;

namespace Earrings.Controllers
{
    [AuthorizeCustom]
    public class UserController : Controller
    {
        private const string COOKIE_NAME = "tknck";
        private const string token = "";

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegistrationRequestModel model)
        {


            if(ModelState.IsValid)
            {
                IToken token = new Token();
                string tk = token.GetToken();
                EarringsBusinessLogic.Authentication.Contracts.IUserFactory userFactory = new UserFactory();
                int result = (int)userFactory.CreateUser(model.Email, model.Username, model.Password, tk);

                switch(result)
                {
                    case 1:
                        ModelState.AddModelError("Email", "Email is already in use.");
                        break;
                    case 2:
                        ModelState.AddModelError("Username", "Username is already taken.");
                        break;
                    case 4:
                        ReturnTokenCookie(model.Username, tk);                        
                        return RedirectToAction("Index", "Home");
                    default:
                        ModelState.AddModelError("Other", "Please try again.");
                        break;
                }
            }
            return View(model);
        }

        public ActionResult Login()
        {
            ViewBag.Message = "Login form.";
            bool x = Request.IsAuthenticated;
            return View();
        }

        private void ReturnTokenCookie(string username, string token)
        {
            var cookie = FormsAuthentication.GetAuthCookie(HttpContext.User.Identity.Name, false);
            cookie.Name = "authtkn";
            cookie.Values.Add(username, token); 
            Response.Cookies.Add(cookie);
        }

    }
}
