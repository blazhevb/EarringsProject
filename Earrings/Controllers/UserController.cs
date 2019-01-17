﻿using Earrings.Models;
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
using EarringsBusinessLogic.Authentication.Abstractions;

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
                        AuthenticationManagerBase authenticationManager = new AuthenticationManager();
                        authenticationManager.Login(model.Username);
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
