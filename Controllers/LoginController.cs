using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Personal_FirstProject.Models;

namespace Personal_FirstProject.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDBContext _db;

        public LoginController(ApplicationDBContext db)
        {
            _db = db;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string login, string password)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                ViewBag.Error = "Please enter both Email/Username and password.";
                return View();
            }

            string passwordHash = PasswordHash.MD5Hash.GetMd5Hash(password);

            var user = _db.Users.FirstOrDefault(u =>
                (u.Email == login || u.Username == login) && u.PasswordHash == passwordHash && !u.IsDeleted);

            if (user != null)
            {
                const string _User = "_User";
                // const string _UserId = "_UserId";
                // HttpContext.Session.SetInt32(_UserId, user.UserId);

                if (user.Role.ToLower() == "admin" || user.Role.ToLower() == "hrm")
                {
                    HttpContext.Session.SetString(_User, JsonSerializer.Serialize(user));
                    return RedirectToAction("Index", "Admin");
                }
                else if (user.Role.ToLower() == "user")
                {
                    HttpContext.Session.SetString(_User, JsonSerializer.Serialize(user));
                    return RedirectToAction("UserProfile", "User", new { id = user.UserId });
                }
            }
            else
            {
                ViewBag.Error = "Invalid Email/Username or Password.";
                return View();
            }

            return View();
        }

        public IActionResult Logout()
        {   
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Login");
        }
        

        // public static string GetMd5Hash(string password)
        // {
        //     using (MD5 md5Hash = MD5.Create())
        //     {
        //         byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
        //         StringBuilder sBuilder = new StringBuilder();
        //         for (int i = 0; i < data.Length; i++)
        //         {
        //             sBuilder.Append(data[i].ToString("x2"));
        //         }
        //         return sBuilder.ToString();
        //     }
        // }
    }
}