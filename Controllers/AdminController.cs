using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Personal_FirstProject.Filters;
using Personal_FirstProject.Models;

namespace Personal_FirstProject.Controllers
{
    [StaffAuth]
    public class AdminController : Controller
    {
        private readonly ApplicationDBContext _db;

        public AdminController(ApplicationDBContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<User> listUsers = _db.Users.ToList();
            return View(listUsers);
        }
        public IActionResult HRMManagement()
        {
            IEnumerable<User> listUsers = _db.Users.Where(u => u.Role.ToLower() == "hrm").ToList();
            return View(listUsers);
        }
        public IActionResult UserManagement()
        {
            IEnumerable<User> listUsers = _db.Users.Where(u => u.Role.ToLower() == "user").ToList();
            return View(listUsers);
        }

        public IActionResult Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = _db.Users.FirstOrDefault(u => u.UserId == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        // [ValidateAntiForgeryToken]
        public IActionResult Create(User user)
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
                return RedirectToAction("Index");
            }
            return View(user);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = _db.Users.FirstOrDefault(u => u.UserId == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        public IActionResult Delete(int? id, User user)
        {
            if (id == null)
            {
                return NotFound();
            }

            user = _db.Users.FirstOrDefault(u => u.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            user.IsDeleted = true;

            this._db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult RestoreUser(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = _db.Users.FirstOrDefault(u => u.UserId == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        public IActionResult RestoreUser(int? id, User user)
        {
            if (id == null)
            {
                return NotFound();
            }

            user = _db.Users.FirstOrDefault(u => u.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            user.IsDeleted = false;

            this._db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = _db.Users.FirstOrDefault(u => u.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            ViewBag.Password = user.PasswordHash;
            return View(user);
        }

        // 
        [HttpPost]
        public IActionResult Update(int? id, User user)
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
            existingUser.Role = user.Role;
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        
    }
}
