using Earrings.Attributes;
using Earrings.Models;
using EarringsBusinessLogic.Authentication;
using EarringsBusinessLogic.Authentication.Contracts;
using System;
using System.Web.Mvc;
using System.Web.Security;

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
                string tkn = token.GetToken();
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
                        ReturnTokenCookie(model.Username, tkn);
                        AuthenticationManager manager = new AuthenticationManager();
                        manager.LogIn(model.Username, tkn);
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
            bool x = this.HttpContext.Request.IsAuthenticated;

            return View();
        }

        private void ReturnTokenCookie(string username, string token)
        {
            var cookie = FormsAuthentication.GetAuthCookie(username, false);
            cookie.Name = "authtkn";
            string usernameToken = String.Join(":", username, token);
            cookie.Value = usernameToken; 
            Response.Cookies.Add(cookie);
        }

    }
}
