using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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


        public ActionResult Login()
        {
            ViewBag.Message = "Login form.";

            return View();
        }
    }
}
