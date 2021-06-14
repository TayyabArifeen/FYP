using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectFY.Models;

namespace ProjectFY.Controllers
{
    public class LoginController : Controller
    {
        private readonly JIECContext _context;
        public LoginController(JIECContext context)
        {
            this._context=context;
        }
        public IActionResult Login()
        {
            return this.View();
        }
        [HttpPost]
        public IActionResult Login([Bind("UserEmail,UserPassword,UserRole")] User user)
        {
            
            
                if(user.UserRole==1)
                {
                    if(this._context.User.Any(c=>c.UserEmail==user.UserEmail && c.UserRole==user.UserRole))
                    {
                        if(this._context.User.Any(c=>c.UserPassword==user.UserPassword))
                        {
                            return this.RedirectToAction("index","Users");
                        }
                        else
                            return this.View(user);
                    }
                    else
                        return this.View(user);
                }
                else if(user.UserRole==2)
                {
                    if (this._context.User.Any(c => c.UserEmail == user.UserEmail && c.UserRole == user.UserRole))
                    {
                        if (this._context.User.Any(c => c.UserPassword == user.UserPassword))
                        {
                            return this.RedirectToAction("Predictions", "User");
                        }
                        else
                            return this.View(user);
                    }
                    else
                        return this.View(user);
                }
                else
                {
                    return this.View(user);
                }            
        }
        public IActionResult RegisterUser()
        {
            return this.View();
        }
        [HttpPost]
        public IActionResult RegisterUser([Bind("UserID,UserName,UserEmail,UserPassword")] User user)
        {
            if(!this._context.User.Any(c=>c.UserEmail==user.UserEmail))
            {
                user.UserRole = 3;
                this._context.User.Add(user);
                this._context.SaveChanges();
            }                
            return this.View(user);
        }
    }
}