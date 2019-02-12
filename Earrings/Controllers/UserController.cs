using Earrings.Attributes;
using Earrings.Models;
using EarringsBusinessLogic.Authentication;
using EarringsBusinessLogic.Authentication.Contracts;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using EarringsBusinessLogic.Authentication.Abstractions;
using System.Security.Principal;

namespace Earrings.Controllers
{
    [AuthorizeCustom]
    public class UserController : Controller
    {
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
                EarringsBusinessLogic.Authentication.Contracts.IUserFactory userFactory = new UserFactory();
                int result = (int)userFactory.CreateUser(model.Email, model.Username, model.Password);

                switch(result)
                {
                    case 1:
                        ModelState.AddModelError("Email", "Email is already in use.");
                        break;
                    case 2:
                        ModelState.AddModelError("Username", "Username is already taken.");
                        break;
                    case 4:
                        AuthenticationManagerBase authenticationManager = new AuthenticationManager(System.Web.HttpContext.Current);
                        authenticationManager.Login(model.Username);
                        IIdentity identity = new GenericIdentity(model.Username);
                        IPrincipal principal = new GenericPrincipal(identity, new string[] { "user" });
                        System.Web.HttpContext.Current.User = principal;

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
    }
}
