using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Personal_FirstProject.Models;

namespace Personal_FirstProject.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDBContext _db;

        public UserController(ApplicationDBContext db)
        {
            _db = db;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        // [ValidateAntiForgeryToken]
        public IActionResult Register(User user)
        {
            // user.IsDeleted = false;
            if (ModelState.IsValid)
            {
                if (_db.Users.Any(u => u.Username == user.Username))
                {
                    ModelState.AddModelError("Username", "Username already exists.");
                }
                if (_db.Users.Any(u => u.Email == user.Email))
                {
                    ModelState.AddModelError("Email", "Email already exists.");
                }
                if (_db.Users.Any(u => u.PhoneNumber == user.PhoneNumber))
                {
                    ModelState.AddModelError("PhoneNumber", "Phone number already exists.");
                }
                if (!ModelState.IsValid)
                {
                    return View(user);
                }
                user.PasswordHash = GetMd5Hash(user.PasswordHash);
                this._db.Users.Add(user);
                this._db.SaveChanges();
                return RedirectToAction("Login", "Login");
            }
            return View(user);
        }

        public static string GetMd5Hash(string password)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder sBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
                return sBuilder.ToString();
            }
        }
    }
}