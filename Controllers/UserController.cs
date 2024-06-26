using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Bcpg;
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
                user.PasswordHash = PasswordHash.MD5Hash.GetMd5Hash(user.PasswordHash);
                this._db.Users.Add(user);
                this._db.SaveChanges();
                return RedirectToAction("Login", "Login");
            }
            return View(user);
        }

        public IActionResult UserProfile(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = this._db.Users.FirstOrDefault(u => u.UserId == id && !u.IsDeleted);
            ViewBag.Password = user.PasswordHash;

            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        public IActionResult UserProfile(int? id, User user)
        {
            if (id == null)
            {
                return NotFound();
            }

            var existingUser = _db.Users.FirstOrDefault(u => u.UserId == id);
            if (existingUser == null)
            {
                return NotFound();
            }

            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.DateOfBirth = user.DateOfBirth;
            existingUser.Gender = user.Gender;
            existingUser.Address = user.Address;
            existingUser.ZipCode = user.ZipCode;
            existingUser.IDCardPassportNumber = user.IDCardPassportNumber;
            if (existingUser.PasswordHash == user.PasswordHash)
            {
                existingUser.PasswordHash = user.PasswordHash;
            }
            else
            {
                existingUser.PasswordHash = PasswordHash.MD5Hash.GetMd5Hash(user.PasswordHash);
            }
            ViewBag.Password = user.PasswordHash;
            existingUser.Role = user.Role;
            _db.SaveChanges();
            return View(user);
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