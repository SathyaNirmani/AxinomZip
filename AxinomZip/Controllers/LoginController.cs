using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AxinomZip.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace AxinomZip.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("CheckValid")]
        public async Task<IActionResult> CheckValid(LoginData model)
        {

            if (ModelState.IsValid)
            {
                var IsValid = (model.UserName == "Axinom" && model.Password == "1234");
                if (!IsValid)
                {
                    ModelState.AddModelError("", "username or password is invalid");
                    return View("Index");

                }
                else
                {
                    HttpContext.Session.SetString("UserName", "Axinom");
                    HttpContext.Session.SetString("Password", "1234");

                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ModelState.AddModelError("", "User Name or Password is Empty");
                return View("Index");
            }

            return View("Index");
        }
    }
}