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
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegistrationRequest registration)
        {
            if(ModelState.IsValid)
            {
                EarringsBusinessLogic.Authentication.Contracts.IUserFactory userFactory = new UserFactory();
                userFactory.CreateUser(registration.Email, registration.Username, registration.Password);
            }
            return View();
        }

        public ActionResult Login()
        {
            ViewBag.Message = "Login form.";

            return View();
        }

    }
}
