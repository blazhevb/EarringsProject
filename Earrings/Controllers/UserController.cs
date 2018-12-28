﻿using Earrings.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EarringsBusinessLogic;
using EarringsBusinessLogic.Authentication;
using EarringsBusinessLogic.Authentication.Contracts;

namespace Earrings.Controllers
{
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
                        WriteCookie(Response, tk);
                        return RedirectToAction("Index", "Home", tk);
                    default:
                        ModelState.AddModelError("Other", "Please try again.");
                        break;
                }
            }
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            ViewBag.Message = "Login form.";

            return View();
        }

        private void WriteCookie(HttpResponseBase response, string token)
        {
            HttpCookie cookie = new HttpCookie("auth");
            cookie.Value = token;
            response.Cookies.Add(cookie);
        }
    }
}
