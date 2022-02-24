using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Login_Register.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;


namespace Login_Register.Controllers
{
    public class HomeController : Controller
    {
        private MyContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, MyContext context)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("register")]
        public IActionResult Register(User newUser)
        {

            if (ModelState.IsValid)
            {
                if (_context.Users.Any(u => u.email == newUser.email))
                {
                    ModelState.AddModelError("email", "Email is already in Use");
                    return View("Index");
                }
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                newUser.password = Hasher.HashPassword(newUser, newUser.password);
                _context.Add(newUser);
                _context.SaveChanges();
                HttpContext.Session.SetString("Useremail", newUser.email);
                return View("Dashboard");
            }
            else
            {
                return View("Index");
            }
        }
        [HttpPost("login")]
        public IActionResult Login(Login loginUser)
        {
            if(ModelState.IsValid)
            {
                User userInDb = _context.Users.FirstOrDefault(u => u.email == loginUser.lemail);
                if(userInDb == null)
                {
                    // If it's null then they are NOT in the database
                    ModelState.AddModelError("lemail", "Invalid login attempt");
                    return View("Index");
                }
                // If we are the correct email it's time to check the password
                // First we make another instance of the password hasher
                PasswordHasher<Login> Hasher = new PasswordHasher<Login>();
                PasswordVerificationResult result = Hasher.VerifyHashedPassword(loginUser, userInDb.password, loginUser.lpassword);
                
                if(result == 0)
                {
                    ModelState.AddModelError("lemail", "Invalid login attempt");
                    return View("Index");
                }
                // HttpContext.Session.SetString("Useremail", userInDb.email);
                return View("Dashboard");
            } else 
            {
                return View("Index");
            }
        }

        [HttpGet("Dashboard")]
        public IActionResult Success()
        {
            // I want to access my data
            //  string email = HttpContext.Session.GetString("Useremail");
            //  User loggedIn = _context.Users.FirstOrDefault(d => d.email == email);
            return View("Dashboard");
        }
        [HttpGet("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
        }
    }

