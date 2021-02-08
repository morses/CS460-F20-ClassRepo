﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Fuji.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Fuji.Data;

namespace Fuji.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly FujiDbContext _fujiDbContext;

        public HomeController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager, FujiDbContext fujiContext)
        {
            _logger = logger;
            _userManager = userManager;
            _fujiDbContext = fujiContext;
        }
        
        public async Task<IActionResult> Index()
        {
            // Information straight from the Controller (does not need to go to the database)
            bool isAdmin = User.IsInRole("Admin");
            bool isAuthenticated = User.Identity.IsAuthenticated;
            string name = User.Identity.Name;
            string authType = User.Identity.AuthenticationType;
            
            // Information from Identity through the user manager
            string id = _userManager.GetUserId(User);       // reportedly does not need to hit the db
            IdentityUser user = await _userManager.GetUserAsync(User);  // does go to the db
            string email = user?.Email ?? "no email";
            string phone = user?.PhoneNumber ?? "no phone number";
            ViewBag.Message = $"User {name} is authenticated? {isAuthenticated} using type {authType} and is an Admin? {isAdmin}.  ID from Identity is {id}, email is {email} and phone is {phone}";

            FujiUser fu = null;
            if(id != null)
            {
                fu = _fujiDbContext.FujiUsers.Where(u => u.AspnetIdentityId == id).FirstOrDefault();
            }

            var appleList = _fujiDbContext.Apples.ToList();
            MainPageVM vm = new MainPageVM { TheIdentityUser = user, TheFujiUser = fu, Apples = appleList };
            return View(vm);
        }

        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
