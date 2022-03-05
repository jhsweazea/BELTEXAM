using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BELTEXAM.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace BELTEXAM.Controllers
{
    public class LoginRegController : Controller
    {
        private MyContext _context;
        public LoginRegController(MyContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View("Index");
        }
        [HttpPost("register")]
        public IActionResult Register(User fromForm)
        {
            if(ModelState.IsValid)
            {
                //check if email is already registered
                if(_context.Users.Any(u => u.Email == fromForm.Email))
                {
                    //kindly gtfo if you are
                    ModelState.AddModelError("Email", "Email is already in use. Please log in instead..");
                    return Index();
                }
                //if not, check that password..
                PasswordHasher<User> hasher = new PasswordHasher<User>();
                fromForm.Password = hasher.HashPassword(fromForm, fromForm.Password);
                _context.Add(fromForm);
                _context.SaveChanges();
                HttpContext.Session.SetInt32("UserId", fromForm.UserId);
                return RedirectToAction("Dashboard", "Activity");
            }
            else
            {
                return Index();
            }
        }
        [HttpPost("login")]
        public IActionResult Login(LoginUser fromForm)
        {
            if(ModelState.IsValid)
            {
                User inDb = _context.Users.FirstOrDefault(u => u.Email == fromForm.LogEmail);

                if (inDb == null)
                {
                    ModelState.AddModelError("LogEmail", "Invalid Email/Password.");
                    return Index();
                }
                PasswordHasher<LoginUser> hasher = new PasswordHasher<LoginUser>();
                var result = hasher.VerifyHashedPassword(fromForm, inDb.Password, fromForm.LogPassword);
                if(result == 0)
                {
                    ModelState.AddModelError("LogEmail", "Invalid Email/Password.");
                    return Index();
                }
                HttpContext.Session.SetInt32("UserId", inDb.UserId);
                //PLACEHOLDER
                return RedirectToAction("Dashboard", "Activity");
            }
            else
            {
                return Index();
            }
        }
        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
