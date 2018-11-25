using Earrings.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EarringsBusinessLogic;
using EarringsBusinessLogic.Authentication;

namespace Earrings.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        public ActionResult Register()
        {
            ViewBag.Message = "Registration form.";

            return View();
        }

        [HttpPost]
        public ActionResult Register(RegistrationRequest registration)
        {
            if(ModelState.IsValid)
            {
                EarringsBusinessLogic.Authentication.Contracts.IUserFactory userFactory = new UserFactory();
                string result = userFactory.CreateUser(registration.Email, registration.Username, registration.Password);
                if(result != null)
                {
                    if(result.ToLowerInvariant().Contains("email"))
                    {
                        ModelState.AddModelError("Email", result);
                    }
                    else
                    {
                        ModelState.AddModelError("Username", result);
                    }
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Login()
        {
            ViewBag.Message = "Login form.";

            return View();
        }

    }
}
